using UnityEngine;
using System.Collections;

public class EnemyTurn : MonoBehaviour {

	private EnemyStats enemyStatsScript = new EnemyStats();

	int critVal;
	int actionVal;

	public int eDamageRange;
	public int eDamageToDo;

	public int playerHP;
	public int enemyStrength;

	public bool iscrit = false;
	public bool isAction = false;


	public void EnemyAttackMethod(){

		EDamageMethod ();
	}
	
	public void ECritCheck(){
		
		critVal = GetRandomValueCrit ();
		
		if(critVal >= 4){
			iscrit = false;
		}
		else if(critVal < 4){
			iscrit = true;
		}
	}

	int GetRandomValueCrit() {
		float rand = Random.value;
		if (rand <= .1f)
			return Random.Range(0, 4);
		
		return Random.Range(5, 11);
	}

	public void ECheckAction(){

		actionVal = GetRandomValueAction ();

		if(actionVal >= 4){
			isAction = true;
		}
		else if(critVal < 4){
			isAction = false;
		}
	}

	int GetRandomValueAction(){
		float rand1 = Random.value;
		if (rand1 <= .2f)
			return Random.Range(0, 4);
		
		return Random.Range(5, 11);
	}

	public void EDamageMethod(){

		playerHP = GameManager.instance.playerHP;
		enemyStrength = EnemyObjectDisable.instance2.enemyStatsScript.eStrength;

		ECheckAction ();

		if (isAction == true) {
			eDamageRange = Random.Range ((enemyStrength - 2), (enemyStrength + 2));
			ECritCheck ();
		
			if (iscrit == false) {
				eDamageToDo = eDamageRange;
			} else if (iscrit == true) {
				eDamageToDo = (int)(eDamageRange * 1.5f);
			}
		
			playerHP -= eDamageToDo;
		
			GameManager.instance.playerHP = playerHP;

		}

	}

	public void ECheckandSwitch(){
		
		playerHP = GameManager.instance.playerHP;
		
		//check if player hp <= 0
		//change to PlayerTurn if still alive
		//change to postBattle (Loss) if dead
		/*if (playerHP <= 0) {
			
			playerHP = 0;
			EnemyStatsScript.eHitPoints = eHitPoints;
			enemyVanquished = true;
			BattleStateMachine.currentState = BattleStateMachine.BattleState.PostBattleWin;
		}*/
		if (playerHP > 0) {
			
			BattleStateMachine.currentState = BattleStateMachine.BattleState.PlayerTurn;
		} 
		else if (playerHP <= 0) {

			//Load Game Over screen
			Application.LoadLevel ("GameOver");

		}
	}

}
