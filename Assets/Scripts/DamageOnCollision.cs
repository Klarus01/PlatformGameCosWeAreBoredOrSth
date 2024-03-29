using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>())
        {
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}