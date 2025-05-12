using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private int expAmount;
    private float minHealth = 0f;

    [SerializeField] private FloatingHealthBar healthBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }


    void Die()
    {
        //Death of Object
        Destroy(gameObject);
        ExpManager.Instance.AddExp(expAmount);
        CheckEnemyCount();
    }

    public void Damage(float damageAmount)
    {
        //Damage the object
        currentHealth -= damageAmount;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth <= minHealth)
        {
            Die();
        }
    }

    void CheckEnemyCount()
    {
        if (Spawner.Instance.transform.childCount - 1  == 0)
            {
                WaveManager.Instance.PrepareNextWave();
            }
    }
}
