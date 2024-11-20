using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Diese Klasse soll die Statuswerte einer Spielfigur festhalten
/// </summary>
public abstract class Unit : MovingObject {

	private int healtPoints = 100;
	private int toughness=1;
	private int damageReduction = 0;
	private int damage=5;
	private short movement=1;
	private int accuracy=100;
	private int dodge=0;
	private int luck=0;
	private int batt;
	private Vector3 postion = new Vector3(0,0,0);
	private string waffe = "";
	private string accessoire = "";
	public int WeaponHp;
	public int weaponghpmax;

	private bool flying=false;

	// Getter-Methoden
	public int getDamageReduction(){
		return this.damageReduction;
	}

	public int getBatterien(){
		return this.batt;
	}

	public int getHealthPoint(){
		return this.healtPoints;
	}

	public int getToughness(){
		return this.toughness;
	}

	public short getMovement(){
		return this.movement;
	}

	public int getDamage(){
		return this.damage;
	}

	public int getAccuracy(){
		return this.accuracy;
	}

	public int getDodge(){
		return this.dodge;
	}

	public int getLuck(){
		return this.luck;
	}

	public bool getFlying(){
		return this.flying;
	}

	public string getWaffe(){
		// print ("GetWaffe: " + this.waffe);
		return this.waffe;
	}

	public string getAccessoire(){
		//print ("GetAcce: " + this.accessoire);
		return this.accessoire;
	}

	//Setter-Methoden
	public void setDamageReduction(int RED){
		this.damageReduction = RED;
	}

	public void setBatterien(int BAT){
		this.batt = BAT;
	}

	public void setHealthPoint(int HP){
		this.healtPoints = HP;
	}

	public void setHToughness(int TO){
		this.toughness = TO;
	}

	public void setDamage(int DMG){
		this.damage = DMG;
	}

	public void setMovement(short MOV){
		this.movement = MOV;
	}

	public void setAccuracy(int ACC){
		this.accuracy = ACC;
	}

	public void setDodge(int DO){
		this.dodge = DO;
	}

	public void setLuck(int LU){
		this.luck = LU;
	}

	public void setFlying(bool FLY){
		this.flying = FLY;
	}

	public void setWaffe(string WAF){
		
		if (WAF == "Gummiaxt") {
			WeaponHp= 10;
			weaponghpmax=10;
		}
		if (WAF == "Fleischwurst") {
			WeaponHp= 5;
			weaponghpmax= 5;
		}
		if (WAF == "Lazor") {
			WeaponHp = 3;
			weaponghpmax = 3;
		}
		
		this.waffe = WAF;
	}

	public void setAccessoire(string ACS){
		print ("SetAcce: " + ACS);
		this.accessoire = ACS;
	}

	//protected abstract void OnCantMove <T> (T Component) 
	//	where T:Component;

}
