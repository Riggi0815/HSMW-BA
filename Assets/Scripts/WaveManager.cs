using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public static WaveManager Instance;

    //Singleton Check
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] WaveScriptable[] waves;

    [SerializeField] float beforeFirstWaveTime;

    [SerializeField] float betwenWaveTime;
    [SerializeField] List<GameObject> enemiesToSpawn = new List<GameObject>();

    [SerializeField] int currentWave = 0;

    public delegate void NewWave(List<GameObject> enemies, float spawnTime);
    public event NewWave OnNewWave;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("SpawnNewWave", beforeFirstWaveTime);
    }

    public void SpawnNewWave()
    {
        WaveScriptable waveData = waves[currentWave];
        

        foreach (EnemyNumber enemyNumber in waveData.enemies)
        {
            for (int i = 0; i < enemyNumber.enemyCount; i++)
            {
                enemiesToSpawn.Add(enemyNumber.enemyPrefab);
            }
        }

        OnNewWave?.Invoke(enemiesToSpawn, waveData.spawnTime);
        currentWave++;
        enemiesToSpawn.Clear();
    } 

    public void PrepareNextWave()
    {
        if (currentWave < waves.Length)
        {
            Invoke("SpawnNewWave", betwenWaveTime);
        }
        else
        {
            Debug.Log("All Waves Cleared");
        }
    }
}
