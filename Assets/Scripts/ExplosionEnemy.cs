using UnityEngine;

public class ExplosionEnemy : Enemy
{

    public override void Update() {
        base.Update();
        Attack();
    }
    public override void Attack()
    {
        if (Vector2.Distance(transform.position, player.position) < attackRange)
            {
                player.GetComponent<IDamageable>().Damage(attackDamage);
                Die();
            }

        
    }

    public override void Die()
    {
        Destroy(gameObject);
        CheckEnemyCount();
    }
}
