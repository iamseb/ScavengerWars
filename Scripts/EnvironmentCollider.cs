using UnityEngine;
using System.Collections;

public class EnvironmentCollider : Indestructable
{
	public float baseDamage = 0.1f;
	public int scoreRemover = 1;

	void OnCollisionEnter(Collision collision) {
		collision.gameObject.SendMessage("Damage", baseDamage*collision.relativeVelocity.magnitude);
		collision.gameObject.SendMessage("ChangeScore", -scoreRemover);
    }

}
