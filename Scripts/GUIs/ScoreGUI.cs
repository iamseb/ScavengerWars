using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour
{

	void OnGUI() {
		// GUILayout.BeginArea(new Rect(Screen.width-300, Screen.height-100, Screen.width, Screen.height));
		GUILayout.BeginArea(new Rect(Screen.width-300, 0, 300, 200));
		ArrayList players = Managers.Mission.players;
		foreach (Player p in players) {
			GUILayout.Box(p.name + " score: " + p.score);
			GUILayout.Box(p.name + " collected: " + p.collectedCount);	
			if (p.currentShip != null){
				GUILayout.Box(p.name + " health: " + p.currentShip.health);
			}
		}
		GUILayout.EndArea();
	}
}

