using UnityEngine;
using System.Collections;

public class GameOverGUI : MonoBehaviour
{
	public bool showing = false;
	
	private Rect MiddleBox(int left, int top, int width, int height) {
		Rect r = new Rect( (Screen.width/2) - (width/2) + left, (Screen.height/2) - (height/2) + top, width, height);
		return r;
	}
	
	void OnGUI() {
		// Make a background box
		if (showing){
			GUI.Box(MiddleBox(0, 0, 100, 40), "GAME OVER");
		}
	}	
}
