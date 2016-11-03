using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



using System.Collections.Generic;       //Allows us to use Lists. 
	
	public class GameManager : MonoBehaviour
	{
	
		
		public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
		private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.


    //player stats
    public int playerHP = 20;
	public int playerStrength = 5;
	public int playerSpeed = 5;
	public int playerCalories = 2000;
	public int playerMoveSpeed = 5;
    public int playerGold = 0;
	
		


	public int gymLevel = 0;                                  //Current level number, expressed in game as "Day 1".
	public static bool pauseBool = false;

    //player inventory
    public static bool hasProteinShake = false;




		
		//Awake is always called before any Start functions
		void Awake()
		{
			//Check if instance already exists
			if (instance == null)
				
				//if not, set instance to this
				instance = this;
			
			//If instance already exists and it's not this:
			else if (instance != this)
				
				//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
				Destroy(gameObject);    
			
			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad(gameObject);
			
			//Get a component reference to the attached BoardManager script
			boardScript = GetComponent<BoardManager>();
			
			//Call the InitGame function to initialize the first level 
			//InitGame();
		}

	void Update(){

		PauseKeyMethod ();
		PauseMethod ();

		StatsUpdate ();
	}
	

	//This is called each time a scene is loaded.
	void OnLevelWasLoaded(int index)
	{
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Main") {
			//Add one to our level number.
			gymLevel++;
			//Call InitGame to initialize our level.
			InitGame ();
		}
	}
		
		//Initializes the game for each level.
		void InitGame()
		{
			//Call the SetupScene function of the BoardManager script, pass it current level number.
			boardScript.SetupScene(gymLevel);

			
		}

		public void GameOver()
		{
			enabled = false;
		}

	public void PauseKeyMethod(){
		//uses the p button to pause and unpause the game
		if (Input.GetKeyDown (KeyCode.P)) {
			if (Time.timeScale == 1) {
				pauseBool = true;
				//showPaused ();
			} else if (Time.timeScale == 0) {
				pauseBool = false;
				//hidePaused ();
			}
		}
	}

	public void PauseMethod(){
		if (pauseBool == true) {
			Time.timeScale = 0;
			//showPaused ();
		}
			else if (pauseBool == false){
				Time.timeScale = 1;
			}
		}
		
	void StatsUpdate(){
		playerStrength = (int)IncGameManager.benchMax;
	}

		

	}
