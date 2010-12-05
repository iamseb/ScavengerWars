using UnityEngine;
using System.Collections;

public class PlayerController : BasePlayer
{
	public KeyCode fireKey = KeyCode.Space;
	private ArrayList handledKeys;
	
	new void Awake(){
		base.Awake();
		handledKeys = new ArrayList();
		handledKeys.Add(fireKey);
		Managers.Input.RegisterHandler(this.gameObject, handledKeys);
	}
	
	void KeyUp(KeyCode key){
	}
	
	void KeyDown(KeyCode key){
		if(key == fireKey){
			Fire();
		}
	}

	
	void FixedUpdate(){ //Use FixedUpdate for Physics changes
		if(!player.disabled){
			float thrustModifier = Input.GetAxis("Vertical");
			float rotationModifier = Input.GetAxis("Horizontal");
		    rigidbody.AddRelativeForce(Vector3.forward * thrustAmount * thrustModifier, ForceMode.Acceleration);
			rigidbody.AddTorque(0, rotationSpeed * rotationModifier, 0, ForceMode.Acceleration);
		}
	}
}

