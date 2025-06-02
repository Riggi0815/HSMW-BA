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
    [SerializeField] TMPro.TextMeshProUGUI waveText;
    [SerializeField] PlayerStats playerStats;
    public int CurrentWave
    {
        get { return currentWave; }
        set { currentWave = value; }
    }

    public delegate void NewWave(List<GameObject> enemies, float spawnTime);
    public event NewWave OnNewWave;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartSpawning()
    {
        Invoke("SpawnNewWave", beforeFirstWaveTime);
    }

    public void SpawnNewWave()
    {
        WaveScriptable waveData = waves[currentWave];
        waveData.explosionEnemyPrefab.GetComponent<Enemy>().Setup(waveData.eEMaxHealth, waveData.eESpeed, waveData.eEAttackDamage, 0, 0, 30, 7);
        waveData.shootingEnemyPrefab.GetComponent<Enemy>().Setup(waveData.shEMaxHealth, waveData.shESpeed, waveData.shEAttackDamage, 0, waveData.shEProjectileSpeed, 20, 4);
        waveData.sprayEnemyPrefab.GetComponent<Enemy>().Setup(waveData.spEMaxHealth, waveData.spESpeed, waveData.spEAttackDamage, waveData.spEAttackSpeed, waveData.spEProjectileSpeed, 10, 3);

        CSVWriter.Instance.WriteCSVLine(currentWave + 1, waveData.explosionEnemyPrefab.name, waveData.explosionEnemyCount, waveData.eEMaxHealth, waveData.eESpeed, waveData.eEAttackDamage, 0, 0, (int)GameManager.Instance.Player.GetComponent<PlayerStats>().Health, (int)GameManager.Instance.Player.GetComponent<PlayerStats>().CurrentHealth, (int)GameManager.Instance.Player.GetComponent<PlayerStats>().CurrentLvl);
        CSVWriter.Instance.WriteCSVLine(currentWave + 1, waveData.shootingEnemyPrefab.name, waveData.shootingEnemyCount, waveData.shEMaxHealth, waveData.shESpeed, waveData.shEAttackDamage, 0, waveData.shEProjectileSpeed, (int)GameManager.Instance.Player.GetComponent<PlayerStats>().Health, (int)GameManager.Instance.Player.GetComponent<PlayerStats>().CurrentHealth, (int)GameManager.Instance.Player.GetComponent<PlayerStats>().CurrentLvl);
        CSVWriter.Instance.WriteCSVLine(currentWave + 1, waveData.sprayEnemyPrefab.name, waveData.sprayEnemyCount, waveData.spEMaxHealth, waveData.spESpeed, waveData.spEAttackDamage, waveData.spEAttackSpeed, waveData.spEProjectileSpeed, (int)GameManager.Instance.Player.GetComponent<PlayerStats>().Health, (int)GameManager.Instance.Player.GetComponent<PlayerStats>().CurrentHealth, (int)GameManager.Instance.Player.GetComponent<PlayerStats>().CurrentLvl);

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
        waveText.text = "Welle " + currentWave.ToString();
        enemiesToSpawn.Clear();
    } 

    public void PrepareNextWave()
    {
        if (currentWave < waves.Length)
        {
            playerStats.Heal(50 + 10 * currentWave);
            Invoke("SpawnNewWave", betwenWaveTime);
        }
        else
        {
            Debug.Log("All Waves Cleared");
            GameManager.Instance.GameComplete();
        }
    }
}
