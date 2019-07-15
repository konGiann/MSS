using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public static SpawnManager _instance = null;

	public Transform[] SpawnLocation;

	public GameObject[] LowLevelEnemies;

	public float SpawnTime;

	int randomLaneIndex;
	int randomEnemy;

	private IEnumerator _startwave;

	void Awake () 
	{
		DontDestroyOnLoad (gameObject);	

		if (_instance == null)
			_instance = this;
		else if (_instance != null)
			Destroy (gameObject);
		
	}

	#region Spawner: Level One Enemies


	public void StartWaveLowLevel()
	{
		randomLaneIndex = Random.Range (0, LowLevelEnemies.Length -1 );
		randomEnemy = Random.Range (0, LowLevelEnemies.Length - 1);

		Instantiate (LowLevelEnemies[randomEnemy], new Vector3(SpawnLocation[randomLaneIndex].position.x, 
			SpawnLocation[randomLaneIndex].position.y - 0.05f, 
			SpawnLocation[randomLaneIndex].position.z ), Quaternion.identity);
		SoundManager._instance.CreatureSpawned.Play ();
	}

	#endregion
}
