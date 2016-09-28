using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class BattleStateMachine : MonoBehaviour {

	bool isplayerturn;
	bool isvictorious;
	bool iscrit;
	bool showresults = false;
	bool enemyVanquished = false;
	bool nextButtonBool = false;
	bool showReward = false;
	bool shownReward = false;


	public bool eShowResults = false;

	public static bool sceneActive = true;

	//GameObject sceneObject = GameObject.Find("EnemyFightSceneObject");


	public int eHitPoints;
	public string eName;
	

	private BattleLog eventLog;

	private bool attackBtn;
	private string attackResults;

	private bool abWeakPunchBtn;
	private bool nextBtn;
	private bool fleeBtn;

	int counter = 1;

	int goldGained = 0;

	float randFlee;


	private PreBattle preBattleScript = new PreBattle();
	private PlayerTurn playerTurnScript = new PlayerTurn();
	private OnAttack onAttackScript1 = new OnAttack();
	private EnemyTurn enemyTurnScript = new EnemyTurn();

	//ability scripts
	private AbWeakPunch abWeakPunchScript = new AbWeakPunch();
	
		
	public enum BattleState {
			PreBattle,
			PlayerTurn,
			Calculate,
			EnemyTurn,
			PostBattleWin
		}

	public static BattleState currentState;

	// Use this for initialization
	void Start(){

		nextButtonBool = false;
		sceneActive = true;

		currentState = BattleState.PreBattle;
		eventLog = GetComponent<BattleLog>();


	
	}
	
	// Update is called once per frame
	void Update () {


		//Debug.Log (abWeakPunchScript.insufficientCalBool);
		//Debug.Log (currentState);


		switch(currentState){
		case(BattleState.PreBattle):
		
			Debug.Log(GameManager.instance.playerSpeed);

			//set enemy name
			eName = EnemyObjectDisable.instance2.enemyStatsScript.eName;

			//Check player and enemy speeds to see who goes first
			preBattleScript.PrepareBattle();
			Debug.Log(preBattleScript.enemySpeed);
			Debug.Log(currentState);
		
			break;

		case(BattleState.PlayerTurn):

			//create or define Attack button
			//if Attack button is selected, then attack
			OnGUI();

			//check if enemy hp <= 0
			//change to EnemyTurn if still alive
			//change to postBattle (win) if dead



			break;

		case(BattleState.Calculate):

			//

			break;

		case(BattleState.EnemyTurn):

			EnemyAttackMethod();

			// update battle log with enemy's move
			//OnGUI();
			
			break;

		case(BattleState.PostBattleWin):

			//OnGUI();
			//enemyVanquished = PlayerTurn.instance.enemyVanquished;

			//update battle log
			if(enemyVanquished == true){
			eventLog.AddEvent("You have vanquished the " + eName + "!");
				enemyVanquished = false;
				//sceneActive = false;
				nextButtonBool = true;

			}

			//Award loot to the player

			//Return to Main Scene
			//enemyFightEnablerScript.EnableFalse();
			
			break;
	
	}
}
	public void OnGUI(){

		if (currentState == BattleState.PostBattleWin && nextButtonBool == true) {
			//Next button (press to see reward)
			PostBattleNextBtn();



		}

		////////// PLAYER TURN ////////////
		if (currentState == BattleState.PlayerTurn) {
			//attack button function
			//AttackButton ();
			AbWeakPunchButton();
			FleeButton();
		}
		if (showresults == true && abWeakPunchScript.iscrit == true && abWeakPunchScript.insufficientCalBool == false) {
			eventLog.AddEvent ("It was a critical hit! " + "You attacked the " + eName + " for " + abWeakPunchScript.damageToDo + " points of damage.");
			showresults = false;
		}
		if (showresults == true && abWeakPunchScript.iscrit == false && abWeakPunchScript.insufficientCalBool == false) {
			eventLog.AddEvent ("You attacked the " + eName + " for " + abWeakPunchScript.damageToDo + " points of damage.");
			showresults = false;
		}
		if (showresults == true && abWeakPunchScript.insufficientCalBool == true) {
			eventLog.AddEvent ("Low calorie warning. You need to eat more to use this ability.");
			showresults = false;
			abWeakPunchScript.insufficientCalBool = false;

		}

		///////////// ENEMY TURN ////////////////
		//if (currentState == BattleState.EnemyTurn) {
		
			if (eShowResults == true && enemyTurnScript.isAction == true && enemyTurnScript.iscrit == true) {
				eventLog.AddEvent ("OUCH! " + "The " + eName + " attacked you and dealt " + enemyTurnScript.eDamageToDo + " points of damage.");
				eShowResults = false;
			}
			if (eShowResults == true && enemyTurnScript.isAction == true && enemyTurnScript.iscrit == false) {
				eventLog.AddEvent ("The " + eName + " attacked you and dealt " + enemyTurnScript.eDamageToDo + " points of damage.");
				eShowResults = false;
			}
			if (eShowResults == true && enemyTurnScript.isAction == false) {
				eventLog.AddEvent ("The " + eName + " is lolli-gagging around.");
				eShowResults = false;
			}

		}
	

		//}

		////////// POST BATTLE WIN  ////////////
		//if (currentState == BattleState.PostBattleWin) {


		//}


	void AttackButton(){


		attackBtn = GUI.Button (new Rect (0,0,150,50), "Attack");
		if (attackBtn) {
			PlayerAttackMethod();
			showresults = true;
			playerTurnScript.CheckandSwitch();
	
		}

	}
	

	public void PlayerAttackMethod(){

		onAttackScript1.PlayerAttackMethod ();  

		//playerTurnScript.OnAttk ();  THIS WAY DOES NOT WORK!!!


		//damageDealt = OnAttack.damageToDO;

		//eHitPoints = enemyStatsScript.eHitPoints;
		//eName = enemyStatsScript.eName;

		Debug.Log (onAttackScript1.iscrit);
		Debug.Log (onAttackScript1.damageToDo);
		Debug.Log (EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints);

	}

	public void EnemyAttackMethod(){

		eShowResults = true;
		enemyTurnScript.EDamageMethod ();
		enemyTurnScript.ECheckandSwitch ();
		OnGUI();

		Debug.Log ("eshowresults is " + eShowResults);
		Debug.Log ("isAction is " + enemyTurnScript.isAction);
		Debug.Log (enemyTurnScript.eDamageToDo);
		Debug.Log ("player hp is " + GameManager.instance.playerHP);
	}

	////BUTTONS////
	void PostBattleNextBtn(){


		nextBtn = GUI.Button (new Rect (160, 0, 150, 50), "Proceed");

		if (counter == 1) {

			if (nextBtn) {
				//calculate reward
				CalculateReward.CalcReward();
				goldGained = CalculateReward.goldToGive;
				eventLog.AddEvent ("You gained " + goldGained + " gold pieces!");
				GameManager.instance.playerGold += goldGained;
				counter = 2;
			}
		}
		else if (counter == 2) {

			if (nextBtn){
				counter = 1;
				nextButtonBool = false;
				sceneActive = false;
			}
		}
			}

	void AbWeakPunchButton(){
		
		
		abWeakPunchBtn = GUI.Button (new Rect (0,0,150,50), "Weak Punch");
		if (abWeakPunchBtn) {
			UseWeakPunchMethod();
			showresults = true;
			CheckandSwitch();
		}
		
	}

	void FleeButton(){

		fleeBtn = GUI.Button (new Rect (0, 60, 150, 50), "Flee Battle");
		if (fleeBtn) {
			FleeMethod();
		}

	}
	
	
	public void UseWeakPunchMethod(){
		
		abWeakPunchScript.UseWeakPunch();
		
		//playerTurnScript.OnAttk ();  THIS WAY DOES NOT WORK!!!
		
		
		//damageDealt = OnAttack.damageToDO;
		
		//eHitPoints = enemyStatsScript.eHitPoints;
		//eName = enemyStatsScript.eName;
		
		Debug.Log (abWeakPunchScript.iscrit);
		Debug.Log (abWeakPunchScript.damageToDo);
		Debug.Log (EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints);
		
	}

	public void FleeMethod(){
		randFlee = Random.Range (0, 2);

		if (randFlee != 1) {
			GameManager.pauseBool = false;
			sceneActive = false;
		} else if (randFlee == 1) {
			showresults = true;
			if (showresults == true) {
				eventLog.AddEvent ("You attempted to flee like a scared little bitch. Too bad. It didn't work.");
				showresults = false;
				currentState = BattleState.EnemyTurn;
			}
		}
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
			currentState = BattleState.PostBattleWin;
		}
		if (eHitPoints > 0 && abWeakPunchScript.insufficientCalBool == false) {
			
			currentState = BattleState.EnemyTurn;
			
		}
	}



}



	

