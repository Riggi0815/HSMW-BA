using UnityEngine;

public class SprayEnemy : Enemy
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] firePoints;
    [SerializeField] GameObject bulletHolder;
    [SerializeField] float projectileSpeed = 10f;

    private float shootTimer;


    public override void Update()
    {
        base.Update();
        if (shootTimer <= 0)
        {
            Attack();
            shootTimer = attackSpeed;
        }else
        {
            shootTimer -= Time.deltaTime;
        }
        
    }

    private void Awake() {
        bulletHolder = GameObject.Find("BulletHolder");
    }

    public override void Setup(float newHealth, float newSpeed, float newAttackDamage, float newAttackSpeed, float newProjectileSpeed, float baseExp, int multiplier)
    {
        base.Setup(newHealth, newSpeed, newAttackDamage, newAttackSpeed, newProjectileSpeed, baseExp, multiplier);
        this.projectileSpeed = newProjectileSpeed;
        this.attackSpeed = newAttackSpeed;
    }

    public override void Attack()
    {

        for (int i = 0; i < firePoints.Length; i++)
        {

            // Instantiate bullet at firepoint position and rotation
            GameObject newBullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation, bulletHolder.transform);

            // Pass damage to the bullet
            Bullet bullet = newBullet.GetComponent<Bullet>();
            bullet.Damage = attackDamage;

            // Get the forward direction of the firepoint
            Vector2 direction = firePoints[i].up;

            // Apply velocity in the direction the arm is pointing
            newBullet.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;
        }

    }
}
