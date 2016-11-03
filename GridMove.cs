using UnityEngine;
using System.Collections;

public abstract class GridMove : MonoBehaviour {

	public LayerMask blockingLayer;         //Layer on which collision will be checked.
	private BoxCollider2D boxCollider;      //The BoxCollider2D component attached to this object.
	private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.

	public float moveSpeed = 3f;
	public float gridSize = 1f;
	private enum Orientation {
		Hori,
		Vert
	};
	private Orientation gridOrientation = Orientation.Vert;
	public bool allowDiagonals = false;
	public bool correctDiagonalSpeed = true;
	private Vector2 input;
	public bool isMoving = false;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float t;
	private float factor;

	protected virtual void Start ()
	{
		//Get a component reference to this object's BoxCollider2D
		boxCollider = GetComponent <BoxCollider2D> ();
		
		//Get a component reference to this object's Rigidbody2D
		rb2D = GetComponent <Rigidbody2D> ();
	}

	protected bool Move1 (int xDir, int yDir, out RaycastHit2D hit)
	{
		Debug.Log ("MOVE1 initiated");
		//Store start position to move from, based on objects current transform position.
		Vector2 start = transform.position;
		
		// Calculate end position based on the direction parameters passed in when calling Move.
		Vector2 end = start + new Vector2 (xDir, yDir);
		
		//Disable the boxCollider so that linecast doesn't hit this object's own collider.
		boxCollider.enabled = false;
		
		//Cast a line from start point to end point checking collision on blockingLayer.
		hit = Physics2D.Linecast (start, end, blockingLayer);
		
		//Re-enable boxCollider after linecast
		boxCollider.enabled = true;
		
		//Check if anything was hit
		if(hit.transform == null)
		{
			//If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
			StartCoroutine (smoothmove (transform));
			
			//Return true to say that Move was successful
			Debug.Log ("true");
			return true;
		}
		
		//If something was hit, return false, Move was unsuccesful.
		Debug.Log ("false");
		return false;
	}

	
	public IEnumerator smoothmove(Transform transform) {
		isMoving = true;
		startPosition = transform.position;
		t = 0;
		
		if(gridOrientation == Orientation.Hori) {
			endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
			                          startPosition.y, startPosition.z + System.Math.Sign(input.y) * gridSize);
		} else {
			endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
			                          startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
		}
		
		if(allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) {
			factor = 0.7071f;
		} else {
			factor = 1f;
		}
		
		while (t < 1f) {
			t += Time.deltaTime * (moveSpeed/gridSize) * factor;
			transform.position = Vector3.Lerp(startPosition, endPosition, t);
			yield return null;
		}

		isMoving = false;
		yield return 0;
	}

protected virtual void AttemptMove <T> (int xDir, int yDir)
	where T : Component
{
	//Hit will store whatever our linecast hits when Move is called.
	RaycastHit2D hit;

	
	//Set canMove to true if Move was successful, false if failed.
	bool canMove = Move1 (xDir, yDir, out hit);
	
	//Check if nothing was hit by linecast
	if(hit.transform == null)
		//If nothing was hit, return and don't execute further code.
		return;
	
	//Get a component reference to the component of type T attached to the object that was hit
	T hitComponent = hit.transform.GetComponent <T> ();
	
	//If canMove is false and hitComponent is not equal to null, meaning MovingObject is blocked and has hit something it can interact with.
	if(!canMove && hitComponent != null)
		
		//Call the OnCantMove function and pass it hitComponent as a parameter.
		OnCantMove (hitComponent);
}
	protected abstract void OnCantMove <T> (T component)
		where T : Component;
}
