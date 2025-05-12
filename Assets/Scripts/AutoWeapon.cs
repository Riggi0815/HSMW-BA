using System.Linq;
using UnityEngine;

public class AutoWeapon : MonoBehaviour
{

    [SerializeField] WeaponManager weaponManager;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float reloadSpeed = 1f;
    [SerializeField] float projectileSpeed = 10f;

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
        // Instantiate the projectile at the weapon's position and rotation
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        newProjectile.GetComponent<Rigidbody2D>().linearVelocity = transform.up * projectileSpeed; // Set the projectile's velocity
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
