using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LeaverTrapDoorController : MonoBehaviour
{
    [SerializeField] private TrapDoorController[] trapDoors;
    [SerializeField] private GameObject interactionKeyUi;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            interactionKeyUi.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            interactionKeyUi.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach (TrapDoorController trapDoor in trapDoors)
                {
                    trapDoor.SwitchState();
                }
            }
        }
    }
}