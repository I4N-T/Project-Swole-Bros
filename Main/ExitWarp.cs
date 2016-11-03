using UnityEngine;
using System.Collections;

public class EnemyFight : MonoBehaviour {

	public float restartLevelDelay = 1f;        //Delay time in seconds to restart level.

	public void Start(){

		//gameObject.AddComponent (Type.GetType ("ExitWarp"));

	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		//Check if the tag of the trigger collided with is Exit.
		if(other.tag == "Enemy")
		{
			//Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
			Invoke ("Restart", restartLevelDelay);
			
			//Disable the player object since level is over.
			enabled = false;
		}

}
}
