using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("--- Components ---")]
    [SerializeField] private Rigidbody2D rb;

    [Header("--- Layers ---")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private LayerMask topWallLayerMask;

    [Header("--- Player Input ---")]
    [SerializeField] private KeyCode jump;
    [SerializeField] private KeyCode leftShoot;
    [SerializeField] private KeyCode rightShoot;

    //Moving
    [SerializeField] private float horizontal;
    private float speed = 20f;
    public bool isFacingRight = true;

    //Jumping
    private float jumpingPower = 15f;
    private float wallJumpingDirection;
    private float wallJumpingDuration = .1f;
    private float wallJumpingTimer;
    private bool isWallJumping = false;
    private Vector2 wallJumpingPower = new Vector2(12.5f, 7.5f);

    //Sliding
    private float wallSlidingSpeed = 2f;
    private float sideOfWall;
    private bool isWallSliding = false;
    private float wallSlidingDuration = .1f;
    private float wallSlidingTimer;

    private bool IsGrounded() => Physics2D.OverlapCircle(transform.position, 1f, groundLayerMask);
    private bool IsWalled() => Physics2D.OverlapCircle(transform.position, 0.55f, wallLayerMask);
    private bool IsTopWalled() => Physics2D.OverlapCircle(transform.position, 1f, topWallLayerMask);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        wallJumpingTimer -= Time.deltaTime;
        wallSlidingTimer -= Time.deltaTime;
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(jump) && ((IsGrounded() && !IsWalled()) || IsTopWalled()))
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

        if ((Input.GetKeyDown(leftShoot) && isFacingRight || Input.GetKeyDown(rightShoot) && !isFacingRight) && horizontal.Equals(0))
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
            isWallSliding = true;
            sideOfWall = horizontal;
        }
        else if (!IsWalled() || IsGrounded())
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            wallSlidingTimer = wallSlidingDuration;
            rb.velocity = new Vector2(0, -wallSlidingSpeed);

            if (horizontal.Equals(0))
            {
                isWallSliding = true;
            }
            else if (!sideOfWall.Equals(horizontal))
            {
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
            if (IsGrounded() || (IsWalled() || horizontal != 0) && wallJumpingTimer < 0)
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
            rb.gravityScale = 5f;
        }
        else
        {
            rb.gravityScale = 2f;
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