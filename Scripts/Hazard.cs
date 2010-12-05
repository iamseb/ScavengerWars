using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour
{
	
	public float damagePerSecond = 10.0f;
	public Color color = Color.black;

	void OnTriggerStay(Collider other){
		other.gameObject.SendMessage("Damage", damagePerSecond * Time.fixedDeltaTime);
	}
	
	void Start(){
		this.particleEmitter.renderer.material.color = color;
	}
}

