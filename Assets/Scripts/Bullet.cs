using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float bulletSpeed = 10f;
    private float timeToLife = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeToLife);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}