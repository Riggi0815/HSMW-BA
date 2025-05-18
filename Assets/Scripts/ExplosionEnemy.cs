using UnityEngine;

public class ExplosionEnemy : Enemy
{

    [SerializeField] ParticleSystem explosionParticle;
    private bool isExploding = false;
    public override void Update()
    {
        base.Update();
        Attack();
    }
    public override void Attack()
    {
        if (Vector2.Distance(transform.position, player.position) < attackRange && !isExploding)
        {
            player.GetComponent<IDamageable>().Damage(attackDamage);
            Explode();
                isExploding = true;
            }

        
    }

    public void Explode()
    {
        speed = 0;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponentInChildren<Canvas>().enabled = false;
        explosionParticle.Play();
        Destroy(gameObject, explosionParticle.main.duration);
        CheckEnemyCount();
    }
}
