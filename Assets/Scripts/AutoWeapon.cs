using System.Linq;
using UnityEngine;

public class AutoWeapon : MonoBehaviour
{

    [SerializeField] WeaponManager weaponManager;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float reloadSpeed = 1f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject bulletHolder;

    [SerializeField] Transform currentTarget;

    float reloadTimer = 0f;
    
    public float ReloadSpeed
    {
        get { return reloadSpeed; }
        set { reloadSpeed = value; }
    }


    private void Update()
    {
        reloadTimer += Time.deltaTime;

        if (reloadTimer >= reloadSpeed && weaponManager.LockedOnEnemy)
        {

            reloadTimer = 0f; // Reset the timer 
            // Fire the projectile
            FireProjectile();
        }
    }

    void FireProjectile() {
        if (playerStats.CurrentLvl >= 4) {
            GameObject newProjectileStraight = Instantiate(projectilePrefab, transform.position, transform.rotation, bulletHolder.transform);
            GameObject newProjectileLeft = Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 30), bulletHolder.transform);
            GameObject newProjectileRight = Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, -30), bulletHolder.transform);
            newProjectileStraight.GetComponent<PlayerBullet>().Target = currentTarget;
            newProjectileLeft.GetComponent<PlayerBullet>().Target = currentTarget;
            newProjectileRight.GetComponent<PlayerBullet>().Target = currentTarget;
            newProjectileStraight.GetComponent<Rigidbody2D>().linearVelocity = newProjectileStraight.transform.up * projectileSpeed;
            newProjectileLeft.GetComponent<Rigidbody2D>().linearVelocity = newProjectileLeft.transform.up * projectileSpeed;
            newProjectileRight.GetComponent<Rigidbody2D>().linearVelocity = newProjectileRight.transform.up * projectileSpeed;
        }
        else{
            GameObject newProjectileStraight = Instantiate(projectilePrefab, transform.position, transform.rotation, bulletHolder.transform);
            newProjectileStraight.GetComponent<Rigidbody2D>().linearVelocity = transform.up * projectileSpeed;
        }
            
        
        
    }

    // Point the weapon towards the target
    public void Aim(Transform target)
    { 
        currentTarget = target;

        // Calculate the direction to the target
        Vector2 direction = target.position - transform.position;

        // Rotate the weapon towards the target
        transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    

}
