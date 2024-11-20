using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Rooms : MonoBehaviour {

	/*		1-20 Obstacles
			1: Tisch
			2: Tisch mit Lampe
			3: Tisch mit Laptop
			4: Tisch mit Lampe und Laptop
			5: Palme
			6: Schrank
			
			21-50 Items:
			21: Gummiaxt
			22: Klopapier
			23: Fleischwurst
			24: Laz0r
			25: Multiversumsmaschine
			
			
			51-70 Essen:
			51: Banane
			52: Kaffee
			
			
			71-80 Batterien:
			71: Batterie

			
			81-90:
			81: Ersti
			82: Zwölfti
			83: Prof
			
			99: Türplatzhalter

	*/

	public List <int[,]> rooms = new List<int[,]>();			// Liste aus normalen Räumen
	public List<int[,]> batteryRooms = new List<int[,]>();		// Liste aus Räumen mit Batterie
	public List<int[,]> itemRooms = new List<int[,]>();			// Liste aus Räumen mit Items
	public List<int> roomCounter = new List<int>();				// Zähler für die 12 zu füllenden Räume

	int[,] r1 = new int[,] {{ 0, 0, 0, 0, 0,99, 4, 0, 0, 0} ,	//Raum mit: 
							{ 0, 0,52, 0, 0, 0, 2, 0, 0, 0} ,	// Items: 	1 Laz,
							{ 0, 0, 0, 0, 0, 0, 1,24, 5, 0} ,	// Essen:	1 Kaf, 1 Ban
							{ 0, 0, 0, 0, 0, 0, 4, 2, 0, 0} ,	// Batterie:	1
							{99, 0, 0, 3,83, 0, 0, 0, 0, 6} ,	// Gegner:	1 E, 1 Z, 1 P
							{ 5, 5, 0, 2, 0, 5, 0, 0, 0,99} ,
							{71, 2, 0, 4, 0, 0, 5,81, 0, 6} ,
							{ 0, 4, 0, 0, 0, 0, 0, 0, 0, 6} ,
							{ 0, 1,82, 0, 0, 0, 0, 0, 0, 6} ,
							{ 0, 0, 0, 4,99, 5,51, 0, 0, 6}} ;
	
	
	int[,] r2 = new int[,] {{51, 0, 0, 0, 0,99, 1, 0,22, 5} ,	// Raum mit:
							{ 0, 0, 0, 0, 0, 0, 3, 0, 3, 0} ,	// Items:	1 Axt, 1 Klo
							{ 0, 0,81, 0, 0, 0, 2, 0, 1, 0} ,	// Essen:	2 Ban
							{ 0, 0, 0, 4, 0, 0, 0, 0, 0, 0} ,	// Batterie:	0
							{99, 0, 0,21, 2, 0, 0, 0, 6, 0} ,	// Gegner:	1 E, 1 P
							{ 0, 0, 0, 0, 5,83, 0, 0, 6,99} ,
							{ 0, 0, 0, 2, 0, 0,51, 0, 6, 0} ,
							{ 0, 0, 0, 1, 0, 0, 0, 0, 0, 0} ,
							{ 0, 0, 0, 0, 0, 0, 6, 0, 0, 0} ,
							{ 0, 0, 0, 0,99, 0, 6, 0, 0, 0}} ;
	
	
	int[,] r3 = new int[,] {{ 5, 0, 0, 0, 0,99, 0, 0, 0, 5} ,	// Raum mit:
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,	// Items:	1 Wur
							{ 0, 0, 0, 2, 4, 3, 0, 0, 0, 0} ,	// Essen:	1 Kaf
							{ 1, 3, 0, 0, 0, 0,83, 6, 0, 0} ,	// Batterie:	1
							{99, 2, 0, 0,23, 0, 0, 6, 0, 0} ,	// Gegner:	1 P
							{ 0, 0, 0, 0,71, 0, 0, 6, 0,99} ,
							{ 0, 5, 0, 0, 0, 0, 0, 0, 0, 0} ,
							{ 0, 2, 0, 0, 2, 1, 3, 0, 0, 0} ,
							{ 0,52, 4, 0, 0, 0, 0, 0, 3, 0} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
						
	
	int[,] r4 = new int[,] {{ 0, 0, 0, 0, 0,99, 0, 0, 0, 0} ,	// Raum mit:
							{ 0, 0, 0, 5, 1, 2, 3, 0, 0, 0} ,	// Items:	1 Axt
							{ 0, 0, 0,81, 0, 0,81, 0, 0, 0} ,	// Essen:	1 Ban, 1 Kaf
							{ 0, 5, 0, 5, 1, 0, 5, 0, 5, 0} ,	// Batterie:	1
							{99, 2, 0, 0,71,52, 6, 0, 6, 0} ,	// Gegner:	2 E, 1 P
							{ 0, 1, 0, 1,51,21, 0, 0, 6,99} ,
							{ 0, 3, 0, 3, 0, 1, 4, 0, 6, 0} ,
							{ 0, 0,83, 0, 0, 0, 0, 0, 0, 0} ,
							{ 0, 0, 0, 1, 3, 4, 2, 0, 0, 0} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
	
	
	int[,] r5 = new int[,] {{22, 0, 0, 0, 0,99, 0, 0, 0, 0} ,	// Raum mit:
							{ 0, 0, 0, 0, 0, 0, 3, 0, 0, 0} ,	// Items:	1 Klo, 1 Wur
							{ 0, 0, 1, 0, 0, 0, 6, 0, 0, 0} ,	// Essen:	1 Kaf, 1 Ban
							{ 0, 0, 4, 0,82, 0, 6, 0, 0, 0} ,	// Batterie:	1
							{99, 0, 1, 0, 0, 0, 6, 0,21, 0} ,	// Gegner:	1 E, 2 Z
							{ 0,52, 2, 0,23, 0, 5,71, 0,99} ,
							{ 0,81, 4, 0, 0, 0, 6, 0, 0, 0} ,
							{ 0, 0, 3, 0, 0,82, 5, 0, 0, 0} ,
							{ 0, 0, 2, 0, 0, 0, 0, 0, 0, 0} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
	
	
	int[,] r6 = new int[,] {{ 0, 0, 0, 0, 0,99, 5, 0, 0, 0} ,	// Raum mit:
							{ 0, 0, 0, 0, 0, 0, 5, 0, 0, 0} ,	// Items:	0
							{ 0, 0, 1, 3, 4, 0, 0, 0, 5, 0} ,	// Essen:	0
							{ 0, 0, 0, 0, 3, 0, 0, 0, 6, 0} ,	// Batterie:	0
							{99, 0, 0, 0, 0, 0, 0, 0, 0, 6} ,	// Gegner:	1 E
							{ 0, 0, 0, 0,81, 0, 0, 0, 0,99} ,
							{ 0, 0, 5, 0, 0, 0, 3, 0, 0, 0} ,
							{ 0, 0, 0, 1, 0, 4, 0, 0, 6, 0} ,
							{ 0, 0, 0, 0, 2, 0, 0, 0, 0, 6} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
						
	
	int[,] r7 = new int[,] {{ 0, 0, 0, 0, 6,99, 1, 0, 0, 0} ,	// Raum mit:
							{ 0, 0, 0, 0, 6, 0, 5, 0, 0, 0} ,	// Items:	0
							{ 0, 0, 0, 0, 6, 0, 6, 0, 0, 0} ,	// Essen:	1 Ban
							{ 1, 5, 2, 5, 6, 0, 6, 0, 0, 0} ,	// Batterie:	0
							{99, 0, 0, 0, 0, 0, 3, 1, 5, 4} ,	// Gegner:	1 E
							{ 3, 5, 3, 5,51,81, 0, 0, 0,99} ,
							{ 0, 0, 0, 3, 0, 6, 5, 5, 3, 1} ,
							{ 0, 0, 0, 2, 0, 4, 0, 0, 0, 0} ,
							{ 0, 0, 0, 2, 0, 3, 0, 0, 0, 0} ,
							{ 0, 0, 0, 1,99, 2, 0, 0, 0, 0}} ;
	
	
	int[,] r8 = new int[,] {{ 0, 0, 0, 0, 0,99, 2, 0, 0, 0} ,	// Raum mit:
							{ 0,51, 0, 0, 0, 0, 0, 0, 5, 0} ,	// Items:	0
							{ 0, 0, 5, 0, 0, 0, 1, 0, 0, 0} ,	// Essen:	2 Kaf, 1 Ban
							{ 0, 0, 0, 0, 6, 0, 0, 0, 1, 0} ,	// Batterie:	0
							{99, 0, 3, 0,52, 0, 2, 0, 0, 0} ,	// Gegner:	1 Z
							{ 0, 0, 0, 0, 3, 0, 0, 0, 1,99} ,
							{ 0, 0, 1, 0, 0,82, 6, 0, 0, 0} ,
							{ 0, 0, 0, 0, 2, 0, 0,51, 5, 0} ,
							{ 0, 0, 4, 0, 0, 0, 5, 0, 0, 0} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
	
	
	int[,] r9 = new int[,] {{ 0, 0, 0, 0, 3,99, 0, 0, 0, 0} ,	// Raum mit:
							{ 0, 0, 5, 0, 1, 0, 0, 0, 0, 0} ,	// Items:	1 Wur
							{ 0,71, 2,83, 4, 1, 5, 5, 0, 0} ,	// Essen:	1 Ban
							{ 3, 5, 2, 0, 0, 0, 0, 0, 0, 0} ,	// Batterie:	1
							{99, 0, 0, 0, 0, 3, 1, 4, 5, 5} ,	// Gegner:	1 E, 1 P
							{ 0, 0, 0, 0, 0, 4, 0, 0, 0,99} ,
							{ 0, 2, 3, 3, 0, 1, 0, 3, 5, 6} ,
							{ 0, 5,23, 3, 0, 2, 0, 2,51, 0} ,
							{ 0, 0, 0, 5, 0, 0, 0, 6,81, 0} ,
							{ 0, 0, 0, 2,99, 0, 0, 0, 0, 0}} ;
	
	
	int[,] r10 = new int[,]{{ 0, 0, 0, 0, 0,99, 0, 0, 0,83} ,	// Raum mit:
							{ 0, 0, 5, 5, 3, 2, 2, 2, 0, 0} ,	// Items:	1 Axt
							{ 0, 1, 0, 0, 0, 0, 0, 0, 5, 0} ,	// Essen:	0
							{ 0, 2, 0, 4, 5, 5, 1, 0, 0, 0} ,	// Batterie:	1
							{99, 2, 0, 1,21,81, 0, 5, 0, 0} ,	// Gegner:	1 E, 1 P
							{ 0, 6, 0, 3,71, 6, 0, 6, 0,99} ,
							{ 0, 5, 0, 0, 5, 6, 0, 6, 0, 0} ,
							{ 0, 0, 2, 0, 0, 0, 0, 4, 0, 0} ,
							{ 0, 0, 0, 2, 5, 2, 3, 0, 0, 0} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
	
	
	int[,] r11 = new int[,]{{ 0, 0, 0, 0, 0,99, 0, 0, 0, 0} ,	// Raum mit:
							{ 0, 0, 1, 0,81, 0, 0, 0, 6, 0} ,	// Items:	0
							{ 0, 0, 0, 1, 0, 0, 0, 5, 0, 0} ,	// Essen:	0
							{ 0, 0, 0, 0, 2, 0, 5, 0, 0, 0} ,	// Batterie:	0
							{99, 0, 0, 0, 0, 3, 0, 0, 0, 0} ,	// Gegner:	2 E
							{ 0, 0, 0, 0, 6, 0, 4, 0, 0,99} ,
							{ 0, 0, 0, 6, 0, 0, 0, 1, 0, 0} ,
							{ 0, 0, 2, 0, 0, 0, 0, 0, 5, 0} ,
							{ 0, 1, 0, 0, 0,81, 0, 0, 0, 0} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
	
	
	int[,] r12 = new int[,]{{24, 0, 0, 0, 0,99, 0, 0, 5,71} ,	// Raum mit:
							{ 2, 2, 1, 4, 5, 0, 0, 0, 6,81} ,	// Items:	1 Laz, 1 Klo
							{ 0, 0, 0, 0, 0, 0, 0, 0, 6, 0} ,	// Essen:	1 Ban
							{ 0, 0, 0, 0, 0,83, 0, 0, 6, 0} ,	// Batterie:	1
							{99, 0,82, 0, 0, 0, 0, 0, 6, 0} ,	// Gegner:	2 E, 1 Z, 1 P
							{ 0, 4, 0, 0, 0, 0, 0, 0, 0,99} ,
							{ 0, 1, 0, 0, 0, 0, 0, 0, 0, 0} ,
							{ 0, 2, 0, 0, 0, 0,81, 0, 0, 0} ,
							{ 0, 2, 0, 0, 0, 5, 1, 3, 1, 6} ,
							{22, 4, 0, 0,99, 0, 0, 0, 0,51}} ;
	
	
	
	int[,] r13 = new int[,]{{ 0, 0, 0, 0, 0,99, 0, 0, 0,81} ,	// Raum mit:
							{ 0, 1, 3, 4, 0, 0, 0, 0, 0, 0} ,	// Items:	0
							{ 0, 0, 0, 1, 0, 0, 3, 5, 6, 0} ,	// Essen:	1 Ban, 1 Kaf
							{ 0, 0, 0, 5, 0, 0, 2, 0, 0, 0} ,	// Batterie:	0
							{99, 0, 1, 0,82, 0, 1, 0, 0, 0} ,	// Gegner:	1 E, 1 Z
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0,99} ,
							{ 0, 0, 4, 0, 0, 0, 4, 6, 5, 0} ,
							{ 0, 0,51, 3, 0, 0, 0,52, 1, 0} ,
							{ 0, 0, 0, 1, 0, 0, 0, 0, 3, 0} ,
							{ 0, 0, 0, 1,99, 0, 0, 0, 4, 0}} ;
	
	
	
	
	int[,] r14 = new int[,]{{ 0, 0, 0, 0, 0,99, 0, 0, 0, 0} ,	// Raum mit:
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,	// Items:	0
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,	// Essen:	1 Ban
							{ 0, 0, 0, 0, 4, 5, 2, 0, 0, 0} ,	// Batterie:	0
							{99, 0, 0, 0, 0,51, 4, 0, 0, 0} ,	// Gegner:	1 E
							{ 0, 0, 0, 0, 2, 5, 6, 0, 0,99} ,
							{ 0, 0, 0,81, 0, 0, 0, 0, 0, 0} ,
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
	
	
	
	
	
	int[,] r15 = new int[,]{{ 0, 0, 0, 0, 0,99, 0, 0, 0, 0} ,	// Raum mit:
							{ 0, 0, 0, 2, 1, 4, 0, 0,81, 0} ,	// Items:	0
							{ 0, 0, 0, 3, 0, 0, 4, 0, 0, 0} ,	// Essen:	1 Ban, 1 Kaf
							{ 0, 1, 4, 1, 0, 0, 3, 0, 0, 0} ,	// Batterie:	0
							{99, 0, 0, 0, 0, 0, 2, 1, 1, 0} ,	// Gegner:	2 E
							{ 0, 2, 1, 0,51, 0, 0, 0, 0,99} ,
							{ 0, 0, 0, 5, 0, 0, 2, 4, 5, 0} ,
							{ 0, 0, 0, 6, 0, 0, 1, 0, 0, 0} ,
							{ 0,81, 0, 1, 4, 6, 1, 0,52, 0} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
	
	
	
	
	
	int[,] r16 = new int[,]{{ 0, 0, 0, 0, 0,99, 0, 0, 0, 0} ,	// Raum mit:
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,	// Items:	0
							{ 0, 0, 0, 0, 3, 1, 0, 0, 0, 0} ,	// Essen:	1 Kaf
							{ 0, 0, 1, 0, 0, 0, 0, 1, 0, 0} ,	// Batterie:	0
							{99, 0, 0, 4,82, 0, 5, 0, 0, 0} ,	// Gegner:	1 Z
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0,99} ,
							{ 0, 0, 0, 3,52, 0, 6, 0, 0, 0} ,
							{ 0, 0, 1, 0, 0, 0, 0, 1, 0, 0} ,
							{ 0, 0, 0, 0, 2, 4, 0, 0, 0, 0} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
	
	
	int[,] r17 = new int[,]{{ 5, 0, 0, 0, 0,99, 0, 0, 0, 1} ,	// Raum mit:
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,	// Items:	0
							{ 0, 4, 0, 0, 0, 0, 0, 3, 0, 0} ,	// Essen:	1 Ban
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,	// Batterie:	0
							{99, 0, 0, 1, 0, 6, 0, 0, 0, 0} ,	// Gegner:	1 E
							{ 0, 0, 0, 0,51, 0, 0, 0, 0,99} ,
							{ 0, 0,81, 2, 0, 4, 0, 0, 0, 0} ,
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,
							{ 0, 3, 0, 0, 0, 0, 0, 6, 0, 0} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
	
	
	
	
	
	int[,] r18 = new int[,]{{ 0, 0, 0, 0, 0,99, 0,81, 0,21} ,	// Raum mit:
							{ 0, 0, 0, 0, 0, 0, 1, 5, 4, 0} ,	// Items:	1 Axt
							{ 0, 0, 0, 0, 0, 0, 0, 0, 2, 0} ,	// Essen:	1 Kaf
							{ 0, 0, 0,81, 0, 0, 0, 0, 1, 0} ,	// Batterie:	0
							{99, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,	// Gegner:	4 E
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0,99} ,
							{ 0, 6, 0, 0, 0, 0, 0, 0, 0, 0} ,
							{ 0, 3, 0, 0, 0, 0,81, 0, 0, 0} ,
							{81, 5, 2, 2, 0, 0, 0, 0, 0, 0} ,
							{52, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
	
	
	
	
	int[,] r19 = new int[,]{{ 0, 0, 0, 0, 0,99, 0, 0, 0, 0} ,	// Raum mit:
							{ 0, 0,51, 0, 0, 4, 0, 0, 0, 0} ,	// Items:	0
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,	// Essen:	1 Ban
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,	// Batterie:	0
							{99, 3, 0, 4, 0, 5, 0, 0, 0, 0} ,	// Gegner:	1 Z
							{ 0, 0, 0, 0, 0, 0, 0, 0, 1,99} ,
							{ 0, 0, 0, 0,82, 0, 0, 0, 0, 0} ,
							{ 0, 0, 3, 0, 0, 0, 1, 0, 0, 0} ,
							{ 0, 0, 0, 0, 6, 0, 0, 0, 0, 0} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
						
	
	
	
	
	int[,] r20 = new int[,]{{ 0, 0, 0, 0, 0,99, 0, 0, 0, 0} ,	// Raum mit:
							{ 0, 0, 0, 0, 4, 0, 0, 0, 0, 0} ,	// Items:	1 Wur
							{ 0,83, 0, 0, 5, 0, 0, 0, 0, 0} ,	// Essen:	1 Kaf, 1 Ban
							{ 0, 0, 0,52, 2, 0, 0, 0, 0, 0} ,	// Batterie:	0
							{99, 1, 3, 1, 2, 3, 5, 1, 6, 0} ,	// Gegner:	1 P
							{ 0, 0, 0,23, 1,51, 0, 0, 0,99} ,
							{ 0, 0, 0, 0, 6, 0, 0, 0, 0, 0} ,
							{ 0, 0, 0, 0, 3, 0, 0, 0, 0, 0} ,
							{ 0, 0, 0, 0, 1, 0, 0, 0, 0, 0} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
	
	
	
	
	
	int[,] r21 = new int[,]{{ 0, 0, 0, 0, 0,99, 0, 0, 0,51} ,	// Raum mit:
							{ 0, 0, 0, 2, 0, 0, 4, 0, 0, 0} ,	// Items:	0
							{ 0, 6, 0, 0, 0, 0, 0, 0,81, 6} ,	// Essen:	1 Ban
							{ 0, 0, 0, 0, 0, 0, 0, 2, 0, 0} ,	// Batterie:	0
							{99, 0, 0, 0, 0, 0, 0, 1, 0, 0} ,	// Gegner:	2 E
							{ 0, 0, 5, 0, 3, 0, 0, 0, 0,99} ,
							{ 0, 0, 0,81, 0, 0, 0, 0, 0, 0} ,
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,
							{ 0, 0, 5, 0, 0, 0, 4, 0, 0, 0} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;
	
	
	
	
	int[,] r22 = new int[,]{{ 0, 0, 0, 0, 0,99, 0, 0, 0, 0} ,	// Raum mit:
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,	// Items:	0
							{ 1, 0, 1, 3, 0, 4, 5, 0, 1, 4} ,	// Essen:	1 Ban, 1Kaf
							{ 0, 0, 0, 0, 0, 0,82, 0, 0, 0} ,	// Batterie:	0
							{99, 0, 0, 4,51, 2, 0, 0, 0, 0} ,	// Gegner:	1 E, 1 Z
							{ 0, 0, 0, 5,52, 3, 0, 0, 0,99} ,
							{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} ,
							{ 3, 5, 0, 1, 3, 0, 4, 3, 0, 2} ,
							{ 0, 0,81, 0, 0, 0, 0, 0, 0, 0} ,
							{ 0, 0, 0, 0,99, 0, 0, 0, 0, 0}} ;



	public List<int[,]> RoomGenerator(){
		List<int[,]> currentRooms = new List<int[,]>(); // Liste, zu füllen mit 12 Räumen
		batteryRooms.Add (r1);
		rooms.Add (r2);
		batteryRooms.Add (r3);
		batteryRooms.Add (r4);
		batteryRooms.Add (r5);
		rooms.Add (r6);
		rooms.Add (r7);
		rooms.Add (r8);
		batteryRooms.Add (r9);
		batteryRooms.Add (r10);
		rooms.Add (r11);
		batteryRooms.Add (r12);
		rooms.Add (r13);
		rooms.Add (r14);
		rooms.Add (r15);
		rooms.Add (r16);
		rooms.Add (r17);
		rooms.Add (r18);
		rooms.Add (r19);
		rooms.Add (r20);
		rooms.Add (r21);
		rooms.Add (r22);
		for(int i = 0; i <= 12; i++){
			print ("Raum Nummer: " + i);
			print ("Noch unbenutzte Räume: " + rooms.Count);
			roomCounter.Add (i);
			int rand = Random.Range (0, rooms.Count);
			currentRooms.Add(rooms [rand]);
			rooms.RemoveAt(rand);
		}

		for(int i = 0; i < 3; i++){
			int randBatteryRoom = Random.Range (0, batteryRooms.Count); 	// Wählt einen zufälligen Index eines Batterie-Raums
			print (i + " - Noch offene Batterie-Räume: " + batteryRooms.Count);
			int randRoomCounter = Random.Range (1, roomCounter.Count);		// Wählt einen zufälligen Index eines zu füllenden Raumes
			int randRoomNumber = roomCounter[randRoomCounter];				// Wandelt den Füllraum-Index in die darin befindliche Zahl um
			print ("Fülle Batterie-Raum in Index: " + randRoomNumber);
			currentRooms[randRoomNumber] = batteryRooms[randBatteryRoom];	// Speichert den Batterie-Raum in die Raumliste am gewählten Index
			batteryRooms.RemoveAt(randBatteryRoom);							// Löscht den ausgewählten Batterie-Raum aus der Liste
			roomCounter.RemoveAt(randRoomCounter);							// Löscht den ausgewählten Index aus der Liste
		}
		return currentRooms;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
