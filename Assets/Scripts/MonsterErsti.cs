using UnityEngine;
using System.Collections;

public class MonsterErsti : StandartMonster {

	// Ersti:
	//
	// HP - 10
	// Damage - 5
	//
	// Zieht jede Runde
	// Zieht immer ein Feld
	// Keine schlaue KI

	AudioClip[] hit;
	
	protected override void Start(){
		hit = new AudioClip[] {erstiHit1, erstiHit2, erstiHit3};
		base.Start ();
		this.setHealthPoint(10);
		this.setDamage(5);
		GameManager.instance.AddEnemyToList(this);
		this.attackSound = erstiAttack;
		this.hitSound = hit[Random.Range (0,hit.Length)];
	}

	void Update(){
		this.hitSound = hit[Random.Range (0,hit.Length)];
	}
	
}
