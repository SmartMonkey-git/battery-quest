using UnityEngine;
using System.Collections;


public class MonsterProf : StandartMonster {
	
	// Prof:
	//
	// HP - 60
	// Damage - 20
	//
	// Zieht jede Runde
	// Zieht immer zwei Felder
	// Schlaue KI --> noch entwickeln

	AudioClip[] hit;

	
	protected override void Start(){
		hit = new AudioClip[] {profHit1, profHit2, profHit3, profHit4, profHit5, profHit6, profHit7};
		base.Start ();
		this.setHealthPoint(30);
		this.setDamage(15);
		GameManager.instance.AddEnemyToList(this);
		this.attackSound = profAttack;
		this.hitSound = hit[Random.Range (0,hit.Length)];
	}
	
	protected override void AttemptMove <T> ( int xDir,int yDir){
		base.AttemptMove<T> (xDir, yDir);
	}

	void Update(){
		this.hitSound = hit[Random.Range (0,hit.Length)];
	}
}