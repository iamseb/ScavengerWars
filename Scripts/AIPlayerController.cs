using System;
using UnityEngine;
using System.Collections;

public class AIPlayerController : MonoBehaviour
{

	public float thrustAmount = 130.0f;
	public float rotationSpeed = 60.0f;
	private float lockPos = 0.0f;
	public bool hasTarget = false;
	public Vector3 currentTarget;
	public Vector3 waypoint;
	public float distanceToTarget = 0.0f;
	public bool isMoving = false;
	public bool targetsAvailable = true;
	
	void Awake() {
	}

	void Update()
	{
		if(!hasTarget && targetsAvailable){
			Collectible next = FindClosestCollectible();
			if (next == null) {
				targetsAvailable = false;
			}
			else {
				currentTarget = next.transform.position;
				hasTarget = true;	
			}
		}
    	rigidbody.rotation = Quaternion.Euler(lockPos, rigidbody.rotation.eulerAngles.y, lockPos);
		transform.rotation = Quaternion.Euler(lockPos, transform.rotation.eulerAngles.y, lockPos);
		transform.position = new Vector3(transform.position.x, 8.0f, transform.position.z);
		Debug.DrawRay(transform.position, transform.forward * 50, Color.blue);
		if(hasTarget){
			distanceToTarget = (transform.position - currentTarget).magnitude;
			Debug.Log("Distance to target: " + distanceToTarget);
			float angleToTarget = turnToTarget();
			if (angleToTarget < 1.0f){
				if(isPathClear() && distanceToTarget > 20.0f){
					isMoving = true;	
				};
				if(distanceToTarget < 20.0f){
					isMoving = false;
					hasTarget = false;
				}
			}
			Debug.DrawLine(transform.position, currentTarget, Color.magenta);
		}
	}
	
	void FixedUpdate() { //Use FixedUpdate for Physics changes
		if(isMoving && rigidbody.velocity.magnitude*2 < distanceToTarget) {
			float thrustModifier = 10.0f * Time.fixedDeltaTime;
		    rigidbody.AddRelativeForce(Vector3.forward * thrustAmount * thrustModifier, ForceMode.Acceleration);
		} else if(isMoving && rigidbody.velocity.magnitude*2 >= distanceToTarget) {
			float thrustModifier = -15.0f * Time.fixedDeltaTime;
		    rigidbody.AddRelativeForce(Vector3.forward * thrustAmount * thrustModifier, ForceMode.Acceleration);
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
				Vector3 reverse = g.transform.position - transform.position;
				Vector3 newpos = g.transform.position + (-transform.right * 50 + reverse.normalized);
				currentTarget = newpos;
				return false;
			}
		}
		return true;
	}
}

