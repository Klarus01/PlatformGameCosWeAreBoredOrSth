using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyController : MonoBehaviour
{
    private Collider2D colWithPlayer;
    private void OnDisable()
    {
        if (colWithPlayer.gameObject.CompareTag("Player"))
        {
            colWithPlayer.gameObject.transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        colWithPlayer = col;
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.transform.SetParent(null);
        }
    }
}
