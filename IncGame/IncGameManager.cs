using UnityEngine;
using System.Collections;

public class IncGameManager : MonoBehaviour {

    
    public static int playerGold;

    public static float benchMax = 0f;
	public static float squatMax = 0f;
	public static float deadMax = 0f;

    //Inc game items
	bool haveDungeonKey = false;
    public static bool hasWorkoutBench = false;
    public static bool hasSquatRack = false;

    //TIMER STUFF
    public static bool workoutHappening = false;

    private bool workedOutToday = false;

    private bool beginWorkoutBtn;
    private bool pushUpBtn;
    private bool squatBtn;
	private bool dungeonBtn;
	private bool cannotEnterCloseBtn;

	private bool cannotEnterLabelBool = false;

    private TimerTest timerTestScript;
    private int timeHere;


	// Use this for initialization
	void Start ()
    {
       timerTestScript = this.gameObject.GetComponent<TimerTest>();
    }
	
	// Update is called once per frame
	void Update () {
        timeHere = timerTestScript.timeLeft;

        Debug.Log(timeHere);
		//round floats for proper display
		benchMax = Mathf.Round(benchMax * 100f) / 100f;
        squatMax = Mathf.Round(squatMax * 100f) / 100f;


        //set dungeon key to be received after benchmax = whatever
        DungeonKeyInitialGet ();
	}

	void OnGUI(){
		DungeonButton ();
        //LABELS
		BenchMaxLabel ();
        SquatMaxLabel();

		if (cannotEnterLabelBool == true)
		{
			CannotEnterCloseButton ();
		}
        //WORKOUT BUTTONS
        //if (workedOutToday == false){
        if (workoutHappening == false)
        {
            BeginWorkoutButton();
        }
        if (workoutHappening == true)
        {
            WorkoutTimerLabel();
            PushUpButton();
            SquatButton();
        }
    }

	//Workout timer stuff
    void BeginWorkoutButton()
    {
        beginWorkoutBtn = GUI.Button(new Rect(Screen.width * 0.07f, Screen.height * 0.5f, Screen.width * 0.23f, Screen.height * 0.1f), "Begin Workout");
        if (beginWorkoutBtn)
        {
            StartCoroutine(timerTestScript.Countdown(10));
            //StartCoroutine(this.gameObject.GetComponent<TimerTest>().Countdown(10));
        }
    }
    void WorkoutTimerLabel()
    {
        GUI.Label(new Rect(Screen.width * 0.07f, Screen.height * 0.1f, Screen.width * 0.25f, Screen.height * 0.1f), "Workout over in: " + timeHere + " seconds.");
    }


    //Bench related things
    void PushUpButton(){

        if (hasWorkoutBench == false)
        {
            pushUpBtn = GUI.Button(new Rect (Screen.width * 0.07f, Screen.height * 0.25f, Screen.width * 0.23f, Screen.height * 0.1f), "Push-Up");
            if (pushUpBtn)
            {
                benchMax += 0.01f;
            }
        }
        if (hasWorkoutBench == true)
        {
            pushUpBtn = GUI.Button(new Rect (Screen.width * 0.07f, Screen.height * 0.25f, Screen.width * 0.23f, Screen.height * 0.1f), "Bench Press");
            if (pushUpBtn)
            {
                benchMax += 0.1f;
            }
        }
    }
		

	 void BenchMaxLabel(){

		GUI.Label (new Rect (Screen.width * 0.07f, Screen.height * 0.825f, Screen.width * 0.25f, Screen.height * 0.1f), "Max Bench Press: " + benchMax);
	}

    //SQUAT RELATED THINGS
    void SquatButton()
    {

        if (hasSquatRack == true)
        {
            squatBtn = GUI.Button(new Rect (Screen.width * 0.07f, Screen.height * 0.365f, Screen.width * 0.23f, Screen.height * 0.1f), "Squat");
            if (squatBtn)
            {
                squatMax += 0.1f;
            }
        }
    }


    void SquatMaxLabel()
    {
        if (hasSquatRack == true)
        {
            GUI.Label(new Rect(Screen.width * 0.07f, Screen.height * 0.875f, Screen.width * 0.25f, Screen.height * 0.1f), "Max Squat: " + squatMax);
        }
    }

    //door to the dungeon
    void DungeonButton(){

		dungeonBtn = GUI.Button (new Rect (0, 0, Screen.width * 0.23f, Screen.height * 0.1f), "Enter the Dungeon");
		if (dungeonBtn) {
			if (haveDungeonKey == true) {
				haveDungeonKey = false;
				SoundManager.instance.gymMusicSource.Stop ();
				SoundManager.instance.mainMusicSource.Play ();
				Application.LoadLevel ("Main");
			} else if (haveDungeonKey == false) 
			{
				cannotEnterLabelBool = true;
			}
		}
	}

	void CannotEnterCloseButton()
	{
		cannotEnterCloseBtn = GUI.Button(new Rect(Screen.width * 0f, Screen.height * 0.1f, Screen.width * 0.4f, Screen.height * 0.1f), "Cannot enter dungeon at this time.");
		if (cannotEnterCloseBtn) {
			cannotEnterLabelBool = false;
		}
	}

	void DungeonKeyInitialGet(){
		if (benchMax >= 0.1f) {
			haveDungeonKey = true;
		}
	}

}
