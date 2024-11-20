using UnityEngine;
using System.Collections;
using System;

public class Loader : MonoBehaviour {

	bool init = true;
	public GameObject gameManager;
	//public SoundManager soundManager;
	
	void Start ()
	{
		print ("load");
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (GameManager.instance == null)
			//Instantiate gameManager prefab
			Instantiate (gameManager);
		
			/*Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
			if (SoundManager.instance == null)
			
			//Instantiate SoundManager prefab
			Instantiate(soundManager);
			*/

	}
}


