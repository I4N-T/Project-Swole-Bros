using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class CalculateReward : MonoBehaviour {

	public static bool getGoldBool;

	public static int calcGoldTier = 1;
	public static int goldToGive = 0;

	public static float rand3;


	public static void CalcReward (){
		//gets gold?
		SetGetGoldBool ();
		//calculate amount of gold
		CalculateGoldTier ();
		CalculateGoldAmount ();
		//gets loot?

		//determine which loot
	}
	
	public static void SetGetGoldBool() {
		float rand1 = Random.value;
		if (rand1 <= .3f)
			getGoldBool = true;
		else if (rand1 > .3) {
			getGoldBool = false;
		}
	}

	public static void CalculateGoldTier(){
		if (getGoldBool == true) {
			float rand2 = Random.value;
			if (rand2 <= .6){
				calcGoldTier = 1;
			}
			else if (rand2 > .6 && rand2 <= .9){
				calcGoldTier = 2;
			}
			else if (rand2 > .9){
				calcGoldTier = 3;
			}
		}
	}

	public static void CalculateGoldAmount(){

		if (calcGoldTier == 1) {
			float rand3 = Random.Range(0,4);
			goldToGive = (int)rand3; //multiply rand3 by ENEMYLEVELFACTOR
		}
		else if (calcGoldTier == 2) {
			float rand3 = Random.Range(4,6);
			goldToGive = (int)rand3; //multiply rand3 by ENEMYLEVELFACTOR
		}
		else if (calcGoldTier == 3) {
			float rand3 = 8;
			goldToGive = (int)rand3; //multiply rand3 by ENEMYLEVELFACTOR
		}
	}

	//Add loot reward methods

}
