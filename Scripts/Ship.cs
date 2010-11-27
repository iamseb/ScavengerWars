using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
	public float health = 100.0f;
	public float fuel = 100.0f;

	public void Damage(float amount) {
		health -= amount;
	}
	
}

