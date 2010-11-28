using UnityEngine;
using System.Collections;

public class GameOverState : GameState
{
	
    public override void OnActivate() {
		Debug.Log("Waking Up Game Over State");
		GameOverGUI gog = FindObjectOfType(typeof(GameOverGUI)) as GameOverGUI;
		Debug.Log("Found GOG: " + gog.name);
		gog.showing = true;
	}
	
    public override void OnDeactivate() {
		Managers.Mission.Reset();
		Application.LoadLevel(1);
	}
	
    public override void OnUpdate() {
		if(Input.GetButtonDown("Fire1")) {
			Managers.Game.SetState(typeof(MainMenuState));
		}
	}

}

