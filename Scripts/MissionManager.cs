using UnityEngine;
using System.Collections;

public class MissionManager : MonoBehaviour
{
	
	public ArrayList players;
	public Player thePlayer;
	public Player aiPlayer;
	public bool isRunning = false;
	public int collectiblesLeft = 1;
	public Player winner;
	
	void Awake (){
		players = new ArrayList();
	}
	
	public void AddPlayer(Player p){
		Debug.Log("Added player: " + p.name);
		players.Add(p);
	}
	
	void Update(){
		if (isRunning) {
			//Debug.Log("Running " + this.name);
			foreach(Player p in players){
				//Debug.Log("Checking player " + p.name + " lives: " + p.lives);
				if(p.lives < 1){
					GameOver();
				}
			}
			if (collectiblesLeft < 1){
				Debug.Log("No collectibles left.");
				GameOver();
			}
		}
	}
	
	protected void GameOver(){
		Debug.Log("GAME OVER MAN");
		isRunning = false;
		int maxScore = 0;
		foreach(Player p in players){
			if(p.score > maxScore){
				winner = p;
				maxScore = p.score;
			}
		}
		Debug.Log("Winner: " + winner.name);
		Managers.Game.SetState(typeof(GameOverState));
	}
	
	public void Reset(){
		players.Clear();
		isRunning = false;
		winner = null;
	}
	
}

