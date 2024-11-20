using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	// Instans des selbigen Objects
	public static GameManager instance = null;
	//BoardManager Script
	public BoardManager boardScript;
	// Stage Script
	public Stage stage;

	//Das Momentane Level
	int currentLevel= 1;
	// Ob der Spieler dran ist
	[HideInInspector] public bool PlayerTurn = true;
	// Charakter Script
	public GameObject character;
	private Transform characterHolder;

	bool initiate = true;
	public int score;

    // Rouvens Zeug
    public float turnDelay = .1f;
    private bool enemiesMoving;
    private List<StandartMonster> enemies;
    public bool used = true;
	
	
	//Die Awake funktion wird vor der Start funtkion aufgerufen, immer wenn ein Game Objekt mit Awake zum Einsatz kommt
	void Awake () {
        print("gamemanager");
		score = 0;
		used = false;
        //Wenn das Objekt instance null ist, wird das eigentliche Obejkt in instance Kopiert.
        if (instance == null)
        {
            instance = this;
            //Sollte instance nicht this sein und bereits existieren so wird es zerstört. Sorgt dafür das immer nur ein GameManager existieren kann.
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
		
		//Sollte die Scene neu geladen werden, so wird das Object nicht zerstört!!
		DontDestroyOnLoad (gameObject);
		//Das BoardScript wird initialisiert.
		boardScript = GetComponent<BoardManager>();
		stage = GetComponent<Stage>();
        enemies = new List<StandartMonster>();
		if (initiate) {
			initiate = false;
			initGame ();
            instance.enabled = true;
		}
		
	}

	//Initialisiert das Spiel!
	void initGame () {
		//print ("initiate - " + initiate);
			characterHolder = new GameObject ("Character").transform;
			GameObject toInstantiate = character;
			print ("init");
			//Das Level wird nur eingefügt, wenn es kleiner als 5 ist, da wir maximal 5 Level haben.
			if (currentLevel < 5) {
				GameObject instance = Instantiate (toInstantiate, new Vector3 (4 * 0.9f, 0 * 0.9f, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent (characterHolder);
				print ("Transform: " + GameObject.FindGameObjectWithTag("Player").transform.localPosition);
				stage.SetupScene ();
			}
			print ("init: " + initiate);
	}


	public void GameOver() {
		enabled = false;
	}

    // Rouvens Zeug
    void Update()
    {
        //print("Update");
        //print("Player: " + PlayerTurn + " Monster: " + enemiesMoving);


        if (PlayerTurn || enemiesMoving)
        {
            return;
        }
        // Couroutine --> Threat
        StartCoroutine(MovingEnemies());

    }

	public void RemoveEnemiesFromList(){
		enemies.Clear ();
	}

	public void RemoveOneEnemieFromList(StandartMonster Monster){
		if (Monster.name == "Prof(Clone)") {
			GameManager.instance.score += 15;
		}
		if (Monster.name == "Ersti(Clone)") {
			GameManager.instance.score +=2;
		}
		if (Monster.name == "Zwölfti(Clone)") {
			GameManager.instance.score +=5;
		}
		print (Monster.transform.position);
		
		foreach (StandartMonster mon in enemies) {
			if (mon.getHealthPoint() <= 0){
				print ("Monster gefunden");
				enemies.Remove(mon);
				GameObject[] mons = GameObject.FindGameObjectsWithTag("Enemy");
				foreach (GameObject m in mons){
					if(Monster.transform.position == m.transform.position){
						Vector3 pos = Monster.getStartPosition();
						float x = pos.x;
						float y = pos.y;
						stage.updateItems(x,y);
						Destroy (m);
					}
				}
			}
		}
	}


    // Rouvens Zeug
    public void AddEnemyToList(StandartMonster script)
    {
        enemies.Add(script);

    }


    IEnumerator MovingEnemies()
    {
        print("MovingEnemies");
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);

        }


        PlayerTurn = true;
        enemiesMoving = false;
    }

}
