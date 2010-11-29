using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	
	public bool isAIPlayer = false;
	public int score = 0;
	public int collectedCount = 0;
	public int lives = 3;
	public SpawnPoint spawn;
	public Ship currentShip;
	public bool disabled = false;

	public void ChangeScore(int val){
		score += val*10;
	}
	
	public void AddCollected(int count){
		collectedCount += count;
	}
	
	public void LoseShip(){
		currentShip = null;
		lives -= 1;
		if(lives > 0){
			spawn.Spawn(1);
		}
	}
}

