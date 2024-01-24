using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimJump : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] private KeyCode jump;
    [SerializeField] private LayerMask groundLayerMask;

    
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(jump) && IsGrounded())
        {
            Jump();
        }
    }
}
