using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public static Spawner Instance;
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

    [SerializeField] List<GameObject> objectsToSpawn;

    [SerializeField] float spawnRadius;
    [SerializeField] float spawnTimer;
    
    private float currentSpwanTime;



    void OnEnable()
    {
        WaveManager.Instance.OnNewWave += HandleNewWave;
    }

    void OnDisable()
    {
        WaveManager.Instance.OnNewWave -= HandleNewWave;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpwanTime > 0)
        {
            currentSpwanTime -= Time.deltaTime;
        }
        else
        {
            if (objectsToSpawn.Count > 0)
            {
                SpawnObjectsAtRondom();
                currentSpwanTime = spawnTimer;
            }
            
        }
        
    }

    void HandleNewWave(List<GameObject> enemies, float spawnTime)
    {
        objectsToSpawn = new List<GameObject>(enemies);
        spawnTimer = spawnTime;
        currentSpwanTime = spawnTimer;
    }

    void SpawnObjectsAtRondom()
    {
        Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
        
        int index = Random.Range(0, objectsToSpawn.Count);
        GameObject obj = Instantiate(objectsToSpawn[index], randomPosition, Quaternion.identity, this.transform);
        objectsToSpawn.RemoveAt(index);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, spawnRadius);
    }
}
