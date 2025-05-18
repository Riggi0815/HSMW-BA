using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] Transform target;

    private PlayerStats playerStats;

    public Transform Target
    {
        set { target = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }


    void Awake()
    {
        playerStats = GameManager.Instance.Player.GetComponent<PlayerStats>();
        Physics2D.IgnoreLayerCollision(6, 8);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit != null)
        {
            // Deal damage to the enemy
            hit.Damage(damage);

        }

        // Destroy the arrow when it collides with an enemy
        Destroy(gameObject);
    }

    private void Update()
    {
        if (target != null && playerStats.CurrentLvl >= 5)
        {
            Vector2 direction = target.position - transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector2.up, direction), 70f * Time.deltaTime);
        }
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = transform.up * speed;
        
        
    }
}
