using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDirectionMovingController : MonoBehaviour
{
    [SerializeField] private GameObject StartWaypoint;
    [SerializeField] private GameObject EndWaypoint;

    [SerializeField] private float speed = 5f;

    private StickyController stickyController;
    private void Awake()
    {
        stickyController = gameObject.GetComponent<StickyController>();
    }

    private void Update()
    {
        
        if (Vector2.Distance(EndWaypoint.transform.position, transform.position) < .1f)
        {
            stickyController.enabled = false;
            gameObject.transform.position = StartWaypoint.transform.position;
            stickyController.enabled = true;
        }

        transform.position =
            Vector2.MoveTowards(transform.position, EndWaypoint.transform.position, Time.deltaTime * speed);
    }
}