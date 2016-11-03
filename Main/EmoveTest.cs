using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class EmoveTest : MonoBehaviour {
	

	private EnemyMove enemyMoveScript = new EnemyMove();
	
	public LayerMask blockingLayer;         //Layer on which collision will be checked.
	private BoxCollider2D boxCollider;      //The BoxCollider2D component attached to this object.
	private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.
	
	public Vector2 enemyPos;

	public int moveVal;
	
	bool justStartedBool = true;
	bool isMoving = true;
	bool valGotten = false;

	public float timeLeft;
	public float timer;
	
	
	
	// Use this for initialization
	protected virtual void Start () {

		timeLeft = 2f;

		//moveVal = Random.Range (1, 5);
		//moveVal = EnemyMove.RandomNumber ();
		moveVal = Random.Range (1, 5);

		//Get a component reference to this object's BoxCollider2D
		//boxCollider = GetComponent <BoxCollider2D> ();
		boxCollider = GetComponentInParent <BoxCollider2D> ();
		
		//Get a component reference to this object's Rigidbody2D
		//rb2D = GetComponent <Rigidbody2D> ();
		rb2D = this.transform.parent.GetComponent<Rigidbody2D> ();
		
		//Vector2 enemyPos = gameObject.transform.position;
		Vector2 enemyPos = this.transform.parent.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		//set moveVal to change every 3 seconds
		timeLeft -= Time.deltaTime;

		if (timeLeft < 0) {

			if (moveVal == 1) {

				MoveRight ();
				if(timeLeft < -2f){
					timeLeft = 2f;
					//moveVal = Random.Range(1,5);
					moveVal = Random.Range (1, 5);

				}
			}
			if (moveVal == 2) {

				MoveLeft ();
				if(timeLeft < -2f){
					timeLeft = 2f;
					//moveVal = Random.Range(1,5);
					moveVal = Random.Range (1, 5);
				}
			
			}
			if (moveVal == 3) {

				MoveUp ();
				if(timeLeft < -2f){
					timeLeft = 2f;
					//moveVal = Random.Range(1,5);
					moveVal = Random.Range (1, 5);
				}
			}
			if (moveVal == 4) {

				MoveDown ();
				if(timeLeft < -2f){
					timeLeft = 2f;
					//moveVal = Random.Range(1,5);
					moveVal = Random.Range (1, 5);
				}
			}
		}

	}

	
	public void MoveRight(){
		Vector2 mvecE = new Vector2 (1, 0);

		rb2D.MovePosition (rb2D.position + mvecE * (3 * Time.deltaTime));
	}
	
	public void MoveUp(){
		Vector2 mvecE = new Vector2 (0, 1);
		
		rb2D.MovePosition (rb2D.position + mvecE * (3 * Time.deltaTime));
	}
	
	public void MoveLeft(){
		Vector2 mvecE = new Vector2 (-1, 0);
		
		rb2D.MovePosition (rb2D.position + mvecE * (3 * Time.deltaTime));
	}
	
	public void MoveDown(){
		Vector2 mvecE = new Vector2 (0, -1);
		
		rb2D.MovePosition (rb2D.position + mvecE * (3 * Time.deltaTime));
	}


	}

