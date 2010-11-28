using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float thrustAmount = 130.0f;
	public float rotationSpeed = 10.0f;
	private float lockPos = 0.0f;
	
	void Awake() {
	}

	void Update()
	{
    	rigidbody.rotation = Quaternion.Euler(lockPos, rigidbody.rotation.eulerAngles.y, lockPos);
		transform.rotation = Quaternion.Euler(lockPos, transform.rotation.eulerAngles.y, lockPos);
		transform.position = new Vector3(transform.position.x, 8.0f, transform.position.z);
	}

	
	void FixedUpdate() { //Use FixedUpdate for Physics changes
		float thrustModifier = Input.GetAxis("Vertical");
		float rotationModifier = Input.GetAxis("Horizontal");
	    rigidbody.AddRelativeForce(Vector3.forward * thrustAmount * thrustModifier, ForceMode.Acceleration);
		rigidbody.AddTorque(0, rotationSpeed * rotationModifier, 0, ForceMode.Acceleration);
	}
}

