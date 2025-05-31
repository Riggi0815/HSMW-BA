using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth, currentHealth, currentExp, maxExp, currentLvl;

    [SerializeField] private FloatingHealthBar healthBar;
    [SerializeField] private ExpBar expBar;
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private AutoWeapon autoWeapon;
    [SerializeField] float damageTakenLastWave;
    public float DamageTakenLastWave { get => damageTakenLastWave; set => damageTakenLastWave = value; }

    public float Health { get => maxHealth; set => maxHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float CurrentLvl { get => currentLvl; set => currentLvl = value; }

    public event Action<float> OnLevelUp;
    public event Action<float, float> OnExpUpdate;
    void OnEnable()
    {
        //Subscribe to the event
        ExpManager.Instance.OnExpChange += HandleExpChange;
    }

    void OnDisable()
    {
        //Unsubscribe from the event
        ExpManager.Instance.OnExpChange -= HandleExpChange;
    }

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        OnExpUpdate?.Invoke(currentExp, maxExp);
        playerBullet.GetComponent<PlayerBullet>().Damage = 45;
        autoWeapon.ReloadSpeed = .8f;
    }

    public void Reset()
    {
        currentHealth = maxHealth;
        playerBullet.GetComponent<PlayerBullet>().Damage = 45;
        autoWeapon.ReloadSpeed = .8f;
        currentExp = 0;
        maxExp = 300;
        currentLvl = 0;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        OnExpUpdate?.Invoke(currentExp, maxExp);
        OnLevelUp?.Invoke(currentLvl);
    }

    private void HandleExpChange(int amount)
    {
        Debug.Log("Exp Changed: " + amount);
        //Handle the experience change
        currentExp += amount;
        OnExpUpdate?.Invoke(currentExp, maxExp);
        if (currentExp >= maxExp && currentLvl != 5)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {

            //Handle the level up
            maxHealth += 30;
            currentHealth = maxHealth;
            healthBar.UpdateHealthBar(currentHealth, maxHealth);

            currentLvl++;
            Debug.Log("Level Up: " + currentLvl);

            currentExp = currentExp - maxExp;
            maxExp = 300 + (50 * currentLvl) + (10 * currentLvl * currentLvl);

            OnLevelUp?.Invoke(currentLvl);
            OnExpUpdate?.Invoke(currentExp, maxExp);
            if (currentLvl == 2)
            {
                playerBullet.GetComponent<PlayerBullet>().Damage = 100;
                autoWeapon.ReloadSpeed = .6f;
            }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        damageTakenLastWave += damageAmount;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    
}
