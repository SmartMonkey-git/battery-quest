using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioSource efxSource;
	public AudioSource musicSource;
	public AudioSource gameOverSource;

	public static SoundManager instance = null;

	void Awake(){
		if (instance == null){
			instance = this;
		} else if (instance != this){
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
		musicSource.volume = 0.5f;
	}

	public void PlaySingle(AudioClip clip){
		efxSource.clip = clip;
		efxSource.Play ();
	}
	
	public void RandomizeSfx (params AudioClip[] clips){
		int randomIndex = Random.Range (0, clips.Length);
		efxSource.clip = clips [randomIndex];
		efxSource.Play ();
	}

	public void GameOverRandomizeSfx (params AudioClip[] clips){
		int randomIndex = Random.Range (0, clips.Length);
		gameOverSource.clip = clips [randomIndex];
		gameOverSource.Play ();
	}
}
