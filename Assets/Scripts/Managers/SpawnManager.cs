using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager _instance = null;

    public Transform[] SpawnLocation;

    public GameObject[] LowLevelEnemies;

    float SpawnRate;
    
    int howManySpawned;

    public bool SpawnEnded;

    int randomLaneIndex;
    int randomEnemy;

    private IEnumerator _startwave;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance == null)
            _instance = this;
        else if (_instance != null)
            Destroy(gameObject);

    }

    #region Spawner: Level One Enemies
    public void StartWaveLowLevel()
    {        
        SpawnRate += Time.deltaTime;
        

        if (SpawnRate >= 5 && !SpawnEnded)
        {
            randomLaneIndex = Random.Range(0, LowLevelEnemies.Length - 1);
            randomEnemy = Random.Range(0, LowLevelEnemies.Length - 1);

            Instantiate(LowLevelEnemies[randomEnemy], new Vector3(SpawnLocation[randomLaneIndex].position.x,
                SpawnLocation[randomLaneIndex].position.y - 0.05f,
                SpawnLocation[randomLaneIndex].position.z), Quaternion.identity);
            SoundManager._instance.CreatureSpawned.Play();

            SpawnRate = 0;
            howManySpawned += 1;
            if (howManySpawned == 3)
                SpawnEnded = true;
        }
    }
    #endregion
}
