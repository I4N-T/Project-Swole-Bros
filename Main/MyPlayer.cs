using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class MyPlayer : MyMove {
	

	public float restartLevelDelay = 1f;        //Delay time in seconds to restart level.

	//ITEM EFFECTS
	public int caloriesAdded = 50;
	public int pointsPerHPBar = 1;              //Number of points to add to player food points when picking up a food object.
	public int pointsPerSoda = 20;              //Number of points to add to player food points when picking up a soda object.

    //SOUNDS
    public AudioClip itemGet;
	
	
	private int hitPoints;                           //Used to store player food points total during level. FOOD IS HP.
	private int strength;
	private int speed;
	private int calories;

	public static bool enemyObjectDisable = false;



	// Use this for initialization
	protected override void Start ()
	{
		
		//Get the current food point total stored in GameManager.instance between levels.
		hitPoints = GameManager.instance.playerHP;
		speed = GameManager.instance.playerSpeed;
		strength = GameManager.instance.playerStrength;
		
		//Call the Start function of the MovingObject base class.
		base.Start ();
	}

	//This function is called when the behaviour becomes disabled or inactive.
	private void OnDisable ()
	{
		//When Player object is disabled, store the current local food total in the GameManager so it can be re-loaded in next level.
		//GameManager.instance.playerHP = hitPoints;
		//GameManager.instance.playerSpeed = speed;
		//GameManager.instance.playerStrength = strength;
	}


	private void OnTriggerEnter2D (Collider2D other)
	{
		//Check if the tag of the trigger collided with is Exit.
		if(other.tag == "Exit")
		{
			//Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
			Invoke ("LoadNextFloor", restartLevelDelay);

			
			//Disable the player object since level is over.
			enabled = false;
		}

		//Check if the tag of the trigger collided with is Enemy
		/*else if(other.tag == "Enemy")
		{
			//Launch Battle Scene

			//Disable the object the player collided with.
			//other.gameObject.SetActive (false);

			//Set the enemyobjectdisable bool to true
			enemyObjectDisable = true;


			//Invoke ("EnemyFight", restartLevelDelay);

			//Application.LoadLevelAdditive("EnemyFight");

			//enabled = false;
		}*/

		//Check if the tag of the trigger collided with is Food.
		else if(other.tag == "Drugs")
		{
            SoundManager.instance.efxSource.clip = itemGet;
            SoundManager.instance.efxSource.Play();
            //Add pointsPerFood to the players current food total.
            pointsPerHPBar = (int)Random.Range(1, 5);
            hitPoints = pointsPerHPBar; 
			GameManager.instance.playerHP += hitPoints;

			
			//Disable the food object the player collided with.
			other.gameObject.SetActive (false);
		}

		
	}

	//Restart reloads the scene when called.
	private void LoadNextFloor ()
	{
		//Load the last scene loaded, in this case Main, the only scene in the game.
		Application.LoadLevel ("Main");
	}


}
