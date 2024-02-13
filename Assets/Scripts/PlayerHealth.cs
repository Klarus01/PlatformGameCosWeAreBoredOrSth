using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHeal;
    private bool isDead = false;

    public float health, maxHealth;

    public bool FullHealth() => health >= maxHealth;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isDead)
        {
            return;
        }

        health -= amount;
        OnPlayerDamaged?.Invoke();

        if (health <= 0)
        {
            health = 0;
            isDead = true;
            OnPlayerDeath?.Invoke();
        }
    }

    public void HealHealth(float amount)
    {
        health += amount;
        OnPlayerHeal?.Invoke();

        if (FullHealth())
        {
            health = maxHealth;
        }
    }

    public void Respawn()
    {
        isDead = false;
        HealHealth(maxHealth);
    }
}