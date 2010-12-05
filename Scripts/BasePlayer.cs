using UnityEngine;
using System.Collections;

public class BasePlayer : MonoBehaviour
{
	public float thrustAmount = 130.0f;
	public float rotationSpeed = 30.0f;
	protected float lockPos = 0.0f;
	protected Player player;
	public float fireTime = 1.0f;
	protected float lastFired = 0.0f;
	
	protected void Awake(){
		Ship ship = gameObject.GetComponent("Ship") as Ship;
		player = ship.owner;
		lastFired = 1.0f;
	}

	protected void Fire(){
		Ship ship = player.currentShip;
		if(lastFired >= fireTime){
			MissileBehaviour m = (MissileBehaviour)Instantiate(ship.missileType, ship.transform.position + (ship.transform.forward * 30.0f), ship.transform.rotation);
			m.Fire(player);
			lastFired = 0.0f;
		}
	}
	
	protected void Update(){
		if(!player.disabled){
	    	rigidbody.rotation = Quaternion.Euler(lockPos, rigidbody.rotation.eulerAngles.y, lockPos);
			transform.rotation = Quaternion.Euler(lockPos, transform.rotation.eulerAngles.y, lockPos);
			transform.position = new Vector3(transform.position.x, 8.0f, transform.position.z);
			lastFired += Time.deltaTime;
		}
	}
	
}

