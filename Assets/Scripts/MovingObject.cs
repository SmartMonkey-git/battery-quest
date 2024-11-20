using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Dies ist die Grundklasse um Objecte auf dem Spielfeld zu bewegen
/// </summary>
// Comment: MovingObject should not be part of the "Monster"-Class familiy, it should instead be added as another component.
public abstract class  MovingObject : MonoBehaviour {
	
	//Bestimmt die zeit die für eine Bewegung genutzt wird
	public float moveTime = 0.1f;
	public LayerMask blockingLayer;
	private BoxCollider2D boxCollider;
	private Rigidbody2D rb2D;
	private float inverseMoveTime;
	
	
	// Use this for initialization
	protected virtual void Start () {
		//Gibt dem Object die komponenten der beiden Klassen
		boxCollider = GetComponent<BoxCollider2D> ();
		rb2D = GetComponent<Rigidbody2D> ();
		inverseMoveTime = 1 / moveTime;

	}


	// Sort für eine saubere bewegung. IEnumerator unterstützt eine einfache iteration
	protected IEnumerator SmoothMovement(Vector3 end){
		// Berrechnet die übrige Distance zwischen dem anfangs und end punkt
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		
		// Schleife läuft solange "sqrRe" kleiner als die kleinst mögliche float zahl die größer als 0 ist. (Epsilon)
		while (sqrRemainingDistance > float.Epsilon) 
		{
			//MoveTowards bewegt ein Objekt auf einer geraden Linie -> parameter(vec3 currentPosition, Vec3 targetPosition, float maxDistanceDelta)
			Vector3 newPosition =Vector3.MoveTowards (rb2D.position, end, inverseMoveTime * Time.deltaTime );
			//Bewegt den Rigidboday entlang des Vectors
			rb2D.MovePosition(newPosition);
			//print (newPosition);
			//"sqrRe" wird neu berechnet
			sqrRemainingDistance= (transform.position - end).sqrMagnitude;
			yield return null; // Warum???
		}
	}


	//Checkt ob es möglich ist sich zu bewegen. Out sorgt dafür, dass der Wert den Hit hat mit aus der Funktion herausgenommen wird
	protected bool Move(int xDir, int yDir, out RaycastHit2D hit){
		//print ("Move");
		// Start und Ziel werden erstellt
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir*0.9f, yDir*0.9f);
		// Box-Collider ausschalten, um mit dem Raycast nicht den eigenen Box-Collider zu treffen
		boxCollider.enabled = false;
		//Die Physic2D klasse castet einen Strahl und guck ob dieser durch ein anderes Gameobject unterbrochen wird
		hit = Physics2D.Linecast (start, end, blockingLayer);
		boxCollider.enabled = true;
		
		if (hit.transform == null) {
			//Startet treath mit SmoothMovment
			StartCoroutine (SmoothMovement (end));
			return true;
		} else
			return false;
	}

	// Was die Einheit macht, wenn man sich nicht bewegen kann
	protected abstract void OnCantMove <T> (T Component) 
		where T:Component;
	
	
	
	//Versuch einer Bewegung
	protected virtual void AttemptMove <T>(int xDir, int yDir)
	where T: Component{
		//Ein RaycastHit2D wird erstellt um die Collisiondetection zu erfassen
		RaycastHit2D hit;
		//Fragt nach der möglichkeit der Bewegungn
		bool canMove = Move(xDir, yDir, out hit);

		// Es wird geguckt ob linecast nichts getroffen hat
		if (hit.transform == null) {
			return;
		}
	

		// Erzeugt ein Object von dem Getroffenen Object
		T hitComponent = hit.transform.GetComponent<T> ();
		if (!canMove && hitComponent != null) {
			//print (this.name +": CantMove");
			OnCantMove(hitComponent);
		}
	}
	// Update is called once per frame
	void Update () {


	}
}