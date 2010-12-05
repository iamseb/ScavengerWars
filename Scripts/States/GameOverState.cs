using UnityEngine;
using System.Collections;

public class GameOverState : GameState
{
	private GameOverGUI gog;
	
    public override void OnActivate() {
		Debug.Log("Waking Up Game Over State");
		gog = FindObjectOfType(typeof(GameOverGUI)) as GameOverGUI;
		Debug.Log("Found GOG: " + gog.name);
		gog.showing = true;
		Debug.Log("About to disable " + Managers.Mission.players.Count + " players");
		foreach(Player p in Managers.Mission.players){
			if(p.currentShip){
				p.currentShip.rigidbody.angularVelocity = Vector3.zero;
				p.currentShip.rigidbody.velocity = Vector3.zero;
			}
			p.disabled = true;
		}
	}
	
    public override void OnDeactivate() {
		Managers.Mission.Reset();
		Application.LoadLevel(1);
	}
	
    public override void OnUpdate() {
		if(Input.GetButtonDown("Fire1")) {
			Managers.Game.SetState(typeof(MainMenuState));
			gog.showing = false;
		}
	}

}

