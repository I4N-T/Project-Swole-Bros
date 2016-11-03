using UnityEngine;
using System.Collections;

public class TimerTest : MonoBehaviour {

    public int time;
    public int timeLeft;

	public int timeWait;
	public int timeLeftWait;

	private DelegateManager delegateManagerScript;

    // Use this for initialization
    void Start()
    {
		delegateManagerScript = this.gameObject.GetComponent<DelegateManager>();
    }

    void OnGUI()
    {
        /*if (IncGameManager.workoutHappening == true)
        {
            GUI.Label(new Rect(Screen.width * 0.07f, Screen.height * 0.25f, Screen.width * 0.25f, Screen.height * 0.1f), "Workout over in: " + timeLeft + " seconds.");
        }*/
    }

    public IEnumerator Countdown(int time)
    {
        timeLeft = time;
        IncGameManager.workoutHappening = true;
        while (time > 0)
        {
            Debug.Log(time--);
            yield return new WaitForSeconds(1);
            timeLeft = time;
        }
        IncGameManager.workoutHappening = false;
        Debug.Log("Countdown Complete!");
    }

	public IEnumerator WaitAfterTurn(int timeWait)
	{
		timeLeftWait = timeWait;
		//BattleStateMachine.waitingNow = true;
		while (timeWait > 0)
		{
			Debug.Log(timeWait--);
			yield return new WaitForSeconds(1);
			timeLeftWait = timeWait;
		}
		Debug.Log ("made it");
		//BattleStateMachine.waitingNow = false;
		delegateManagerScript.TriggerEvent();
	}

    // Update is called once per frame
    void Update () {
        
	}
}
