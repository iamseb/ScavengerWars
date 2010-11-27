using UnityEngine;
using System.Collections;

public class EnvironmentCollider : MonoBehaviour
{
	public float baseDamage = 0.1f;

	void OnCollisionEnter(Collision collision) {
		collision.gameObject.SendMessage("Damage", baseDamage*collision.relativeVelocity.magnitude);
    }

}

