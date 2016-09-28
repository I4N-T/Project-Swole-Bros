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
		GUI.Label(new Rect(Screen.width / 5, Screen.height - (Screen.height / 3), (Screen.width / 2) + 69, Screen.height / 3), guiText, GUI.skin.textArea);
	}
	
	public void AddEvent(string eventString)
	{
		Eventlog.Add(eventString);
		
		if (Eventlog.Count >= maxLines)
			Eventlog.RemoveAt(0);
		
		guiText = "";
		
		foreach (string logEvent in Eventlog)
		{
			guiText += logEvent;
			guiText += "\n";
		}
	}
}
