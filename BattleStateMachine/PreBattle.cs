using UnityEngine;
using System.Collections;

public class PreBattle : MonoBehaviour {


	private EnemyObjectDisable enemyObjectScript;
	private EnemyStats enemyStatsScript = new EnemyStats();

	private int enemyHitPoints;
	private int enemyStrength;
	public int enemySpeed;


	public void PrepareBattle(){
		//select random enemy from list (for now just chooses the one enemy)
		enemyHitPoints = EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints;
		enemyStrength = EnemyObjectDisable.instance2.enemyStatsScript.eStrength;
		enemySpeed = EnemyObjectDisable.instance2.enemyStatsScript.eSpeed;
	
		//Check player and enemy speeds to see who goes first
		CompareSpeeds ();
	}


	public void CompareSpeeds(){
		
		if (GameManager.instance.playerSpeed <= enemySpeed) {
			BattleStateMachine.currentState = BattleStateMachine.BattleState.EnemyTurn;
		} else if (GameManager.instance.playerSpeed > enemySpeed) {
			BattleStateMachine.currentState = BattleStateMachine.BattleState.PlayerTurn;
		}
	}
}
