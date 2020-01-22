using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public enum GameState 
	{
		PREPARATION,
		SPAWNWAVE,
		FIGHT,
		PAUSED
	}

	public PlayerStats player;

	public ICreature[] creaturesInScene;

	public GameState currentState;

	public static GameManager _instance = null;

	public int WaveLevel = 0;

	float spawnRate;

	public float waveCountDown;	


	public void Test()
	{
		Debug.Log ("Clicked!");
	}
	void Awake()
	{
		// Make object Singleton
		Singleton ();

		// Instantiate classes, fine GOs
		Init ();

		waveCountDown = 3.5f;

	}

	/// <summary>
	/// PREPARATION: Player is able to ready himself before the wave start, craft items etc.
	/// 			 When ready, he pushed the READY button
	/// SPAWNWAVE: When the button is pushed, the wave starts to spawn Creatures. That's the SPAWNWAVE 
	/// 		   state. We define how many we want
	/// FIGHT: When all creatures are spawned into the scene, we change current state to Fight
	/// 	   In that state we check when the player has killed all the creatures in the scene and
	/// 	   change state back to PREPARATION
	/// </summary>
	void Update()
	{
		player.PlayerLevelUp ();
		
		FindCreaturesInScene ();

		switch (currentState) 
		{
			case GameState.PREPARATION:
				PreparationState ();
                // Debug.Log(currentState);
				break;

			case GameState.SPAWNWAVE:
				StartWave ();
                // Debug.Log(currentState);
                break;

			case GameState.FIGHT: 
				FightWave ();
                // Debug.Log(currentState);
                break;

			default:
				break;
		}
		
		AddedToBestiary ();
	}

	void StartWave()
	{
        SpawnManager._instance.SpawnEnded = false;

        // 3..2..1..GO!
        waveCountDown -= Time.deltaTime;
        SpawnManager._instance.StartWaveLowLevel();

        // If we reach the amount of creatures spawned, stop spawning and go to Fight state
        if (SpawnManager._instance.SpawnEnded)
        {
            currentState = GameState.FIGHT;
        }

        //// SpawnRate is the amount of seconds in beween each spawning
        //spawnRate += Time.deltaTime;

        //// Start wave and iterate for every spawnRate(seconds) defined
        //if(spawnRate > 5)
        //{
        //	SpawnManager._instance.StartWaveLowLevel ();
        //	howManySpawned += 1;
        //	spawnRate = 0;
        //}

        //// If we reach the amount of creatures spawned, stop spawning and go to Fight state
        //if (howManySpawned == 3) 
        //{
        //	currentState = GameState.FIGHT;
        //}

    }

	void FightWave()
	{
		// If all creatures are killed, end wave and go to preparation state
		if(creaturesInScene.Length == 0)
			currentState = GameState.PREPARATION;				
	}

	void PreparationState()
	{
		// Make the READY button visible
		GUIManager._instance.ReadyButton.gameObject.SetActive (true);

		// Reset counter for next wave
		waveCountDown = 3.5f;

	}

	// Find all creatures in the scene and add them to an array
	void FindCreaturesInScene()
	{
		creaturesInScene = Object.FindObjectsOfType<ICreature> ();
	}

	void AddedToBestiary ()
	{
		foreach (var creature in creaturesInScene) {
			if (creature.AddedToBestiary == true) {
				player.AddCreatureToBestiary (creature);
				creature.AddedToBestiary = false;
			}
		}
	}

	void Init ()
	{
		// Instantiate the player
		player = new PlayerStats ("Dean", 1, 20, 10, 0, 25, 10);
		// Instantiate player's bestiary
		GameManager._instance.player.CreatureBestiary = new List<ICreature> ();
		// Initial GameState when game starts
		currentState = GameState.PREPARATION;

	}

	void Singleton ()
	{
		if (_instance == null)
			_instance = this;
		else
			if (_instance != this)
				Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}






}
