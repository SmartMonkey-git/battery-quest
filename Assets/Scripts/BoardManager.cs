using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic; 	// Um Listen nutzen zu können 
using Random = UnityEngine.Random;	// Random über den Unity Engine number generator benutzen


/* Unity 2D Roguelike Video Game
 * 
 * Board-Manager
 * Zum Erstellen des Spielfeldes
 * 
 * Programmiert von: Jana Gädtke
 * Gruppe: F. Fries, J. Gädtke, M. Hermes, F. Hufen, R. Reuter
 * Fach: Produktion Digitaler Medien
 * Betreuer: Ingo Schebesta
 * Hochschule Emden/Leer
 * Studiengang Medientechnik
 * 
 * Datum: 01.12.15
 */


public class BoardManager : MonoBehaviour {
	
	// Spalten und Reihen des Spielfeldes
	public int columns = 10;
	public int rows = 10;

	public int currentLayoutRow;
	public int currentLayoutColumn;

	public Stage stage;

	bool doorTop = false;
	bool doorBottom = false;
	bool doorLeft = false;
	bool doorRight = false;
	
	// Zu benutzende (vorläufige) GameObjects
	//
	// GameObjetcs für den Aufbau des Boards
	public GameObject[] floorTiles; // Boden-Elemente 
	public GameObject[] wallTiles;	// Wand-Elemente
	public GameObject doorTile;		// Tür-Elemente
	public GameObject edgeTile;		// Wand-Ecken-Elemente
	// GameObjects für dObstacles
	public GameObject table;
	public GameObject tableLamp;
	public GameObject tableLaptop;
	public GameObject tableLampLaptop;
	public GameObject palmtree;
	public GameObject closet;
	// GameObjects für Items
	public GameObject axt;
	public GameObject klo;
	public GameObject lazor;
	public GameObject wurst;
	public GameObject batterie;
	public GameObject kaffee;
	public GameObject banane;
	// GameObjects für Gegner
	public GameObject ersti;
	public GameObject zwölfti;
	public GameObject prof;
	// GameObject für Multiversumsmaschine
	public GameObject multiversum;

	private Transform boardHolder; 	// Speicherort für die Position des GameObjects
	private Transform roomHolder;
	private List <Vector3> gridPositions = new List <Vector3>(); // Liste aller Orte für Tiles
	
	// Listen und Arrays für die Speicherung der Rauminhalte
	List <int[,]> layouts;					// Liste an Layouts --> zu füllen aus Layout-Klasse
	int[,] currentLayout;					// Array des aktuellen Layouts
	List <List<int[,]>> roomTypes;		// Raumtypen-Liste aus Räumen --> zu füllen aus Raum-Klasse
	List <int[,]> rooms;					// Raum-Liste aus Item-Positionen 
	int[,] itemPositions = new int[10,10];	// Arrays mit Positionen der Rauminhalte

	public void placeMultiversumsMaschine(){
		int[,] room = stage.getCurrentRoom ();
		room [4, 0] = 25;
		int number = stage.getCurrentRoomNumber ();
		stage.currentRooms[number] = room;
	}

	// Generiert das Board aus Boden und Wänden
	public void BoardSetup(){
		boardHolder = new GameObject("Board").transform;
		// Feststellen, ob Räume anliegend sind und dementsprechend Türen zu generieren sind
		stage = GetComponent<Stage>();
		currentLayout = stage.getCurrentLayout();
		print ("CurrentLayout: " + currentLayout[0,0] + "," + currentLayout[0,1] + "," + currentLayout[0,2] + "," + currentLayout[0,3] + "," + currentLayout[0,4] + "," + currentLayout[0,5] + "," + currentLayout[0,6] + "," + currentLayout[0,7] + "," + currentLayout[0,8] + "," + currentLayout[0,9] + "," + currentLayout[0,10] + "," + currentLayout[0,11]);
		print ("CurrentLayout: " + currentLayout[1,0] + "," + currentLayout[1,1] + "," + currentLayout[1,2] + "," + currentLayout[1,3] + "," + currentLayout[1,4] + "," + currentLayout[1,5] + "," + currentLayout[1,6] + "," + currentLayout[1,7] + "," + currentLayout[1,8] + "," + currentLayout[1,9] + "," + currentLayout[1,10] + "," + currentLayout[1,11]);
		print ("CurrentLayout: " + currentLayout[2,0] + "," + currentLayout[2,1] + "," + currentLayout[2,2] + "," + currentLayout[2,3] + "," + currentLayout[2,4] + "," + currentLayout[2,5] + "," + currentLayout[2,6] + "," + currentLayout[2,7] + "," + currentLayout[2,8] + "," + currentLayout[2,9] + "," + currentLayout[2,10] + "," + currentLayout[2,11]);
		print ("CurrentLayout: " + currentLayout[3,0] + "," + currentLayout[3,1] + "," + currentLayout[3,2] + "," + currentLayout[3,3] + "," + currentLayout[3,4] + "," + currentLayout[3,5] + "," + currentLayout[3,6] + "," + currentLayout[3,7] + "," + currentLayout[3,8] + "," + currentLayout[3,9] + "," + currentLayout[3,10] + "," + currentLayout[3,11]);
		for(int r = 0; r < 12; r++){
			for(int c = 0; c < 12; c++){
				if(currentLayout[r,c] == 8){
					currentLayoutRow = r;
					currentLayoutColumn = c;
					print (currentLayoutRow + " " + currentLayoutColumn);
					if(currentLayoutRow != 0 && currentLayout[currentLayoutRow-1,currentLayoutColumn] != 0){
						doorTop = true;
						//print ("Door Top");
					}
					if(currentLayoutColumn != 0 && currentLayout[currentLayoutRow, currentLayoutColumn-1] != 0){
						doorLeft = true;
						//print ("Door Left");
					}
					if(currentLayoutRow != 11 && currentLayout[currentLayoutRow+1, currentLayoutColumn] != 0){
						doorBottom = true;
						//print ("Door Bottom");
					}
					if(currentLayoutColumn != 11 && currentLayout[currentLayoutRow, currentLayoutColumn+1] != 0){
						doorRight = true;
						//print ("Door Right");
					}
				}
			}
		}
		// Raum aus Wänden, Türen und Boden generieren
		for(int i = -1; i <= columns; i++){ // Laufe alle Spalten..
			float xvec = i*0.9f;
			for(int j = -1; j <= rows; j++){ // ..und Zeilen durch und..
				float yvec = j*0.9f;
				int rot = 0;
				GameObject toInstantiate = floorTiles[Random.Range (0, floorTiles.Length-1)]; // ..fülle diese mit Boden-Prefabs
				if (i == -1){ 			// wenn das aktuelle Feld in der Spalte ganz links ist
					rot = 270; 			// Rotation
					toInstantiate = wallTiles[Random.Range(0, wallTiles.Length-1)]; // Belege dies mit einem Wand-Prefab
					if(j == -1){ // Ecke unten links
						rot = 270;
						toInstantiate = edgeTile;
					}
					else if(j == rows){ // Ecke oben links
						rot = 0;
						toInstantiate = edgeTile;
					}
					else if(j == 5 && doorLeft){ // Tür links
						rot = 270;
						toInstantiate = doorTile;
					}
				}
				else if(i == columns){ 	// wenn das aktuelle Feld in der Spalte ganz rechts ist
					rot = 90;
					toInstantiate = wallTiles[Random.Range(0, wallTiles.Length-1)];
					if(j == rows){ // Ecke oben rechts
						rot = 90;
						toInstantiate = edgeTile;
					}
					else if(j == -1){ // Ecke unten rechts
						rot = 180;
						toInstantiate = edgeTile;
					}
					else if(j == 4 && doorRight){ // Tür rechts
						rot = 90;
						toInstantiate = doorTile;
					}
				}
				else if(j == -1){ 		// wenn das aktuelle Feld in der Reihe ganz unten ist
					rot = 180;
					toInstantiate = wallTiles[Random.Range(0, wallTiles.Length-1)];
					if(i == 4 && doorBottom){ // Tür unten
						rot = 180;
						toInstantiate = doorTile;
					} else {
						toInstantiate = wallTiles[Random.Range (0, wallTiles.Length-1)];
					}
				} 
				else if(j == rows){		// wenn das aktuelle Feld in der Reihe ganz oben ist
					toInstantiate = wallTiles[Random.Range(0, wallTiles.Length-1)];
					if(i == 5 && doorTop){ // Tür oben
						toInstantiate = doorTile;
					}
				}
				GameObject instance = Instantiate (toInstantiate, new Vector3 (xvec,yvec,0f), Quaternion.AngleAxis(rot, Vector3.back)) as GameObject; // Klonen des Objektes
				instance.transform.SetParent(boardHolder);	// Zusammenführen aller Objekte im boardHolder

			}
		}
		doorTop = false;
		doorBottom = false;
		doorRight = false;
		doorLeft = false;
	}


	public void RoomSetup(){
		roomHolder = new GameObject("Room").transform;
		stage = GetComponent<Stage> ();
		int[,] room = stage.getCurrentRoom();			// aktuellen Raum abrufen
		for(int i = 0; i < 10; i++){ 					// Zeilen
			float xvec = i*0.9f;						// Raumposition x ermitteln 
			for (int j = 0; j < 10; j++){ 				// Spalten
				float yvec = j*0.9f;					// Raumposition y ermitteln
				GameObject toInstantiate = null;
				if(room[i,j] == 1){						// Folgend: Auslesen des Arrays des aktuellen Raumes
					toInstantiate = table;				// und darauf basierendes Füllen des Raumes
				} else if (room[i,j] == 2){
					toInstantiate = tableLamp;
				} else if (room[i,j] == 3){
					toInstantiate = tableLaptop;
				} else if (room[i,j] == 4){
					toInstantiate = tableLampLaptop;
				} else if (room[i,j] == 5){
					toInstantiate = palmtree;
				} else if (room[i,j] == 6){
					toInstantiate = closet;
				} else if (room[i,j] == 21){
					toInstantiate = axt;
				} else if (room[i,j] == 22){
					toInstantiate = klo;
				} else if (room[i,j] == 23){
					toInstantiate = wurst;
				} else if (room[i,j] == 24){
					toInstantiate = lazor;
				} else if (room[i,j] == 25){
					toInstantiate = multiversum;
				} else if (room[i,j] == 51){
					toInstantiate = banane;
				} else if (room[i,j] == 52){
					toInstantiate = kaffee;
				} else if (room[i,j] == 71){
					toInstantiate = batterie;
				} else if (room[i,j] == 81) {
					toInstantiate = ersti;
				} else if (room[i,j] == 82) {
					toInstantiate = zwölfti;
				} else if (room[i,j] == 83){
					toInstantiate = prof;
				}
				if(toInstantiate != null){
					GameObject instance = Instantiate (toInstantiate, new Vector3(xvec,yvec,0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(roomHolder);
				}
			}
		}
	}
}