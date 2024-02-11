using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveingEnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distance = 0.2f;


    private Rigidbody2D rb;
    private Collider2D col;
    private Vector2 direction = Vector2.right;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

        if (EdgeIsReached())
        {
            ChangeDirection();
        }
    }

    private bool EdgeIsReached()
    {
        float x = direction.x == -1 ? col.bounds.min.x - 0.1f : col.bounds.max.x + 0.1f;

        float y = col.bounds.min.y;

        Vector2 original = new Vector2(x, y);

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