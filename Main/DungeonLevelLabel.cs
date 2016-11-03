using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DungeonLevelLabel : MonoBehaviour {

	public int gymLevel;
    private bool incGameBtn;

	public int playerHP;
	public int playerCalories;


	void OnGUI(){
        //if enemyfightsceneobject does not exist
		GUI.Label (new Rect (0, 0, Screen.width * 0.47f, Screen.height * 0.1f), "Current Floor: " + gymLevel);
		GUI.Label (new Rect (Screen.width * 0.8f, Screen.height * 0.1f, Screen.width * 0.47f, Screen.height * 0.1f), "Player HP: " + playerHP);
		GUI.Label (new Rect (Screen.width * 0.8f, Screen.height * 0.2f, Screen.width * 0.47f, Screen.height * 0.1f), "Player Calories: " + playerCalories);
        IncGameButton();


	}
	// Use this for initialization
	void Start () {
			gymLevel = GameManager.instance.gymLevel;
		playerHP = GameManager.instance.playerHP;
		playerCalories = GameManager.instance.playerCalories;
	
	}
	
	// Update is called once per frame
	void Update () {
		gymLevel = GameManager.instance.gymLevel;
		playerHP = GameManager.instance.playerHP;
		playerCalories = GameManager.instance.playerCalories;
	}

    void IncGameButton()
    {

        incGameBtn = GUI.Button(new Rect(500, 0, 150, 50), "Return to home gym");
        if (incGameBtn)
        {
            SoundManager.instance.mainMusicSource.Stop();
            SoundManager.instance.battleMusicSource.Stop();
            SoundManager.instance.gymMusicSource.Play();
            SceneManager.LoadScene(4);
            GameManager.instance.gymLevel = 0;
        }
    }
}
