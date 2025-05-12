using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] RotateAroundPlayer rotateAroundPlayer;
    [SerializeField] AutoWeapon autoWeapon;
    [SerializeField] EnemyDetector enemyDetector;
    [SerializeField] Transform closestEnemy;

    private bool lockedOnEnemy = false;
    public bool LockedOnEnemy => lockedOnEnemy;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyDetector.IsEnemyDetected)
        {
            closestEnemy = enemyDetector.GetClosestEnemy();
            lockedOnEnemy = true;
            rotateAroundPlayer.Rotate(closestEnemy);
            autoWeapon.Aim(closestEnemy);
        }else
        {
            lockedOnEnemy = false;
        }
        
    }
}
