using UnityEngine;
using System.Collections;

public class EnemyObjectDisable : MonoBehaviour {

	public EnemyStats enemyStatsScript;

	public static EnemyObjectDisable instance2 = null;

	public int eHitPoints;
	public int eStrength;
	public int eSpeed;

    //public string enemyType;

	
	void Awake(){

		enemyStatsScript =  this.transform.parent.GetComponent<EnemyStats> (); //this allows this script to change enemystats variables

        //enemyType = this.transform.parent.name;
	}
	

	private void OnTriggerEnter2D (Collider2D other){
	
		if (other.tag == "Player") {

			//if(MyPlayer.enemyObjectDisable == true){
			// remove the Enemy object from playing field, while keeping scripts active
			this.transform.parent.localScale = new Vector3 (0, 0, 0);

			//set instance
			InstanceMethod();

			//Pause physics
			GameManager.pauseBool = true;

			////////stuff that used to be in MyPlayer/////////
			/// Load EnemyFight scene, additive
			Application.LoadLevelAdditive("EnemyFight");

            //Stop main music and play battle music
            SoundManager.instance.mainMusicSource.Stop();
            SoundManager.instance.gymMusicSource.Stop();
           // SoundManager.instance.battleMusicSource.Play();



			//Once enemy is dead, set instance2 = null
			//if(instance2.enemyStatsScript.eHitPoints <= 0){
				//instance2 = null;
			//}

				//MyPlayer.enemyObjectDisable = false;
		

			//set enemy stats local to this script
			//eHitPoints = EnemyStats.instance1.eHitPoints;
			//eStrength = EnemyStats.instance1.eStrength;
			//eSpeed = EnemyStats.instance1.eSpeed;

			//}
	}
}



	public void InstanceMethod(){
	
		if (instance2 == null) {
			
			//if not, set instance to this
			instance2 = this;
		} else if (instance2 != this) {

			instance2 = this;
		}
	}



}
