using UnityEngine;

public class Spike : MonoBehaviour
{
    private float damage = 6f;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>())
        {
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
