using System.Linq;
using UnityEngine;

public class AutoWeapon : MonoBehaviour
{

    [SerializeField] WeaponManager weaponManager;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float reloadSpeed = 1f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] PlayerStats playerStats;

    float reloadTimer = 0f;


    private void Update() {
        reloadTimer += Time.deltaTime;

        if (reloadTimer >= reloadSpeed && weaponManager.LockedOnEnemy) {
            
            reloadTimer = 0f; // Reset the timer 
            // Fire the projectile
            FireProjectile();
        }
    }

    void FireProjectile() {
        if (playerStats.CurrentLvl >= 2) {
            GameObject newProjectileStraight = Instantiate(projectilePrefab, transform.position, transform.rotation);
            GameObject newProjectileLeft = Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 30));
            GameObject newProjectileRight = Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, -30));
            newProjectileStraight.GetComponent<Rigidbody2D>().linearVelocity = newProjectileStraight.transform.up * projectileSpeed;
            newProjectileLeft.GetComponent<Rigidbody2D>().linearVelocity = newProjectileLeft.transform.up * projectileSpeed;
            newProjectileRight.GetComponent<Rigidbody2D>().linearVelocity = newProjectileRight.transform.up * projectileSpeed;
        }
        {
            GameObject newProjectileStraight = Instantiate(projectilePrefab, transform.position, transform.rotation);
            newProjectileStraight.GetComponent<Rigidbody2D>().linearVelocity = transform.up * projectileSpeed;
        }
            
        
        
    }

    // Point the weapon towards the target
    public void Aim(Transform target)
    {
        // Calculate the direction to the target
        Vector2 direction = target.position - transform.position;

        // Rotate the weapon towards the target
        transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    

}
