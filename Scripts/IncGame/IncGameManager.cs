using UnityEngine;
using System.Collections;

public class IncGameManager : MonoBehaviour {

	public static float benchMax = 1.99f;
	public static float squatMax = 0f;
	public static float deadMax = 0f;

	bool haveDungeonKey = false;



	private bool pushUpBtn;
	private bool dungeonBtn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//round floats for proper display
		benchMax = Mathf.Round(benchMax * 100f) / 100f;


		//set dungeon key to be received after benchmax = whatever
		DungeonKeyInitialGet ();
	}

	void OnGUI(){
		DungeonButton ();

		PushUpButton ();
		BenchMaxLabel ();
	}

	//Bench related things
	void PushUpButton(){
		
		
		pushUpBtn = GUI.Button (new Rect (550, 50, 150, 50), "Push-Up");
		if (pushUpBtn) {
			benchMax += 0.01f;
	}
		}

	 void BenchMaxLabel(){

		GUI.Label (new Rect (550, 300, 150, 50), "Max Bench Press: " + benchMax);
	}

	//door to the dungeon
	void DungeonButton(){

		dungeonBtn = GUI.Button (new Rect (0, 0, 150, 50), "Enter the Dungeon");
		if (dungeonBtn) {
			if (haveDungeonKey == true) {
				haveDungeonKey = false;
				Application.LoadLevel ("Main");

			}
		}
	}

	void DungeonKeyInitialGet(){
		if (benchMax >= 1f) {
			haveDungeonKey = true;
		}
	}

}
