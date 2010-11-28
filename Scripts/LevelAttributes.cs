using UnityEngine;
using System.Collections;

public class LevelAttributes : MonoBehaviour
{
	void Awake(){
		Player p = (Player)Instantiate(Managers.Mission.thePlayer);
		p.name = "Player 1";
		Managers.Mission.AddPlayer(p);
		
		Player ai = (Player)Instantiate(Managers.Mission.thePlayer);
		ai.name = "AI Player";
		ai.isAIPlayer = true;
		Managers.Mission.AddPlayer(ai);
		
		SpawnPoint[] spawns = FindObjectsOfType(typeof(SpawnPoint)) as SpawnPoint[];
		
		spawns[0].owner = p;
		p.spawn = spawns[0];
		
		spawns[1].owner = ai;
		ai.spawn = spawns[1];
		
	}
}
