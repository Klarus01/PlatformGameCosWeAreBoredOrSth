using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimJump : MonoBehaviour
{
    [SerializeField] private float jumpPower;
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
        playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerInRange = false;
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
