using UnityEngine;
using System.Collections;

public class LevelAttributes : MonoBehaviour
{
	void Awake(){
		Player p = (Player)Instantiate(Managers.Mission.thePlayer);
		SpawnPoint spawn = FindObjectOfType(typeof(SpawnPoint)) as SpawnPoint;
		spawn.owner = p;
		p.spawn = spawn;
		Managers.Mission.AddPlayer(p);
	}
}
