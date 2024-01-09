using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("--- Movement Stats ---")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float horizontal;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float jumpingPower = 12f;
    [SerializeField] private float wallSlidingSpeed = 2f;
    [SerializeField] private bool isWallJumping;
    [SerializeField] private float wallJumpingDirection;
    [SerializeField] private float wallJumpingDuration = 0.4f;
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(5f, 20f);
    [SerializeField] private bool isFacingRight = true;

    [Header("--- Layers ---")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallLayerMask;

    [Header("--- Player Input ---")]
    [SerializeField] private KeyCode jump;

    private bool IsGrounded() => Physics2D.OverlapCircle(transform.position, 1.1f, groundLayerMask);
    private bool IsWalled() => Physics2D.OverlapCircle(transform.position, 0.6f, wallLayerMask);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(jump) && IsGrounded() && !IsWalled())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetKeyUp(jump) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        WallSlide();
        WallJump();

        if ((isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) && !isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    private void WallJump()
    {

        if (Input.GetKeyDown(jump) && IsWalled())
        {
            isWallJumping = true;
            wallJumpingDirection = -transform.localScale.x;
            Debug.Log(wallJumpingDirection * wallJumpingPower.x);
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}