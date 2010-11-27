using UnityEngine;
using System.Collections;

public class LevelAttributes : MonoBehaviour {
	
	public Rect bounds;
	static private LevelAttributes instance;
	
	Vector3 upperLeft;
	Vector3 lowerLeft;
	Vector3 upperRight;
	Vector3 lowerRight;

	// Use this for initialization
	//void Start () {
	//
	//}
	
	// Update is called once per frame
	//void Update () {
	//
	//}
	
	public static LevelAttributes GetInstance() {
		if (instance == null) {
			instance = (LevelAttributes)FindObjectOfType(typeof(LevelAttributes));
			if (!instance) {
				Debug.LogError("There must be an active LevelAttributes in the scene.");
			}
		}
		return instance;
	}
	
	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
        lowerLeft = new Vector3(bounds.xMin, bounds.yMax, 0);
        upperLeft = new Vector3(bounds.xMin, bounds.yMin, 0);
        lowerRight = new Vector3(bounds.xMax, bounds.yMax, 0);
        upperRight = new Vector3(bounds.xMax, bounds.yMin, 0);
        
        Gizmos.DrawLine(lowerLeft, upperLeft);
        Gizmos.DrawLine(upperLeft, upperRight);
        Gizmos.DrawLine(upperRight, lowerRight);
        Gizmos.DrawLine(lowerRight, lowerLeft);
	}
}