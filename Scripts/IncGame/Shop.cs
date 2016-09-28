using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

	private bool openShopBtn;
	private bool buyConsumablesBtn;
	private bool buyEcerciseEquipmentBtn;

	private bool showConsumables;
	private bool testBool;

	//Texture2D[] repre; //Array of textures displayed on button
	string[] itemNamesA = new string[5]; //Array of levels(scenes) names
	int Y = 0;
	int X;

	// Use this for initialization
	void Start () {
		itemNamesA [0] = "Protein Shake";
		itemNamesA [1] = "Item B";
		itemNamesA [2] = "Item C";
		itemNamesA [3] = "Item D";
		itemNamesA [4] = "Item E";
	}

	void OnGUI(){
		OpenShopButton ();

		if (showConsumables == true) {
			ConsumablesButton();
			if (testBool == true) {
				TestMethOK();
			}
		}

	}

	void OpenShopButton(){

		openShopBtn = GUI.Button (new Rect (200,0,150,50), "Shop");
		if (openShopBtn) {
			//ConsumablesButton();
			showConsumables = true;	
		}		
	}

	void ConsumablesButton(){
		
		buyConsumablesBtn = GUI.Button (new Rect (200,55,150,50), "Consumables");
		if (buyConsumablesBtn) {
			testBool = true;
		}		
	}

	void TestMethOK(){
			for (int i = 0; i < itemNamesA.Length; i++) {
			if (GUI.Button (new Rect (5, Y, 50, 50), itemNamesA[i])) {
				print("w/e");
			}
			Y += 55;
		}
	}



	
	// Update is called once per frame
	void Update () {
	
	}
}
