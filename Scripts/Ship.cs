using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
	public float health = 100.0f;
	public float fuel = 100.0f;
	public int collectedCount = 0;
	public Player owner;
	public bool destroyed = false;
	public MissileBehaviour missileType;
	
	public void Damage(float amount) {
		health -= amount;
		if (health < 0) {
			owner.LoseShip();
			destroyed = true;
		}
	}
	
	public void Collect(int val) {
		collectedCount += 1;
		owner.AddCollected(1);
		ChangeScore(val);
	}
	
	public void ChangeScore(int val){
		owner.ChangeScore(val);
	}
	
	public void Update(){
		if (destroyed) {
			BlowUp();
		}
	}
	
	public void BlowUp(){
		Destroy(gameObject);
	}
	
}
