using UnityEngine;
using System.Collections;

public class LevelAttributes : MonoBehaviour
{
	
	void Awake(){
		Debug.Log("Awake: " + this.name);
		
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
		
		Collectible[] collectibles = FindObjectsOfType(typeof(Collectible)) as Collectible[];
		Managers.Mission.collectiblesLeft = collectibles.Length;
		
		Managers.Mission.isRunning = true;
	}
}
