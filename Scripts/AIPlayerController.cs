using System;
using UnityEngine;
using System.Collections;

public class AIPlayerController : BasePlayer
{

	public bool hasTarget = false;
	public Vector3 currentTarget;
	public float distanceToTarget = 0.0f;
	public bool isMoving = false;
	public bool targetsAvailable = true;
	public float scanTime = 0.1f;
	private float lastScan = 0.0f;
	public float fieldOfViewRange = 60.0f;
	public float fireDetectionRange = 200.0f;
	
	
	void ScanForMissileTarget(){
		if(CanSeePlayer()){
			Fire();
		}
	}

	
	bool CanSeePlayer(){
	    RaycastHit hit;
		Player op = (Player)Managers.Mission.players[0];
		Ship s = player.currentShip;
	    Vector3 rayDirection = op.currentShip.transform.position - s.transform.position;
	    //float distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);
	    //if(Physics.Raycast(transform.position, rayDirection, out hit)){ // If the player is very close behind the enemy and not in view the enemy will detect the player
	    //    if(hit.transform.gameObject.GetComponent("Ship") && (distanceToPlayer <= minPlayerDetectDistance)){
	    //        //Debug.Log("Caught player sneaking up behind!");
	    //        return true;
	    //    }
	    //}
	
	    if((Vector3.Angle(rayDirection, s.transform.forward)) < fieldOfViewRange){ // Detect if player is within the field of view
	    	if (Physics.Raycast(s.transform.position, rayDirection, out hit, fireDetectionRange)) {
	            if(hit.transform.gameObject.GetComponent("Ship")) {
	                //Debug.Log("Can see player");
	                return true;
	            }else{
	                //Debug.Log("Can not see player");
	                return false;
	            }
	        }
	    }
		return false;
	}


	void OnDrawGizmos(){
		Ship s = player.currentShip;
	    // Draws a line in front of the player and one behind this is used to visually illustrate the detection ranges in front and behind the enemy
	    Gizmos.color = Color.yellow; // the color used to detect the player in front
	    Gizmos.DrawRay (s.transform.position, s.transform.forward * fireDetectionRange);
	}
	
	
	new void Update()
	{
		base.Update();
		if(!player.disabled){
			lastScan += Time.deltaTime;
			if(lastScan > scanTime){
				ScanForMissileTarget();
				if(targetsAvailable){
					Collectible next = FindClosestCollectible();
					if (next == null) {
						targetsAvailable = false;
					}
					else {
						currentTarget = next.transform.position;
						hasTarget = true;	
					}
				}
				lastScan = 0.0f;
			}
			if(hasTarget){
				bool pathClear = isPathClear();
				distanceToTarget = (transform.position - currentTarget).magnitude;
				//Debug.Log("Distance to target: " + distanceToTarget);
				float angleToTarget = turnToTarget();
				if (angleToTarget < 1.0f){
					if(pathClear && distanceToTarget > 2.0f){
						isMoving = true;	
					};
					if(distanceToTarget < 2.0f){
						isMoving = false;
						hasTarget = false;
					}
				}
				else {
					isMoving = false;
				}
				Debug.DrawLine(transform.position, currentTarget, Color.magenta);
			}
		}
	}
	
	void FixedUpdate() {
		if(!player.disabled){
			if(isMoving && rigidbody.velocity.magnitude*2 < distanceToTarget) {
				float thrustModifier = 20.0f * Time.fixedDeltaTime;
			    rigidbody.AddRelativeForce(Vector3.forward * thrustAmount * thrustModifier, ForceMode.Acceleration);
			} else if(isMoving && rigidbody.velocity.magnitude*3 >= distanceToTarget) {
				float thrustModifier = -25.0f * Time.fixedDeltaTime;
			    rigidbody.AddRelativeForce(Vector3.forward * thrustAmount * thrustModifier, ForceMode.Acceleration);
			} else if(!isMoving){
				if(rigidbody.velocity.magnitude > 1.0f){
					rigidbody.AddForce(-rigidbody.velocity);
				}
			}
		}
	}
	
	Collectible FindClosestCollectible() {
		Collectible[] collectibles = FindObjectsOfType(typeof(Collectible)) as Collectible[];
		Collectible closest = null;
		float minDist = 100000.0f;
		foreach(Collectible c in collectibles) {
			Vector3 distance = c.transform.position - transform.position;
			float dist = distance.magnitude;
			if(dist < minDist){
				minDist = dist;
				closest = c;
			}
		}
		return closest;
	}
	
	private float turnToTarget(){
        Transform shipYTrans = transform;
		Vector3 targetTrans = this.currentTarget;
        
        // Get a vector from the ship towards the target
        Vector3 targetVect = targetTrans - shipYTrans.position;
        
        // Set Y to be equal so we are on a XZ plane relative to the ship
        Vector3 shipVect = shipYTrans.forward;
        targetVect.y = shipVect.y;
                        
		// Rotate at the turn speed until less than one increment remains        
        float angleToTarget = Vector3.Angle(shipVect, targetVect);
        float addAngle = 0;
        if (angleToTarget > this.rotationSpeed * Time.deltaTime)
        {
 			addAngle = this.rotationSpeed * Time.deltaTime;
		}
		else   // Use the angle directly.
		{
            addAngle = angleToTarget;
		}

        // Compute the vector perpendicular to the start and destination 
        //		vectors. When the vectors cross over, the perpindicular 
        //		vector will change from pointing up to pointing down, so 
        //		the location in Y will become negative. This will allow us  
        //		to rotate around in the shortest direction.
        Vector3 perp = Vector3.Cross(shipVect, targetVect);
		addAngle *= Math.Sign(perp.y); // The sign of a number is only 1 or -1.
		
        // Add the new angle of change to whatever the turret was at before
		shipYTrans.Rotate(0, addAngle, 0);

		return angleToTarget;
    }
	
	bool isPathClear(){
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.forward, out hit, distanceToTarget)){
			GameObject g = hit.collider.gameObject;
			if(g.GetComponent("EnvironmentCollider")){
				Debug.Log("Path blocked by " + g.name);
				return false;
			}
		}
		return true;
	}
}

