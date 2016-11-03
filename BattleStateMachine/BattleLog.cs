using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleLog : MonoBehaviour 
{
	// Private VARS
	private List<string> Eventlog = new List<string>();
	private string guiText = "";
	
	// Public VARS
	public int maxLines = 10;
	
	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width * .185f, Screen.height - (Screen.height / 3), (Screen.width * .63f), Screen.height * .33f), guiText, GUI.skin.textArea);
	}
	
	public void AddEvent(string eventString)
	{
		Eventlog.Add(eventString);

        if (Eventlog.Count >= maxLines)
        {
            Eventlog.RemoveAt(0);
            Eventlog.RemoveAt(1);
            Eventlog.RemoveAt(2);
        }


        guiText = "";
		
		foreach (string logEvent in Eventlog)
		{
			guiText += logEvent;
			guiText += "\n";
		}
	}
}
