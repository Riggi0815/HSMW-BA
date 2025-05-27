using UnityEngine;

public class ShootingEnemy : Enemy
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] GameObject bulletHolder;
    [SerializeField] float projectileSpeed = 10f;

    public override void Setup(float newHealth, float newSpeed, float newAttackDamage, float newAttackSpeed, float newProjectileSpeed, float baseExp, int multiplier)
    {
        base.Setup(newHealth, newSpeed, newAttackDamage, newAttackSpeed, newProjectileSpeed, baseExp, multiplier);
        this.projectileSpeed = newProjectileSpeed;
    }
    
    private void Awake() {
        bulletHolder = GameObject.Find("BulletHolder");
    }

    public override void Attack()
    {
        // Calculate the direction to the target
        Vector2 direction = player.position - transform.position;

        // Rotate the weapon towards the target
        transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);

        // Fire the projectile
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation, bulletHolder.transform);

        Bullet bullet = newBullet.GetComponent<Bullet>();
        bullet.Damage = attackDamage;

        newBullet.GetComponent<Rigidbody2D>().linearVelocity = transform.up * projectileSpeed; // Set the projectile's velocity
    }   


}
