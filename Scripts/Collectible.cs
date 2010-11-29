using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour
{
	public int val = 20;
	
	public void OnTriggerEnter(Collider other){
		Debug.Log(other.gameObject.name);
		other.gameObject.SendMessage("Collect", val);
		Managers.Mission.collectiblesLeft -= 1;
		Destroy(gameObject);
	}
	
}

