using UnityEngine;
using System.Collections;

	public class Wall : MonoBehaviour
	{

		
		
		
		
		
		//DamageWall is called when the player attacks a wall.
		public void DamageWall ()
		{


			
		Application.LoadLevel ("SquatRackLevel");
			
				//Disable the gameObject.
				gameObject.SetActive (false);
		}
	}

