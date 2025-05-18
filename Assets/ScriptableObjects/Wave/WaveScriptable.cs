using UnityEngine;

[CreateAssetMenu(fileName = "WaveScriptable", menuName = "Scriptable Objects/WaveScriptable")]
public class WaveScriptable : ScriptableObject
{

    [Header("Wave Settings")]

    public int waveNumber;
    public float spawnTime;// Time between every enemy

    [Header("Enemies")]
    [Header("Explosion Enemy")]
    public GameObject explosionEnemyPrefab;
    public int explosionEnemyCount;
    public float eEMaxHealth;
    public float eESpeed;
    public float eEAttackDamage;

    [Header("Shooting Enemy")]
    public GameObject shootingEnemyPrefab;
    public int shootingEnemyCount;
    public float shEMaxHealth;
    public float shESpeed;
    public float shEAttackDamage;
    public float shEProjectileSpeed;

    [Header("SprayEnemy")]
    public GameObject sprayEnemyPrefab;
    public int sprayEnemyCount;
    public float spEMaxHealth;
    public float spESpeed;
    public float spEAttackDamage;
    public float spEAttackSpeed;
    public float spEProjectileSpeed;

    

    
}
