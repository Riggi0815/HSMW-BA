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
        waveData.explosionEnemyPrefab.GetComponent<Enemy>().Setup(waveData.eEMaxHealth, waveData.eESpeed, waveData.eEAttackDamage, 0, 0);
        waveData.shootingEnemyPrefab.GetComponent<Enemy>().Setup(waveData.shEMaxHealth, waveData.shESpeed, waveData.shEAttackDamage, 0, waveData.shEProjectileSpeed);
        waveData.sprayEnemyPrefab.GetComponent<Enemy>().Setup(waveData.spEMaxHealth, waveData.spESpeed, waveData.spEAttackDamage, waveData.spEAttackSpeed, waveData.spEProjectileSpeed);

        for (int i = 0; i < waveData.explosionEnemyCount; i++)
        {
            enemiesToSpawn.Add(waveData.explosionEnemyPrefab);
        }

        for (int i = 0; i < waveData.shootingEnemyCount; i++)
        {
            enemiesToSpawn.Add(waveData.shootingEnemyPrefab);
        }

        for (int i = 0; i < waveData.sprayEnemyCount; i++)
        {
            enemiesToSpawn.Add(waveData.sprayEnemyPrefab);
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
