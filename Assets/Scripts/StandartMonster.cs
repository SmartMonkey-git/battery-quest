using UnityEngine;
using System.Collections;


// Comment: Classes could be merged, the extra layer of Unity is not necessary
public class StandartMonster : Unit {

	private Animator animator;
	private Transform target;
	public bool secondRound; 
	private Vector3 startPosition;

	// Comment: This should have been rather a list.
	public AudioClip profHit1;
	public AudioClip profHit2;
	public AudioClip profHit3;
	public AudioClip profHit4;
	public AudioClip profHit5;
	public AudioClip profHit6;
	public AudioClip profHit7;
	public AudioClip profAttack;
	public AudioClip erstiHit1;
	public AudioClip erstiHit2;
	public AudioClip erstiHit3;
	public AudioClip erstiAttack;
	public AudioClip zwoelftiHit1;
	public AudioClip zwoelftiHit2;
	public AudioClip zwoelftiHit3;
	public AudioClip laser1;

	public AudioClip hitSound;
	public AudioClip attackSound;

	public void setStartPosition(Vector3 pos){
		this.startPosition = pos;
	}

	public Vector3 getStartPosition(){
		return this.startPosition;
	}

	protected override void Start(){
		startPosition = this.transform.position;
		secondRound = false;
		this.setHealthPoint(10);
		this.setDamage(5);
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		animator = GetComponent <Animator>();
		base.Start ();
		hitSound = erstiHit1;
		attackSound = erstiAttack;
	}

	protected override void AttemptMove <T> ( int xDir,int yDir){

		base.AttemptMove<T> (xDir, yDir);

		if(xDir == 1){							// Wenn sich nach rechts bewegt
			animator.SetBool("Mirror", false);	// Mirroring ausstellen
		} else if (xDir == -1){					// Wenn sich nach links bewegt
			animator.SetBool("Mirror", true);	// Mirroring einstellen
		}
		animator.SetTrigger ("Walk");			// Laufen triggern
	}



	public void MoveEnemy(){

		int xDir = 0;
		int yDir = 0;

		if (Mathf.Abs(target.position.x - transform.position.x) < 0.9f) {
			yDir = target.position.y > transform.position.y ? 1 : -1;
			float ausgabeY = target.position.y - transform.position.y;

		} else {
			float ausgabeX = target.position.x - transform.position.x;
			xDir = target.position.x > transform.position.x ? 1 : -1;
			//print (this.transform+" und " + target);
		}

		AttemptMove <Playera> (xDir	, yDir);
	}

	protected override void OnCantMove <T> (T Component){
		Playera hitPlayer = Component as Playera;
		hitPlayer.loseHP (this.getDamage());
		animator.SetTrigger ("Attack");
		SoundManager.instance.PlaySingle (this.attackSound);
	}

	public void getDamaged(int damage){

		damage = damage - this.getDamageReduction ();
		if (damage < 0) {
			damage=0;
		}
		animator.SetTrigger ("Hit");
		SoundManager.instance.PlaySingle (this.hitSound);

		int ATMHP = this.getHealthPoint() - damage;
		this.setHealthPoint(ATMHP);
		if (ATMHP <= 0) {
			//print ("remoooove enemy");
			//print ("this: " + this);
			GameManager.instance.RemoveOneEnemieFromList(this);
		}
	}
}