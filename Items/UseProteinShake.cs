using UnityEngine;
using System.Collections;

public class UseProteinShake : MonoBehaviour {

    private BattleLog eventLog;

    public void UseProteinShakeMethod()
    {

        GameManager.hasProteinShake = false;
        GameManager.instance.playerCalories += 1000;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
