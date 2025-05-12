using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth, currentHealth, currentExp, maxExp, currentLvl;

    [SerializeField] private FloatingHealthBar healthBar;

    public float Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

    private void Awake() {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void HandleExpChange(int amount)
    {
        //Handle the experience change
        currentExp += amount;
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        //Handle the level up
        maxHealth += 10;
        currentHealth = maxHealth;

        currentLvl++;

        currentExp = 0;
        maxExp += 100;
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }
}
