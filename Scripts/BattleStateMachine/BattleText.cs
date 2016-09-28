using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BattleText : MonoBehaviour {

	private EnemyStats enemyStatsScript = new EnemyStats();

	private int hitPoints;                           //Used to store player food points total during level. FOOD IS HP.
	private int strength;
	private int speed;

	private int eHitPoints;

	public Text healthText;
	private string hpText;

	public Text eHealthText;
	private string eHPText;



	// Use this for initialization
	 public void Start () {

		hitPoints = GameManager.instance.playerHP;
		speed = GameManager.instance.playerSpeed;
		strength = GameManager.instance.playerStrength;
		eHitPoints = EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints;

		hpText = GameManager.instance.playerHP.ToString();
		eHPText = EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints.ToString ();
	
		healthText.text = "HP: " + hpText;
		eHealthText.text = "HP: " + eHPText;

	}
	
	// Update is called once per frame
	void Update () {

		hpText = GameManager.instance.playerHP.ToString();
		eHPText = EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints.ToString ();
	
		healthText.text = "HP: " + hpText;
		eHealthText.text = "HP: " + eHPText;

	}

}
