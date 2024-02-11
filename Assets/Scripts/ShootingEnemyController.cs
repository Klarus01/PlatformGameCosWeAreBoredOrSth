using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyController : MonoBehaviour
{
    [Header("--- Movement ---")] 
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distance = 0.2f;
    [HideInInspector] public bool isFacingRight = true;


    [Header("--- Shooting ---")] 
    [SerializeField] private Transform bulletSpawnerTransform;
    [SerializeField] private Transform raycastTransform;
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private float shootCooldown;
    [SerializeField] private int amountOfBullets;


    [Header("--- Player Detection ---")] [SerializeField]
    private float range;

    // [SerializeField] private float seekingTime;

    //movement
    private Rigidbody2D rb;
    private Collider2D col;
    private Vector2 direction = Vector2.right;

    //behavior
    // private bool seek = false;
    private bool shoot = false;
    private bool move = true;

    //shooting
    float lastShootTime;

    //player detection
    // private float lastTimeSeePlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (IsSeeingPlayer())
        {
            // seek = false;
            shoot = true;
            move = false;
            // lastTimeSeePlayer = Time.time;
        }
        // else if (IsSeeking())
        // {
        //     Debug.Log(lastTimeSeePlayer);
        //     Debug.Log(lastTimeSeePlayer + seekingTime);
        //     Debug.Log(IsSeeking());
        //
        //     seek = true;
        //     shoot = false;
        //     move = false;
        // }
        else
        {
            // seek = false;
            shoot = false;
            move = true;
        }

        if (move)
        {
            rb.MovePosition(rb.position + direction * (speed * Time.deltaTime));

            if (EdgeIsReached())
            {
                ChangeDirection();
            }
        }

        if (shoot && IsShootCooldown())
        {
            StartCoroutine(ShootBullets());
            lastShootTime = Time.time;
        }
        //
        // if (seek)
        // {
        //     Debug.Log("seeking");
        // }
    }

    private bool IsShootCooldown() => lastShootTime + shootCooldown < Time.time;
    // private bool IsSeeking() => lastTimeSeePlayer + seekingTime < Time.time;

    private IEnumerator ShootBullets()
    {
        for (int i = 0; amountOfBullets > i; i++)
        {
            float angle = isFacingRight ? 0f : 180f;
            Instantiate(bulletPrefab, bulletSpawnerTransform.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));
            yield return new WaitForSeconds(0.1f);
        }
    }

    private bool IsSeeingPlayer()
    {
        var raycastPosition = raycastTransform.position;
        Vector2 seeDirection;
        if (direction.x > 0)
        {
            seeDirection = Vector2.right;
        }
        else
        {
            seeDirection = Vector2.left;
        }

        Debug.DrawRay(raycastPosition, seeDirection * range, Color.red);
        var collision = Physics2D.Raycast(raycastPosition, seeDirection, range);


        if (collision.collider.gameObject.GetComponent<PlayerController>())
        {
            return true;
        }

        return false;
    }

    private bool EdgeIsReached()
    {
        float x = direction.x == -1 ? col.bounds.min.x - 0.1f : col.bounds.max.x + 0.1f;

        float y = col.bounds.min.y;

        Vector2 original = new Vector2(x, y);

        // Vector2 newVector = ; 

        Debug.DrawRay(original, Vector2.down * distance, Color.green);
        var collision = Physics2D.Raycast(original, Vector2.down, distance);

        if (collision.collider == null)
        {
            return true;
        }

        return false;
    }

    private void ChangeDirection()
    {
        isFacingRight = !isFacingRight;
        direction.x *= -1;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.GetComponent<PlayerController>())
        {
            ChangeDirection();
        }
    }
}