using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("--- Components ---")]
    [SerializeField] private Rigidbody2D rb;

    [Header("--- Layers ---")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallLayerMask;

    [Header("--- Player Input ---")]
    [SerializeField] private KeyCode jump;

    //Moving
    [SerializeField] private float horizontal;
    private float speed = 20f;
    private bool isFacingRight = true;

    //Jumping
    private float jumpingPower = 12f;
    private float wallJumpingDirection;
    private float wallJumpingDuration = .1f;
    private float wallJumpingTimer;
    private bool isWallJumping = false;
    private Vector2 wallJumpingPower = new Vector2(20f, 10f);

    //Sliding
    private float wallSlidingSpeed = 2f;
    private float sideOfWall;
    private bool isWallSliding = false;
    private float wallSlidingDuration = .1f;
    private float wallSlidingTimer;

    private bool IsGrounded() => Physics2D.OverlapCircle(transform.position, 1f, groundLayerMask);
    private bool IsWalled() => Physics2D.OverlapCircle(transform.position, 0.5f, wallLayerMask);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        wallJumpingTimer -= Time.deltaTime;
        wallSlidingTimer -= Time.deltaTime;
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
        GravityScaleChange();

        if ((isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) && !isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!isWallJumping && !isWallSliding)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private void WallSlide()
    {
        if (wallSlidingTimer > 0)
        {
            return;
        }

        if (IsWalled() && !IsGrounded() && !isWallSliding)
        {
            Debug.Log("XDD");
            isWallSliding = true;
            sideOfWall = horizontal;
        }
        else if (!IsWalled() || IsGrounded())
        {
            Debug.Log("XDD3");
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            wallSlidingTimer = wallSlidingDuration;
            rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);

            if (horizontal.Equals(0))
            {
                Debug.Log("XD");
                isWallSliding = true;
            }
            else if (!sideOfWall.Equals(horizontal))
            {
                Debug.Log("XD2");
                isWallSliding = false;
            }
        }
    }

    private void WallJump()
    {
        if (Input.GetKeyDown(jump) && IsWalled())
        {
            isWallJumping = true;
            wallJumpingTimer = wallJumpingDuration;
            wallJumpingDirection = -transform.localScale.x;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);

            if (transform.localScale.x != wallJumpingDirection)
            {
                Flip();
            }
        }

        if (isWallJumping)
        {
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            if (IsGrounded() || (IsWalled() && wallJumpingTimer < 0) || (horizontal != 0 && wallJumpingTimer < 0))
            {
                isWallJumping = false;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }
        }
    }

    private void GravityScaleChange()
    {
        if (rb.velocity.y < -2f && !isWallSliding && !isWallJumping)
        {
            rb.gravityScale = 3f;
        }
        else
        {
            rb.gravityScale = 1.2f;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}