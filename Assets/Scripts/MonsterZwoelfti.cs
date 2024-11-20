using UnityEngine;
using System.Collections;

public class MonsterZwoelfti : StandartMonster {
	
	// Zwölfti:
	//
	// HP - 30
	// Damage - 10
	//
	// Zieht jede zweite Runde
	// Zieht immer ein Feld
	// Schlaue KI --> noch entwickeln

	private bool skipMove;
	public AudioClip[] hit;
	
	protected override void Start(){
		hit = new AudioClip[] {zwoelftiHit1, zwoelftiHit2, zwoelftiHit3};
		base.Start ();
		this.setHealthPoint (20);
		this.setDamage (10);
		GameManager.instance.AddEnemyToList (this);
		this.attackSound = laser1;
		this.hitSound = hit[Random.Range (0,hit.Length)];
	}
	
	protected override void AttemptMove <T> ( int xDir,int yDir){
		if (skipMove) {
			skipMove=false;
			return;
		}
		base.AttemptMove<T> (xDir, yDir);
		skipMove = true;
	}

	void Update(){
		this.hitSound = hit[Random.Range (0,hit.Length)];
	}

}
