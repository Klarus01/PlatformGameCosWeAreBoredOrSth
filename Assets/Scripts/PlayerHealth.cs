using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHeal;

    public float health, maxHealth;

    public bool FullHealth() => health >= maxHealth;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        OnPlayerDamaged?.Invoke();

        if (health <= 0)
        {
            health = 0;
            Debug.Log("umrzyłeś");
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
        HealHealth(maxHealth);
    }
}