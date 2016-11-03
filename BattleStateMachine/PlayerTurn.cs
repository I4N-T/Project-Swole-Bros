using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerTurn : MonoBehaviour {

	private EnemyStats enemyStatsScript = new EnemyStats();

	public static PlayerTurn instance = null;

	private OnAttack onAttackScript = new OnAttack();

	public int eHitPoints;

	public static bool enemyVanquished = false;


	void Awake()
	{
		//Check if instance already exists
		if (instance == null)
			
			//if not, set instance to this
			instance = this;
		
		//If instance already exists and it's not this:
		else if (instance != this)
			
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject);    
	}


	// Use this for initialization
	void Start () {
		

	}


	public void PlayerTurnMethod(){

		//create or define Attack button

		//if Attack button is selected, then attack
		OnAttk ();
		/*Debug.Log (onAttackScript.iscrit);
		Debug.Log (onAttackScript.damageToDo);
		Debug.Log (EnemyStatsScript.eHitPoints);
		*/


	}

	public void OnAttk(){
		onAttackScript.PlayerAttackMethod ();
	}

	public void CheckandSwitch(){

		eHitPoints = EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints;

		//check if enemy hp <= 0
		//change to EnemyTurn if still alive
		//change to postBattle (win) if dead
		if (eHitPoints <= 0) {

			eHitPoints = 0;
			EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints = eHitPoints;
			enemyVanquished = true;
			BattleStateMachine.currentState = BattleStateMachine.BattleState.PostBattleWin;
		}
		if (eHitPoints > 0) {

			BattleStateMachine.currentState = BattleStateMachine.BattleState.EnemyTurn;
		
		}
	}



	

	
	// Update is called once per frame
	void Update () {
	


	}



	/*at end of turn
	GameManager.instance.playerHP = hitPoints;
	GameManager.instance.playerSpeed = speed;
	GameManager.instance.playerStrength = strength;
	*/
}
