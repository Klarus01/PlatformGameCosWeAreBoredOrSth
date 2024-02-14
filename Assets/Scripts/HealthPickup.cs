using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private float healAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth health))
        {
            if (health.FullHealth())
            {
                return;
            }

            health.HealHealth(healAmount);
            Destroy(gameObject);
        }
    }
}