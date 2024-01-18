using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 10f;
    private float timeToLife = 2f;

    private Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeToLife);
        float horizontalInput = Input.GetAxis("Fire1");
        direction = (horizontalInput > 0) ? Vector3.right : Vector3.left;
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}