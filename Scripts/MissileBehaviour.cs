using UnityEngine;
using System.Collections;

public class MissileBehaviour : MonoBehaviour
{
	public float acceleration = 500.0f;
	public float damage = 10.0f;
	public Color color = Color.red;
	public Player owner;
	public float explodeRadius = 60.0f;
	
	public void Fire(Player firedBy){
		owner = firedBy;
	}
	
	void Start(){
		rigidbody.AddForce(transform.forward * acceleration, ForceMode.Impulse);
	}

	// Update is called once per frame
	void FixedUpdate(){
		rigidbody.AddForce(transform.forward * acceleration * Time.fixedDeltaTime, ForceMode.Acceleration);
	}
	
	void OnCollisionEnter(Collision collision){
		Debug.Log("Missile hit " + collision.gameObject.name);
		collision.gameObject.SendMessage("Damage", damage);
		owner.SendMessage("ChangeScore", 1.0f);
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, explodeRadius);
        foreach (Collider hit in colliders) {
			Debug.Log("Explosion hit " + hit.gameObject.name);
            if (hit.gameObject.rigidbody){
				Debug.Log("Adding explosive force to " + hit.gameObject.name);
                hit.rigidbody.AddExplosionForce(damage*10.0f, explosionPos, explodeRadius, 0.0f, ForceMode.Impulse);
			}
        }
		Destroy(gameObject);
	}
	
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position, explodeRadius);
	}
	
	public void Damage(float amount){
		return;
	}
	
	public void ChangeScore(float amount){
		return;
	}
}

