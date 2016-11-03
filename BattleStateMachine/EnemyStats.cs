using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

	public static EnemyStats instance1;

    //public int eIndexVar = 0;
	public int eHitPoints = 12;
	public int eStrength = 1;
	public int eSpeed = 1;

	public string eName = "Alien Goo";
    public string eIdleText = "is lolli-gagging around.";

    //STATUS EFFECTS
    public bool isKnockDown = false;
    public bool isStillKnockDown = false;


    void Awake()
	{
		//Check if instance already exists
		//if (instance1 == null)
			
			//if not, set instance to this
			//instance1 = this;
		
		//If instance already exists and it's not this:
		//else if (instance1 != this)
			
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			//Destroy (gameObject);    
	}

	// Use this for initialization
	void Start () {
		//GameManager.instance.enemyHP = eHitPoints;
		//GameManager.instance.enemyStrength = eStrength;
		//GameManager.instance.enemySpeed = eSpeed;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (eHitPoints == 0) {
			//GameManager.pauseBool = false;
			gameObject.SetActive(false);
		}
	}
}
