using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth, currentHealth, currentExp, maxExp, currentLvl;

    [SerializeField] private FloatingHealthBar healthBar;
    [SerializeField] private ExpBar expBar;

    public float Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

    private void Awake() {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void HandleExpChange(int amount)
    { 
        Debug.Log("Exp Changed: " + amount);
        //Handle the experience change
        currentExp += amount;
        OnExpUpdate?.Invoke(currentExp, maxExp);
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

        currentExp = currentExp - maxExp;
        maxExp += 100;

        OnLevelUp?.Invoke(currentLvl);
        OnExpUpdate?.Invoke(currentExp, maxExp);
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }
}
