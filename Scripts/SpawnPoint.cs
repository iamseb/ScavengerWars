using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
	public Player owner;
	public Ship shipType;
	
	void Start(){
		Spawn(1);
	}
	
	public void Spawn(int delay){
		StartCoroutine(DelayedSpawn(delay));
	}
	
	protected IEnumerator DelayedSpawn(int delay){
		yield return new WaitForSeconds(delay);
		Ship ship;
		ship = (Ship)Instantiate(shipType, transform.position, transform.rotation);
		ship.owner = owner;
		ship.owner.currentShip = ship;
	}
}

