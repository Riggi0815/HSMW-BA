using System.Linq;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{

    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private bool showDetectionRadius = true;

    [SerializeField] private Collider2D[] enemiesInRange;
    [SerializeField] Collider2D closestEnemy;

    bool isEnemyDetected = false;
    public bool IsEnemyDetected => isEnemyDetected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectEnemies();
        if (isEnemyDetected)
        {
            GetClosestEnemy();
        }
        
    }

    private void DetectEnemies()
    {
        // Use 2D physics for enemy detection in a 2D game
            enemiesInRange = Physics2D.OverlapCircleAll(
            playerTransform.position,
            detectionRadius,
            enemyLayer
        );

        if (enemiesInRange.Length > 0)
        {
            isEnemyDetected = true;
        }
        else
        {
            isEnemyDetected = false;
        }
    }

    public Transform GetClosestEnemy()
    {

        // Find the closest enemy
        closestEnemy = enemiesInRange.OrderBy(e => Vector2.Distance(playerTransform.position, e.transform.position)).FirstOrDefault();
    
        return closestEnemy.transform;
    }

    // Visual debugging
    private void OnDrawGizmos()
    {
        if (showDetectionRadius && playerTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(playerTransform.position, detectionRadius);
        }
    }
}
