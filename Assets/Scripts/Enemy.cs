using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{   
    
    public float maxHealth;
    [SerializeField] float currentHealth;
    public int expAmount;
    private float minHealth = 0f;
    [SerializeField] private FloatingHealthBar healthBar;

    public float speed;
    [SerializeField] float patrolRange;
    public float attackRange;
    public float attackSpeed;
    public float attackDamage;

    public Transform player;
    private float worldBorder = 27f;

    [SerializeField] private bool walkPointSet = false;
    private Vector2 walkPoint;

    enum enemyType
    {
        None,
        Melee,
        Ranged
    }

    [SerializeField] enemyType currentEnemyType = enemyType.None;

    public float Health { get {return currentHealth;} set {currentHealth = value;} }

    protected void Start() {
        Init();
    }

    protected virtual void Init() {
        player = GameManager.Instance.Player.transform;

        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public virtual void Setup(float newHealth, float newSpeed, float newAttackDamage, float newAttackSpeed, float newProjectileSpeed, float baseExp, int multiplier)
    {
        expAmount = (int)(baseExp + (WaveManager.Instance.CurrentWave * multiplier));
        this.maxHealth = newHealth;
        this.speed = newSpeed;
        this.attackDamage = newAttackDamage;
    }

    public virtual void Update()
    {
        Move();
    }
    
    protected virtual void Move() {
        switch(currentEnemyType)
        {
            case enemyType.Melee:
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
                break;
            case enemyType.Ranged:
                    if (!walkPointSet)
                    {
                        SearchWalkPoint();
                    }
                    else
                    {
                        if (walkPoint.x < -worldBorder || walkPoint.x > worldBorder || walkPoint.y < -worldBorder || walkPoint.y > worldBorder)
                        {
                            walkPointSet = false;
                            SearchWalkPoint();
                        }
                        Vector2 direction = walkPoint - (Vector2)transform.position;
                        direction.Normalize();
                        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 500 * Time.deltaTime);
                        transform.position = Vector2.MoveTowards(this.transform.position, walkPoint, speed * Time.deltaTime);
                    }
                    Vector2 distanceToWalkPoint = (Vector2)transform.position - walkPoint;

                if (distanceToWalkPoint.magnitude < 0.2f)
                {
                    StartCoroutine(Wait());
                    Attack();
                    walkPointSet = false;
                    }
                break;
            default:
                break;
        }
    }

    IEnumerator Wait()
    {
        speed = 0f;
        yield return new WaitForSeconds(1f);
        speed = 3f;
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-patrolRange, patrolRange);
        float randomY = Random.Range(-patrolRange, patrolRange);

        if (randomX > worldBorder)
        {
            randomX = worldBorder;
        }
        else if (randomX < -worldBorder)
        {
            randomX = -worldBorder;
        }
        if (randomY > worldBorder)
        {
            randomY = worldBorder;
        }
        else if (randomY < -worldBorder)
        {
            randomY = -worldBorder;
        }

        walkPoint = new Vector2(transform.position.x + randomX, transform.position.y + randomY);

        walkPointSet = true;
    }

    public virtual void Attack() {
        
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth <= minHealth)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        ExpManager.Instance.AddExp(expAmount);
        CheckEnemyCount();
    }

    public void CheckEnemyCount()
    {
        if (Spawner.Instance.transform.childCount - 1  == 0 && Spawner.Instance.ObjectsToSpawn.Count == 0)
            {
                CSVWriter.Instance.WriteDamageLine(player.GetComponent<PlayerStats>().DamageTakenLastWave);
                player.GetComponent<PlayerStats>().DamageTakenLastWave = 0;
                WaveManager.Instance.PrepareNextWave();
            }
    }

    
}
