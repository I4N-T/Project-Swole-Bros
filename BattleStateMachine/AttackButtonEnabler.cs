using UnityEngine;
using System.Collections;

public class AttackButtonEnabler : BattleStateMachine {



	// Update is called once per frame
	void ButtonFunction () {
	
		if (currentState == BattleState.PlayerTurn) {
			gameObject.SetActive (true);
		} else if (currentState != BattleState.PlayerTurn) {
			gameObject.SetActive (false);
		}
	}
}
