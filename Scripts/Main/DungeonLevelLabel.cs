using UnityEngine;
using System.Collections;

public class DungeonLevelLabel : MonoBehaviour {

	public int gymLevel;


	void OnGUI(){
		GUI.Label (new Rect (0, 0, 150, 50), "Current Floor: " + gymLevel);
	}
	// Use this for initialization
	void Start () {
			gymLevel = GameManager.instance.gymLevel;
	
	}
	
	// Update is called once per frame
	void Update () {
		gymLevel = GameManager.instance.gymLevel;
	}
}
