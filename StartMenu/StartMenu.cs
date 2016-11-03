using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	private bool showGUI = false;
	private bool closeHowToBtn;

	string howToPlayString;

	void Start()
	{
		howToPlayString = "Use WASD to move.\n \n" +
			"When inside your home gym, click BEGIN WORKOUT to begin a 10 second workout. During this workout, try to do as many reps of each exercise" +
			" as possible. \n \n" +
			"You will learn a new ability when your max bench press and max squat are both high enough. You can use this ability in battle. Abilities cost CALORIES to perform. \n \n" +
			"Enter the Dungeon to engage Martian invaders in turn-based combat. \n \n" +
			"Collect protein bars in the Dungeon to recover your HP. \n \n" +
			"LET'S GET SWOLLLLLLLLLL BROOOOTTTHHHHHEEERRRRRR!!!";
	}

	void OnGUI()
	{
		if (showGUI == true)
		{
		HowToBox();
		CloseHowToBox();
		}
	}


	public void PlayGameBtn()
    {
        SoundManager.instance.mainMusicSource.Stop();
        SoundManager.instance.gymMusicSource.Play();
        SceneManager.LoadScene("IncMain");
	}

	public void HowToPlayBtn()
	{
		showGUI = true;
	}

	void HowToBox() 
	{
		GUIStyle myBoxStyle = new GUIStyle(GUI.skin.box);
		GUIStyle howToTextStyle = new GUIStyle (GUI.skin.box);
		myBoxStyle.fontSize = 35;
		myBoxStyle.normal.background =  MakeTex(2, 2, new Color(0.6f, 0.3f, 0.3f, 1f));
		howToTextStyle.fontSize = 13;
		howToTextStyle.normal.background = MakeTex(2, 2, new Color(0.6f, 0.3f, 0.3f, 1f));
		howToTextStyle.wordWrap = true;
		GUI.Box(new Rect(Screen.width * 0.29f , Screen.height * 0.1f, Screen.width * 0.5f, Screen.height * 0.8f), "How To Play", myBoxStyle);
		GUI.Box(new Rect(Screen.width * 0.29f , Screen.height * 0.2f, Screen.width * 0.5f, Screen.height * 0.7f), howToPlayString, howToTextStyle);
	}

	void CloseHowToBox()
	{
		GUIStyle myBoxStyle = new GUIStyle(GUI.skin.box);
		GUIStyle myOtherStyle = new GUIStyle (GUI.skin.box);
		myBoxStyle.fontSize = 35;
		myBoxStyle.normal.background =  MakeTex(2, 2, new Color(0.6f, 0.3f, 0.3f, 1f));
		myOtherStyle.normal.background = MakeTex (2, 2, new Color (1f, 1f, 1f, 1f));
		GUI.Box (new Rect (Screen.width * 0.6925f, Screen.height * 0.7805f, Screen.width * 0.086f, Screen.height * 0.1117f), "", myOtherStyle);
		closeHowToBtn =  GUI.Button(new Rect (Screen.width * 0.695f, Screen.height * 0.785f, Screen.width * 0.08f, Screen.height * 0.1f), "X", myBoxStyle);
		if (closeHowToBtn)
		{
			showGUI = false;
		}
	}

	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}


}
