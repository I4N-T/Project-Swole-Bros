using UnityEngine;
using System.Collections;

public class EnemyTurn : MonoBehaviour {

	private EnemyStats enemyStatsScript = new EnemyStats();

	int critVal;
	int actionVal;
    int knockDownVal;

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
		
        //if isknockdown = true, then 75% chance of
		
		if (playerHP > 0) {
			
			BattleStateMachine.currentState = BattleStateMachine.BattleState.PlayerTurn;
		} 
		else if (playerHP <= 0) {

			//Load Game Over screen
			Application.LoadLevel ("GameOver");

		}
	}
    public void ECheckStatus()
    {
        if (EnemyObjectDisable.instance2.enemyStatsScript.isKnockDown == true)
        {
            knockDownVal = GetRandomValueKnockDown();
            if (knockDownVal == 0)
            {
                EnemyObjectDisable.instance2.enemyStatsScript.isStillKnockDown = false;
            }
            else if (knockDownVal == 1)
            {
                EnemyObjectDisable.instance2.enemyStatsScript.isStillKnockDown = true;              
            }
        }
        else if (EnemyObjectDisable.instance2.enemyStatsScript.isKnockDown == false)
        {
            EnemyObjectDisable.instance2.enemyStatsScript.isStillKnockDown = false;
        }
    }

    public void EChangeStatus()
    {
        if (EnemyObjectDisable.instance2.enemyStatsScript.isStillKnockDown == false)
        {
            EnemyObjectDisable.instance2.enemyStatsScript.isKnockDown = false;
        }
        else if (EnemyObjectDisable.instance2.enemyStatsScript.isStillKnockDown == true)
        {
            EnemyObjectDisable.instance2.enemyStatsScript.isKnockDown = true;
        }
    }

    int GetRandomValueKnockDown()
    {
        float rand2 = Random.value;
        if (rand2 <= .75f)
            return 0;

        return 1;
    }

}
