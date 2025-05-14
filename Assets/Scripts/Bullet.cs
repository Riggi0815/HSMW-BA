using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage;
    

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
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
}
