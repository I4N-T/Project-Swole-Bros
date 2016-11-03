using UnityEngine;
using System.Collections;

public class EnemyFightEnabler: MonoBehaviour {

	//private BattleStateMachine battleStateMachineScript = new BattleStateMachine();

	bool sceneAtiveHere;

	//GameObject enemyFightEnabler;


	void Update () {

		EnableFalse ();

	}

	public void EnableFalse() {

		sceneAtiveHere = BattleStateMachine.sceneActive;

		if (/*BattleStateMachine.currentState == BattleStateMachine.BattleState.PostBattleWin &&*/ sceneAtiveHere == false) {
			Func();
	
		}
	}

		
	

	public void Func() {

        //Debug.Log (GameManager.playerHP);
        SoundManager.instance.battleMusicSource.Stop();
        SoundManager.instance.mainMusicSource.Play();
		Destroy(gameObject);
	
	}

}



