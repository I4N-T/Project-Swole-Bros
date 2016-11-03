using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class BattleStateMachine : MonoBehaviour {

	bool isplayerturn;
	bool isvictorious;
	bool iscrit;
	bool showresults = false;
    bool showresults1 = false;
    bool showresults2 = false;
	bool enemyVanquished = false;
	bool nextButtonBool = false;
	bool showReward = false;
	bool shownReward = false;

	public static bool waitingNow = false;
	//public int timeWait;
	//public int timeLeftWait;


	public bool eShowResults = false;
    public bool eShowKnockDownResults = false;

	public static bool sceneActive = true;

	//GameObject sceneObject = GameObject.Find("EnemyFightSceneObject");


	public int eHitPoints;
	public string eName;
    public string eIdleText;
	

	private BattleLog eventLog;

	private bool attackBtn;
	private string attackResults;

	private bool abWeakPunchBtn;
    private bool abShoveBtn;
	private bool nextBtn;
	private bool fleeBtn;
    private bool useItemBtn;

	int counter = 1;

	int goldGained = 0;

	float randFlee;


	private PreBattle preBattleScript = new PreBattle();
	private PlayerTurn playerTurnScript = new PlayerTurn();
	private OnAttack onAttackScript1 = new OnAttack();
	private EnemyTurn enemyTurnScript = new EnemyTurn();

	private DelegateManager delegateManagerScript;

	//ability scripts
	private AbWeakPunch abWeakPunchScript = new AbWeakPunch();
    private AbShove abShoveScript = new AbShove();

    //item scripts
    private UseProteinShake useProteinShakeScript = new UseProteinShake();


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
		DelegateManager.EventContainerOne += ChangeBattleState;
		delegateManagerScript = this.gameObject.GetComponent<DelegateManager>();


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

            //set enemy idle message
            eIdleText = EnemyObjectDisable.instance2.enemyStatsScript.eIdleText;

			//Check player and enemy speeds to see who goes first
			preBattleScript.PrepareBattle();
			Debug.Log(preBattleScript.enemySpeed);
			Debug.Log(currentState);
		
			break;

		case(BattleState.PlayerTurn):

			//create or define Attack button
			//if Attack button is selected, then attack
			//OnGUI();

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
		
			//AbWeakPunchButton();
			FleeButton();

			if (IncGameManager.benchMax >= 0.1f) 
			{
				AbWeakPunchButton();
			}

            
            if (IncGameManager.benchMax >= 2 && IncGameManager.squatMax >= 2)
            {
                AbWeakShoveButton();
            }

            if (GameManager.hasProteinShake == true)
            {
                UseItemButton();
            }
        }

        //WEAK PUNCH
		if (showresults1 == true && abWeakPunchScript.iscrit == true && abWeakPunchScript.insufficientCalBool == false) {
			eventLog.AddEvent ("It was a critical hit! " + "You attacked the " + eName + " for " + abWeakPunchScript.damageToDo + " points of damage.");
			showresults1 = false;
		}
		if (showresults1 == true && abWeakPunchScript.iscrit == false && abWeakPunchScript.insufficientCalBool == false) {
			eventLog.AddEvent ("You attacked the " + eName + " for " + abWeakPunchScript.damageToDo + " points of damage.");
			showresults1 = false;
		}
		if (showresults1 == true && abWeakPunchScript.insufficientCalBool == true) {
			eventLog.AddEvent ("Low calorie warning. You need to eat more to use this ability.");
			showresults1 = false;
			abWeakPunchScript.insufficientCalBool = false;
		}
        //SHOVE
        if (showresults2 == true && abShoveScript.iscrit == true && abShoveScript.insufficientCalBool == false)
        {
            eventLog.AddEvent("It was a critical hit! " + "You shoved the " + eName + " for " + abShoveScript.damageToDo + " points of damage.");
            showresults2 = false;
        }
        if (showresults2 == true && abShoveScript.iscrit == false && abShoveScript.insufficientCalBool == false)
        {
            eventLog.AddEvent("You shoved the " + eName + " for " + abShoveScript.damageToDo + " points of damage.");
            showresults2 = false;
        }
        if (showresults2 == true && abShoveScript.insufficientCalBool == true)
        {
            eventLog.AddEvent("Low calorie warning. You need to eat more to use this ability.");
            showresults2 = false;
            abShoveScript.insufficientCalBool = false;
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
       /* if (eShowResults == true && enemyTurnScript.isAction == false && EnemyObjectDisable.instance2.enemyStatsScript.isKnockDown == true)
        {
            eventLog.AddEvent("The " + eName + "is knocked down and cannot attack.");
            eShowResults = false;
        }*/

        if (eShowResults == true && enemyTurnScript.isAction == false && eShowKnockDownResults == false)
        {
				eventLog.AddEvent ("The " + eName + eIdleText);
				eShowResults = false;     
		}
        if (eShowResults == true && enemyTurnScript.isAction == false && eShowKnockDownResults == true)
        {
            eventLog.AddEvent("The " + eName + "is knocked down and cannot attack.");
            eShowResults = false;
            eShowKnockDownResults = false;
        }
    }
	

		//}

		////////// POST BATTLE WIN  ////////////
		//if (currentState == BattleState.PostBattleWin) {


		//}


	/*void AttackButton(){


		attackBtn = GUI.Button (new Rect (0,0,150,50), "Attack");
		if (attackBtn) {
			PlayerAttackMethod();
			showresults = true;
			playerTurnScript.CheckandSwitch();
	
		}

	}*/
	

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

        Debug.Log("enemy knockdown" + EnemyObjectDisable.instance2.enemyStatsScript.isKnockDown);
		//eShowResults = true;
        if (EnemyObjectDisable.instance2.enemyStatsScript.isKnockDown == false)
        {
            eShowResults = true;
            enemyTurnScript.EDamageMethod();
            enemyTurnScript.ECheckandSwitch();
        }
        else if (EnemyObjectDisable.instance2.enemyStatsScript.isKnockDown == true)
        {
            eShowKnockDownResults = true;
            eShowResults = true;
            
            //set isaction to false to avoid the battle Log saying the wrong thing
            enemyTurnScript.isAction = false;

            enemyTurnScript.ECheckStatus();
            Debug.Log("enemy knockdown mid " + EnemyObjectDisable.instance2.enemyStatsScript.isKnockDown);
            enemyTurnScript.ECheckandSwitch();
            enemyTurnScript.EChangeStatus();
           // Debug.Log("enemy knockdown after " + EnemyObjectDisable.instance2.enemyStatsScript.isKnockDown); //this proved that the problem with battle Log is here
            //check if (isStillKnockedDown = false) then 
            // EnemyObjectDisable.instance2.enemyStatsScript.isKnockDown = false;
        }

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
                GameManager.pauseBool = false;
				sceneActive = false;
			}
		}
			}

    //MAKE ATTACK/ABILITIES BUTTON THAT PULLS OUT WITH MULTIPLE OPTIONS
	void AbWeakPunchButton(){

		abWeakPunchBtn = GUI.Button (new Rect (0, 0, Screen.width * 0.23f, Screen.height * 0.1f), "Weak Punch");
		if (abWeakPunchBtn) {
			UseWeakPunchMethod();
			showresults1 = true;
			CheckandSwitch();
		}	
	}

    void AbWeakShoveButton()
    {

        abShoveBtn = GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.5f, Screen.width * 0.23f, Screen.height * 0.1f), "Shove");
        if (abShoveBtn)
        {
            UseShoveMethod();
            showresults2 = true;
            CheckandSwitch();
        }
    }

    void FleeButton(){

		fleeBtn = GUI.Button (new Rect (0, Screen.height * 0.105f, Screen.width * 0.23f, Screen.height * 0.1f), "Flee Battle");
		if (fleeBtn) {
			FleeMethod();
		}

	}

    void UseItemButton()
    {

        useItemBtn = GUI.Button(new Rect(0, Screen.height * 0.21f, Screen.width * 0.23f, Screen.height * 0.1f), "Use Item");
        if (useItemBtn)
        {
           useProteinShakeScript.UseProteinShakeMethod();
           eventLog.AddEvent("You drank a Protein Shake. Man, that really hit the spot! You gained 1000 calories.");
           currentState = BattleState.EnemyTurn;
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

    public void UseShoveMethod()
    {

        abShoveScript.UseShove();

        //playerTurnScript.OnAttk ();  THIS WAY DOES NOT WORK!!!


        //damageDealt = OnAttack.damageToDO;

        //eHitPoints = enemyStatsScript.eHitPoints;
        //eName = enemyStatsScript.eName;

        Debug.Log("Critical" + abShoveScript.iscrit);
        Debug.Log("Knockdown" + abShoveScript.isKnockDown);
        Debug.Log(abShoveScript.damageToDo);
        Debug.Log(EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints);
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
				StartCoroutine (WaitAfterTurn ());

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

			//wait 1 second
			StartCoroutine(WaitAfterTurn());

				//currentState = BattleState.EnemyTurn;	
		}
	}

	public void ChangeBattleState()
	{
		Debug.Log ("event triggered");
		currentState = BattleState.EnemyTurn;
	}

	 IEnumerator WaitAfterTurn()
	{
		
		yield return new WaitForSecondsRealtime (1);
		delegateManagerScript.TriggerEvent ();
	}
	/*public IEnumerator WaitAfterTurn(int timeWait)
	{
		timeLeftWait = timeWait;
		//BattleStateMachine.waitingNow = true;
		while (timeWait > 0)
		{
			//Debug.Log(timeWait--);
			yield return new WaitForSeconds(1);
			timeLeftWait = timeWait;
		}
		Debug.Log("made it this far");
		//BattleStateMachine.waitingNow = false;
		//delegateManagerScript.TriggerEvent();
	}*/



}



	

