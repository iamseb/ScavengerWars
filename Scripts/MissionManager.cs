using UnityEngine;
using System.Collections;

public class MissionManager : MonoBehaviour
{
	
	public ArrayList players;
	public Player thePlayer;
	public bool isRunning = true;
	
	void Awake (){
		players = new ArrayList();
	}
	
	public void AddPlayer(Player p){
		Debug.Log("Added player: " + p.name);
		players.Add(p);
	}
	
	void Update(){
		if (isRunning) {
			foreach(Player p in players){
				Debug.Log("Checking player " + p.name + " lives: " + p.lives);
				if(p.lives < 1){
					GameOver();
				}
			}
		}
	}
	
	protected void GameOver(){
		Debug.Log("GAME OVER MAN");
		isRunning = false;
		Managers.Game.SetState(typeof(GameOverState));	
	}
	
	public void Reset(){
		players.Clear();
		isRunning = true;
	}
	
}

