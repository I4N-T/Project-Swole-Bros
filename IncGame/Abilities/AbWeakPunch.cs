using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class AbWeakPunch /*: BaseAbility*/ {

	string aname = "Weak Punch";
	string adescription = "A weak punch. It doesn't do a lot of damage.";

	float adamageLowerLim = 1f;
	float adamageUpperLim = 4f;
	int aCalorieCost = 100;

	public int damageRange;
	public int damageToDo;
	public int critVal;
	public bool iscrit;
	public int enemyHP;
	public int playerCalories;

	public bool insufficientCalBool;

	/*public AbWeakPunch()
		: base(new BaseAbility(aname, adescription, adamageLowerLim, adamageUpperLim, aCalorieCost)
		       {
			damageToDo = Random.Range(1,5);


		}*/
	public void UseWeakPunch(){
		playerCalories = GameManager.instance.playerCalories;

		if (playerCalories >= aCalorieCost) {
			WeakPunchCritCheck ();
			WeakPunchDamageMethod ();
		} else if (playerCalories < aCalorieCost) {
			insufficientCalBool = true;
		}
	}

		public void WeakPunchCritCheck(){
			
			critVal = GetRandomValue ();
			
			if(critVal >= 4){
				iscrit = false;
			}
			else if(critVal < 4){
				iscrit = true;
			}
		}
		
		int GetRandomValue() {
			float rand = Random.value;
			if (rand <= .3f)
				return Random.Range(0, 4);
			
			return Random.Range(5, 11);
		}

	public void WeakPunchDamageMethod(){
		
		enemyHP = EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints;

		damageRange = (int)Random.Range (adamageLowerLim, adamageUpperLim);
		
		if (iscrit == false) {
			damageToDo = damageRange;
		} else if (iscrit == true) {
			damageToDo = (int)(damageRange * 2f);
		}
		
		enemyHP -= damageToDo;

		GameManager.instance.playerCalories -= aCalorieCost;
		
		
		EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints = enemyHP;
	}


}
