using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Playera : Unit {

	// Objekt genutzter Komponenten
	private Animator animator;
	public BoardManager board;
	public Stage stage;

	// Objekte zur Anzeige von Text
	public Text GameOver;
	public Text EndScore;
	public Text Impressum;

	// Prüfvariable ob das Spiel vorbei ist
	public bool gameisover;

	// Prüfvariable ob Tasten betätigt wurden
	public bool buttonPressable = true;
	public bool returnPressed = false;

	// Verzögerung 
	//public float turnDelay = 1f;

	// Zeitzähler
	public float updateInterval;
	private double lastInterval;
	public int timeCounter;
	public float timeNow;
	public int timeLeft;

	// Objekte zum Anzeigen von Waffe + Accessiore
	public Texture2D fleischwurst;
	public Texture2D gummiaxt;
	public Texture2D laser;
	public Texture2D klo;

	// Batteriezähler
	public Texture2D battCounter;
	public Texture2D battFounds;

	// Objekte für die Statusanzeigen
	public Rect lifeBarRect;
	public Rect lifeBarRedRect;
	public Rect lifeBarFrameRect;
	public Texture2D lifebarGreen;
	public Texture2D lifebarRed;
	public Texture2D lifebarFrame;
	public Texture2D Handy;
	public Texture2D RahmenWaffe;
	public Texture2D RahmenAccess;

	// Sounds
	public AudioClip basicAttack;
	public AudioClip playerHit1;
	public AudioClip playerHit2;
	public AudioClip playerHit3;
	public AudioClip playerHit4;
	public AudioClip laser1;
	public AudioClip collectItem;
	public AudioClip collectBattery;
	public AudioClip eat;
	public AudioClip drink;
	public AudioClip burp;
	public AudioClip gummiAxt;
	public AudioClip win;
	public AudioClip gameOver1;
	public AudioClip gameOver2;
	public AudioClip kanyeWest;
	public AudioClip walk;

	// Zwischenspeichern des ausgewählten Attack-Sounds
	public AudioClip currentAttackSound;

	// Objekt zur Anzeige der Beschreibungen (Notizzettel)
	public Texture2D[] beschreibungenGrafik = new Texture2D[12];

	// Prüfvariablen, ob der Spieler auf einem Feld mit jew. Objekt steht
	bool fleischwustBooler = false;
	bool gummiaxtBooler =false;
	bool laserBooler= false;
	bool bananaBooler = false;
	bool coffeBooler = false;
	bool klopapierBooler= false;
	bool batteriBooler= false;
	bool mulitmaschBooler= false;
	// Variablen zur Punktezählung
	int subScore;
	public bool pointcalc = true;


	//////////////
	// Methoden //
	//////////////
	

	// Bei erstmaligem Aufruf des Spielers
	protected override void Start () {
		animator = GetComponent<Animator>(); 
		base.Start ();
		gameisover = false;
		GameObject GameOverText = GameObject.Find ("GameOver Text").gameObject;
		GameObject ImpressumText = GameObject.Find ("Impressum Text").gameObject;
		GameObject ScoreEnde = GameObject.Find ("EndScore Text").gameObject;
		GameOver = GameOverText.GetComponent<Text>();
		Impressum = ImpressumText.GetComponent<Text>();
		EndScore = ScoreEnde.GetComponent<Text> ();
		updateInterval = 1f;
		lastInterval = Time.realtimeSinceStartup;
		timeCounter = 0;
		timeLeft = 250 - timeCounter;
		currentAttackSound = basicAttack;
	}

	// 
	protected override void OnCantMove <T> (T component){
		print ("Hitcomponent: "+component);
		this.WeaponHp -= 1;
		animator.SetTrigger("Attack");
		SoundManager.instance.PlaySingle (currentAttackSound);
		print (currentAttackSound);
		StandartMonster hitMonster = component as StandartMonster;
		print("MosnterHP: " + hitMonster.getHealthPoint ());
		hitMonster.getDamaged (this.getDamage ());
	}

//	IEnumerator turnDelaying(){
//		yield return new WaitForSeconds(turnDelay);
//	}


	protected override void AttemptMove<T> (int xDir, int yDir){
		base.AttemptMove <T>(xDir, yDir);
		GameManager.instance.PlayerTurn = false;
		// Box-Collider ausschalten, um mit dem Raycast nicht den eigenen Box-Collider zu treffen
		//Die Physic2D klasse castet einen Strahl und guck ob dieser durch ein anderes Gameobject unterbrochen wird
		RaycastHit2D hit;
		base.Move(xDir, yDir, out hit);

		if(xDir == 1){
			animator.SetBool("MirrorIdle", false);
		} else if (xDir == -1){
			animator.SetBool("MirrorIdle", true);
		}
		if (Move (xDir, yDir, out hit)) {
			animator.SetTrigger ("Walk");
			SoundManager.instance.PlaySingle (walk);
		}

		if (hit.collider == null) {
			print("No collider hit");
			return;
		}

		String name = hit.collider.gameObject.name;
		print ("Collider = " + name);

		if (name == "Door(Clone)") {
			print ("Tür gefunden!");
			GameObject boardholder = GameObject.Find ("Board").gameObject;
			Destroy (boardholder);
			GameObject roomholder = GameObject.Find ("Room").gameObject;
			Destroy (roomholder);
			newRoom ();
			newPosition ();
		}
	}



	//Erzeugt neuen Raum
	// Comment: Should not be in the playe class.
	// Goes with the first law of the SOLID pricipal.
	public void newRoom(){
		stage.updateRoom ();
		board.BoardSetup ();
		board.RoomSetup ();
		GameManager.instance.RemoveEnemiesFromList ();
		GameManager.instance.PlayerTurn = true;
	}

	// Berechnet neue Postion für anliegenden Raum (Wenn rechts raus - Links rein)
	public void newPosition(){
		print("Moving player");
		Vector3 links = new Vector3 (0.9f * 0, 0.9f * 5, 0f);
		Vector3 unten = new Vector3 (0.9f*4, 0.9f*0, 0f);
		Vector3 oben = new Vector3 (0.9f*5, 0.9f*9, 0f);
		Vector3 rechts = new Vector3 (0.9f*9, 0.9f*4, 0f);
		if (transform.position == links){
			transform.position = rechts;
		} else if (transform.position == rechts){
			transform.position = links;
		} else if (transform.position == oben) {
			transform.position = unten;
		} else if (transform.position == unten) {
			transform.position = oben;
		}
	}


	// Update is called once per frame
	void Update () {
		board = GameManager.instance.GetComponent<BoardManager> ();
		stage = GameManager.instance.GetComponent<Stage>();
		timeNow = Time.realtimeSinceStartup;

		if (Input.GetKey ("escape")) {
				Application.Quit ();


		
		}

		if (this.WeaponHp <= 0) {
			setWaffe("");
			this.setDamage(5);
			currentAttackSound = basicAttack;
		}

		if (timeNow > lastInterval + updateInterval && gameisover == false) {
			//print ("Gewonnen: " + gameisover);
			//print ("Erhöhen");
			timeCounter++;
			timeLeft = 250-timeCounter;
			lastInterval = timeNow;
		}
		if(timeLeft <= 0){
			GameOver.text = "Game Over";
			SoundManager.instance.RandomizeSfx(gameOver1, gameOver2, kanyeWest);
			gameisover = true;
			GameManager.instance.GameOver();
		}
		if (!GameManager.instance.PlayerTurn) {
			return;
		}

		int horizontal = 0;
		int vertikal = 0;
		horizontal = (int) Input.GetAxisRaw ("Horizontal");
		vertikal   = (int) Input.GetAxisRaw ("Vertical");

		if (horizontal != 0) {
			vertikal =0;
		}
		if (horizontal != 0 || vertikal != 0) {
			AttemptMove <StandartMonster> (horizontal, vertikal);

		}
	}

	void OnGUI(){

		Event e = Event.current;
		if (e.isKey) {
			if (e.keyCode == KeyCode.Return) {
				print ("Return Pressed!");
				returnPressed = true;
			}
		}

		//schrift Links
		GUI.Label (new Rect (45, 860, 500, 500), "- 'Shor applause.mp3' von J.Zazvurek (http://freesound.org/people/J.Zazvurek/sounds/60788/)");
		GUI.Label (new Rect (45, 820, 500, 500), "- 'Whip Crack 01.wav' von CGEffex (http://freesound.org/people/CGEffex/sounds/93100/)");
		//Zettel Links
		GUI.Label (new Rect (-20, -130, beschreibungenGrafik [9].width, beschreibungenGrafik [9].height), beschreibungenGrafik [9]);
		if (fleischwustBooler != false) {
			GUI.Label (new Rect (-20, -130, beschreibungenGrafik [3].width, beschreibungenGrafik [3].height), beschreibungenGrafik [3]);
		} else if (gummiaxtBooler != false) {
			GUI.Label (new Rect (-20, -130, beschreibungenGrafik [2].width, beschreibungenGrafik [2].height), beschreibungenGrafik [2]);
		} else if (laserBooler != false) {
			GUI.Label (new Rect (-20, -130, beschreibungenGrafik [4].width, beschreibungenGrafik [4].height), beschreibungenGrafik [4]);
		} else if (bananaBooler != false) {
			GUI.Label (new Rect (-20, -130, beschreibungenGrafik [0].width, beschreibungenGrafik [0].height), beschreibungenGrafik [0]);
		} else if (coffeBooler != false) {
			GUI.Label (new Rect (-20, -130, beschreibungenGrafik [1].width, beschreibungenGrafik [1].height), beschreibungenGrafik [1]);
		} else if (klopapierBooler != false) {
			GUI.Label (new Rect (-20, -130, beschreibungenGrafik [5].width, beschreibungenGrafik [5].height), beschreibungenGrafik [5]);
		} else if (batteriBooler != false) {
			GUI.Label (new Rect (-20, -130, beschreibungenGrafik [10].width, beschreibungenGrafik [10].height), beschreibungenGrafik [10]);
		} else if (mulitmaschBooler != false) {
			GUI.Label (new Rect (-20, -130, beschreibungenGrafik [11].width, beschreibungenGrafik [11].height), beschreibungenGrafik [11]);
		}


		// Handy Position Rechts
		GUI.Label(new Rect (1250+200, 0, Handy.width, Handy.height), Handy);
		GUI.Label(new Rect (1360+200, 49, 200, 50), "Konzentration: ");
		GUI.Label(new Rect (1360+200, 80, RahmenWaffe.width, RahmenWaffe.height), RahmenWaffe);
		GUI.Label(new Rect (1480+200, 80, RahmenAccess.width, RahmenAccess.height), RahmenAccess);
		GUI.Label(new Rect (1480+200, 63, 200, 50), "Time: " + timeLeft.ToString());
		if (getWaffe () != "") {
			GUI.Label (new Rect (1370+200, 175, 50, 50), this.WeaponHp + "/" + this.weaponghpmax);
		}
		GUI.Label(new Rect(1450+200,408, 90,90), "Score: " + GameManager.instance.score );

		// LifeBar rechts am Bildschirm
		lifeBarRect.position = new Vector2 (1360+200, 69);
		lifeBarRect.width = this.getHealthPoint ();
		lifeBarRect.height = 10;
		GUI.DrawTexture (lifeBarRect, lifebarGreen); // Grüner Teil der Lifebar
		lifeBarRedRect.position = new Vector2 (1360+this.getHealthPoint()+200, 69);
		lifeBarRedRect.width = 100 - this.getHealthPoint ();
		lifeBarRedRect.height = 10;
		GUI.DrawTexture (lifeBarRedRect, lifebarRed); // Roter Teil der Lifebar


		//Batterien Links am Bildschirm
		if (this.getBatterien () <= 0) {
			//print ("Batt");
			GUI.Label (new Rect (1335+200, 360, battCounter.width, battCounter.height), battCounter);
		} else {
			//print ("BattFound");
			GUI.Label (new Rect (1333+200, 362, battFounds.width, battFounds.height), battFounds);
		}

		if (this.getBatterien () <= 1) {
			GUI.Label (new Rect (1335 +30+200, 360, battCounter.width, battCounter.height), battCounter);
		} else {
			GUI.Label (new Rect (1335 +28+200, 362, battFounds.width, battFounds.height), battFounds);
		}
		if (this.getBatterien () <= 2) {
			GUI.Label (new Rect (1335 + 60 + 200, 360, battCounter.width, battCounter.height), battCounter);
		} else {
			GUI.Label (new Rect (1335 + 58 + 200, 362, battFounds.width, battFounds.height), battFounds);
		}



		// Waffen Bilder werden Rechts am Rand gesetzt
		if (this.getWaffe() == "Fleischwurst") {
			//print ("FW");
			GUI.Label (new Rect (1380+200, 95, fleischwurst.width, fleischwurst.height), fleischwurst);
		}

		if (this.getWaffe()== "Gummiaxt") {
			//print ("GA");
			GUI.Label (new Rect (1375+200, 100, gummiaxt.width, gummiaxt.height), gummiaxt);
		}

		if (this.getWaffe() == "Lazor") {
			//print ("lazor");
			GUI.Label (new Rect (1375+200, 105, laser.width, laser.height), laser);
		}


		if(this.getAccessoire() == "Klopapier"){
			GUI.Label (new Rect (1495+200, 100, klo.width, klo.height), klo);
		}
		
	}

	void OnTriggerExit2D(Collider2D other){
		print ("TriggerExit2D");
		fleischwustBooler = false;
		gummiaxtBooler = false;	
		laserBooler = false;
		bananaBooler = false;
		coffeBooler = false;
		klopapierBooler = false;
		batteriBooler= false;
		mulitmaschBooler = false;
		
	}


	void OnTriggerStay2D(Collider2D other){
		Vector3 pos = transform.position;
		float x = 0;
		float y = 0;
		x = pos.x;
		y = pos.y;
		if (other.tag == "Fleisch") {
			fleischwustBooler = true;
			if (returnPressed == true) {
				GameObject[] fl = GameObject.FindGameObjectsWithTag ("Fleisch");
				foreach(GameObject f in fl){
					if (f.transform.position == transform.position) {
						SoundManager.instance.PlaySingle(collectItem);
						currentAttackSound = basicAttack;
						GameManager.instance.score+=5;
						Destroy (f);
						stage.updateItems (x, y);
						this.setWaffe ("Fleischwurst");
						this.setDamage(10);
						fleischwustBooler = false;
					}
				}
				returnPressed = false;
			}
		} else if (other.tag == "Lazor") {
			laserBooler= true;
			if (returnPressed == true) {
				GameObject[] la = GameObject.FindGameObjectsWithTag ("Lazor");
				foreach (GameObject l in la){
					if (l.transform.position == transform.position) {
						SoundManager.instance.PlaySingle(collectItem);
						currentAttackSound = laser1;
						GameManager.instance.score+=10;
						Destroy (l);
						stage.updateItems (x, y);
						this.setWaffe ("Lazor");
						this.setDamage(20);
						laserBooler =false;
					}
				}
				returnPressed = false;
			}
		} else if (other.tag == "Axt") {
			gummiaxtBooler =true;
			if (returnPressed == true) {
				GameObject[] gu = GameObject.FindGameObjectsWithTag ("Axt");
				foreach (GameObject g in gu){
					if (g.transform.position == transform.position) {
						SoundManager.instance.PlaySingle(collectItem);
						currentAttackSound = gummiAxt;
						GameManager.instance.score+=3;
						Destroy (g);
						stage.updateItems (x, y);
						this.setWaffe ("Gummiaxt");
						this.setDamage(5);
						gummiaxtBooler =false;
					}
				}
				returnPressed = false;
			}
		} else if (other.tag == "Klo") {
			klopapierBooler=true;
			if (returnPressed == true) {
				GameObject[] kl = GameObject.FindGameObjectsWithTag ("Klo");
				foreach (GameObject k in kl){
					if (k.transform.position == transform.position) {
						SoundManager.instance.PlaySingle(collectItem);
						GameManager.instance.score+=5;
						Destroy (k);
						stage.updateItems (x, y);
						this.setAccessoire ("Klopapier");
						this.setDamageReduction(2);
						klopapierBooler= false;
					}
				}
				returnPressed = false;
			}
		} else if (other.tag == "Banane") {
			bananaBooler= true;
			if (returnPressed == true) {
				GameObject[] ba = GameObject.FindGameObjectsWithTag("Banane");
				foreach (GameObject b in ba){
					if (b.transform.position == transform.position) {
						GameManager.instance.score+=3;
						Destroy (b);
						stage.updateItems (x, y);
						gainHP(5);
						print ("HP Tag: " + this.getHealthPoint());
						bananaBooler =false;
						SoundManager.instance.RandomizeSfx(eat,burp);
					}
				}
				returnPressed = false;
			}
		} else if (other.tag == "Kaffee"){
			coffeBooler = true;
			if (returnPressed == true){
				GameObject[] ka = GameObject.FindGameObjectsWithTag ("Kaffee");
				foreach(GameObject kf in ka){
					if(kf.transform.position == transform.position){
						GameManager.instance.score+=5;
						Destroy(kf);
						stage.updateItems (x,y);
						gainHP(10);
						print ("HP Tag: " + this.getHealthPoint());
						coffeBooler =false;
						SoundManager.instance.RandomizeSfx(drink,burp);
					}
				}
				returnPressed = false;
			}
		} else if (other.tag == "Batterie"){
			batteriBooler=true;
			if (returnPressed == true) {
				GameObject[] bat = GameObject.FindGameObjectsWithTag ("Batterie");
				foreach(GameObject bt in bat){
					if(bt.transform.position == transform.position){
						Destroy(bt);
						stage.updateItems (x,y);
						int batterien = this.getBatterien() + 1;
						this.setBatterien(batterien);
						batteriBooler=false;
						SoundManager.instance.PlaySingle(collectBattery);
					}
				}
				returnPressed = false;
			}
		} else if (other.tag == "Multiversum"){
			//EndScore.text = "Score: punkte";
			mulitmaschBooler= true;
			if (this.getBatterien() == 3){
				if(pointcalc==true){
					GameManager.instance.score = GameManager.instance.score + this.getHealthPoint()*3;
					GameManager.instance.score = GameManager.instance.score+ this.timeLeft;
					subScore = GameManager.instance.score;
					pointcalc=false;
				}
				subScore-=1;
				int pointdisplay = GameManager.instance.score - subScore;
				if(pointdisplay >= 0 && subScore >= 0){
					EndScore.text = "Score: " + pointdisplay;
				}
				gameisover = true;
				GameOver.text = "Gewonnen!";
				SoundManager.instance.PlaySingle(win);
			}
		}
	}


	// Aktuelle HP nach HP-Verlust errechnen
	public void loseHP(int lose){
		animator.SetTrigger ("Hit");
		SoundManager.instance.RandomizeSfx (playerHit1, playerHit2, playerHit3, playerHit4);
		lose = lose - this.getDamageReduction ();
		if (lose < 0) {
			lose = 0;
		}
		int ATMHP = this.getHealthPoint ()- lose; // ATMHP = At-the-moment-HP
		if (ATMHP < 0){
			ATMHP = 0;
		}
		this.setHealthPoint(ATMHP);
		if (this.getHealthPoint () <= 0) {
			gameisover = true;
			GameOver.text = "Game Over";
			SoundManager.instance.musicSource.Stop ();
			SoundManager.instance.GameOverRandomizeSfx(gameOver1, gameOver2, kanyeWest);
			GameManager.instance.GameOver();
		}
	}

	// Aktuelle HP nach HP-Heilung errechnen
	public void gainHP(int add){
		if (add < 0){
			add = 0;
		}
		int GOTHP = this.getHealthPoint () + add; //GOTHP = Got HP 
		if (GOTHP > 100) {
			GOTHP = 100;
		}
		this.setHealthPoint (GOTHP);

	}

	
}
