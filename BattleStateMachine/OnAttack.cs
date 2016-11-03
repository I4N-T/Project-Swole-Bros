using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class OnAttack : MonoBehaviour {

	//public GameObject otherObject;

	//private EnemyObjectDisable enemyObjectScript1 = new EnemyObjectDisable ();
	//private EnemyObjectDisable enemyObjectScript;

	public int critVal;
	public int damageRange;
	public int damageToDo;

	public int playerStrength;
	public int enemyHP;

	public bool iscrit;

	void Awake(){
	
		//enemyObjectScript =  otherObject.GetComponent<EnemyObjectDisable> ();
	
	}

	public void PlayerAttackMethod(){

		CritCheck ();
		DamageMethod ();
	}

	public void CritCheck(){
		 
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

	public void DamageMethod(){

		enemyHP = EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints;
		playerStrength = GameManager.instance.playerStrength;
		damageRange = Random.Range ((playerStrength - 2), (playerStrength + 2));

		if (iscrit == false) {
			damageToDo = damageRange;
		} else if (iscrit == true) {
			damageToDo = (int)(damageRange * 1.5f);
		}

		enemyHP -= damageToDo;


		EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints = enemyHP;
	}
	
}
