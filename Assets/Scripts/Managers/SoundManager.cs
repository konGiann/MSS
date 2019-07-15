using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager _instance;

	AudioSource[] audios;

	[HideInInspector]
	public AudioSource Preparation;
	[HideInInspector]
	public AudioSource SpawnWave;
	[HideInInspector]
	public AudioSource CreatureSpawned;
	[HideInInspector]
	public AudioSource CreatureDied;
	[HideInInspector]
	public AudioSource ItemUnlocked;
	[HideInInspector]
	public AudioSource ItemPlaced;


	void Awake () 
	{
		if (_instance == null)
			_instance = this;
		else if(_instance != null)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);	

		audios = GetComponents <AudioSource> ();
		Preparation = audios [0];
		SpawnWave = audios [1];
		CreatureSpawned = audios [2];
		CreatureDied = audios [3];
		ItemUnlocked = audios [4];
		ItemPlaced = audios [5];
	}

	void Update () 
	{
		switch (GameManager._instance.currentState) {
		case GameManager.GameState.PREPARATION:
			if (!Preparation.isPlaying)
				Preparation.Play ();
			SpawnWave.Stop ();
			break;
		case GameManager.GameState.SPAWNWAVE:
			if (!SpawnWave.isPlaying)
				SpawnWave.Play ();
			Preparation.Stop ();
			break;
		default:
			break;
		}

	}




}
