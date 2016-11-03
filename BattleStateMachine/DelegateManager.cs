using UnityEngine;
using System.Collections;

public class DelegateManager : MonoBehaviour {
	//answers.unity3d.com/questions/600416/how-do-delegates-and-events-work.html

	public delegate void DelegateContainer();
	public static event DelegateContainer EventContainerOne;

	public void TriggerEvent()
	{
		Debug.Log ("event triggered");
		if (EventContainerOne != null) 
		{
			EventContainerOne ();
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
