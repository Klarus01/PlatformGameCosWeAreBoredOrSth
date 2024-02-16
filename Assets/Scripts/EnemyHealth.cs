using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int health;
    [SerializeField] private int maxHealth;

    public int Health { get { return health; } set { health = value; } }

    private void Start()
    {
        health = maxHealth;
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}