using UnityEngine;
using System;
using System.Collections.Generic;


// Klasse aller aktuell im Level zu speichernden Informationen
public class Stage : MonoBehaviour {
	public int[,] currentLayout;				// Array des momentanen Layouts/Map
	public List<int[,]> currentRooms;			// Liste aller Räume im aktuellen Level
	private int[,] currentRoom;					// Array des momentanen Raums
	public Playera player;
	public GameObject Player;

	
	BoardManager board;							// Objekt der Klasse Board
	Layouts layout;
	bool konstruktor = true;
	Rooms rooms;

	public void Start(){

	}

	public void Awake(){
		layout = GetComponent<Layouts>();
		board = GetComponent<BoardManager>();
		rooms = GetComponent<Rooms>();
		if (konstruktor = true) {
			this.currentLayout = layout.LayoutGenerator ();
			//print("Punkt 0 0 " + currentLayout[1,2]);
			konstruktor = false;
			this.currentRooms = rooms.RoomGenerator();
		}
	}

	// Aktuelles Layout-Array zurückgeben
	public int[,] getCurrentLayout(){
		//print ("Punkt in Methode " + this.currentLayout[1,2]);
		return this.currentLayout;
	}

	// Liste aller ausgewählten Räume zurückgeben
	public List<int[,]> getCurrentRooms(){
		return this.currentRooms;
	}

	// Layout-Array aktualisieren, wenn in den nächsten Raum gegangen wird
	public void updateRoom (){ 
		print ("updateRoom");
		Vector3 target = GameObject.FindGameObjectWithTag("Player").transform.localPosition;
		Vector3 links = new Vector3 (0.9f * 0, 0.9f * 5, 0f);
		Vector3 unten = new Vector3 (0.9f*4, 0.9f*0, 0f);
		Vector3 oben = new Vector3 (0.9f*5, 0.9f*9, 0f);
		Vector3 rechts = new Vector3 (0.9f*9, 0.9f*4, 0f);

		if (target == links){ // Wenn Tür links betreten wurde
			int[,] updatedLayout = this.currentLayout;
			for (int i = 0; i < 12; i++){
				for (int j = 0; j < 12; j++){
					if(updatedLayout[i,j] == 8){
						int c = j-1;
						updatedLayout[i,c] = 8;
						updatedLayout[i,j] = 1;
						this.currentLayout = updatedLayout;
						return;
					}
				}
			}
		}
		else if (target == unten){ // Wenn Tür unten betreten wurde
			int[,] updatedLayout = this.currentLayout;
			for (int i = 0; i < 12; i++){
				for (int j = 0; j < 12; j++){
					if(updatedLayout[i,j] == 8){
						int c = i+1;
						updatedLayout[c,j] = 8;
						updatedLayout[i,j] = 1;
						this.currentLayout = updatedLayout;
						return;
					}
				}
			}
		}
		else if(target == oben){
			int[,] updatedLayout = this.currentLayout;
			for (int i = 0; i < 12; i++){
				for (int j = 0; j < 12; j++){
					if(updatedLayout[i,j] == 8){
						int c = i-1;
						updatedLayout[c,j] = 8;
						updatedLayout[i,j] = 1;
						this.currentLayout = updatedLayout;
						return;
					}
				}
			}
		}
		else if (target == rechts){
			int[,] updatedLayout = this.currentLayout;
			for (int i = 0; i < 12; i++){
				for (int j = 0; j < 12; j++){
					if(updatedLayout[i,j] == 8){
						int c = j+1;
						print ("j = " + j);
						print ("j+1 = " + c);
						updatedLayout[i,c] = 8;
						updatedLayout[i,j] = 1;
						this.currentLayout = updatedLayout;
						return;
					}
				}
			}
		}
	} 

	public void updateItems(float x, float y){
		float x1 = x / 0.9f;
		float y1 = y / 0.9f;
		x1 = (float)Math.Round ((double)x1);
		y1 = (float)Math.Round ((double)y1);
		int zwzeile = (int)x1;
		int zwspalte = (int)y1;
		int zeile = zwzeile;
		int spalte = zwspalte;
		int[,] room = getCurrentRoom ();
		room [zeile, spalte] = 0;
		int number = getCurrentRoomNumber ();
		this.currentRooms[number] = room;
	}

	public int getCurrentRoomNumber(){
		int roomNumber = 0;
		int[,] theLayout = this.currentLayout;
		List<int[,]> allRooms = this.currentRooms;
		int n = 0;
		foreach (int r in theLayout) {
			if(r != 0){
				roomNumber++;
				if(r == 8){
					n = roomNumber;
					break;
				}
			}
		}
		return n;
	}

	// aktuelle Raumnummer zurückgeben
	public int[,] getCurrentRoom(){
		int roomNumber = 0;
		int[,] theLayout = this.currentLayout;
		List<int[,]> allRooms = this.currentRooms;
		int n = 0;
		foreach (int r in theLayout) {
			if(r != 0){
				roomNumber++;
				if(r == 8){
					n = roomNumber;
					break;
				}
			}
		}
		int[,] theCurrentRoom = allRooms[n];
		return theCurrentRoom;
	}
	

	
	// Erstellt das Level, ruft alle dazu nötigen Methoden auf
	public void SetupScene(){
		board = GetComponent<BoardManager>();
		board.BoardSetup ();
		board.placeMultiversumsMaschine();
		board.RoomSetup ();
	}
	
	
}