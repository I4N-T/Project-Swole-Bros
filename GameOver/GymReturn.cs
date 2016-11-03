using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GymReturn : MonoBehaviour {

	private bool incGameBtn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		IncGameButton ();
	}

	void IncGameButton()
	{

		incGameBtn = GUI.Button(new Rect(500, 0, 150, 50), "Return to home gym");
		if (incGameBtn)
		{
			GameManager.instance.playerHP = 20;
			GameManager.instance.playerCalories = 2000;

			GameManager.pauseBool = false;
			SoundManager.instance.mainMusicSource.Stop();
			SoundManager.instance.battleMusicSource.Stop();
			SoundManager.instance.gymMusicSource.Play();
			SceneManager.LoadScene(4);
			GameManager.instance.gymLevel = 0;
		}
	}
}
