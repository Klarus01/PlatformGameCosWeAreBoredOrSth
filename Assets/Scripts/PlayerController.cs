using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("--- Components ---")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D col;
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private Transform respawnPoint;

    [Header("--- Layers ---")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallLayerMask;

    [Header("--- Player Input ---")]
    [SerializeField] private KeyCode jump;

    //Moving
    public float horizontal;
    private float speed = 20f;
    private Vector2 damagedPower = new(10f, 5f);
    [HideInInspector] public bool isFacingRight = true;

    //Jumping
    private float jumpingPower = 15f;
    private float wallJumpingDirection;
    private float wallJumpingDuration = .1f;
    private float wallJumpingTimer;
    private bool isWallJumping = false;
    private Vector2 wallJumpingPower = new(15f, 10f);

    //Sliding
    private float wallSlidingSpeed = 2f;
    private float sideOfWall;
    private bool isWallSliding = false;

    private bool IsGrounded() => Physics2D.CapsuleCast(col.bounds.center, col.size, col.direction, 0f, Vector2.down, 0.1f, groundLayerMask);
    private bool IsCeiling() => Physics2D.CapsuleCast(col.bounds.center, col.size, col.direction, 0f, Vector2.up, 0.1f, groundLayerMask);
    private bool IsWalled() => Physics2D.CapsuleCast(col.bounds.center, col.size, col.direction, 0f, Vector2.right * transform.localScale.x, 0.1f, wallLayerMask);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        wallJumpingTimer -= Time.deltaTime;
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(jump) && (IsGrounded()))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetKeyUp(jump) && rb.velocity.y > 0f && !isWallJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        WallJump();
        WallSlide();
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
            Debug.Log("XD");
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            if (IsGrounded() || IsCeiling() || (IsWalled() || horizontal != 0) && wallJumpingTimer < 0)
            {
                Debug.Log("XD1");
                isWallJumping = false;
                rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y * 0.5f);
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

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void SetNewRespawnPoint(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }

    public void RespawnPlayer()
    {
        transform.position = respawnPoint.position;
        playerHealth.Respawn();
    }
}