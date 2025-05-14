using UnityEngine;

[System.Serializable]
public class EnemyNumber
{
    public GameObject enemyPrefab;
    public int enemyCount;
}

[CreateAssetMenu(fileName = "WaveScriptable", menuName = "Scriptable Objects/WaveScriptable")]
public class WaveScriptable : ScriptableObject
{
    
    public int waveNumber;
    public EnemyNumber[] enemies = new EnemyNumber[3];
    public float spawnTime;

    
}
