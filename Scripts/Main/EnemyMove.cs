using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class EnemyMove : MonoBehaviour {

	public static EnemyMove instance3 = null;
	
	public LayerMask blockingLayer;         //Layer on which collision will be checked.
	private BoxCollider2D boxCollider;      //The BoxCollider2D component attached to this object.
	private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.
	
	public Vector2 enemyPos;

	
	void Awake(){
		
		if (instance3== null) {
			
			//if not, set instance to this
			instance3 = this;
		} else if (instance3!= this) {
			
			instance3 = this;
		}
		
	}

	public static int RandomNumber(){
		//InstanceMethod ();

		return Random.Range (1, 5);
	}

	public void InstanceMethod(){
		
		if (instance3== null) {
			
			//if not, set instance to this
			instance3 = this;
		} else if (instance3!= this) {
			
			instance3 = this;
		}
	}

}
