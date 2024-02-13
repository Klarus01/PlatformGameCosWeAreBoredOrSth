using UnityEngine;

public class MimJump : MonoBehaviour
{
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private KeyCode jump;
    [SerializeField] private LayerMask groundLayerMask;

    private bool playerInRange = false;

    private Rigidbody2D rb;

    private bool IsGrounded() => Physics2D.OverlapCircle(transform.position, 1f, groundLayerMask);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>())
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>())
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(jump) && IsGrounded())
            {
                Jump();
            }
        }
    }
}