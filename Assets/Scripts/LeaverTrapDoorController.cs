using System;
using UnityEngine;

public class LeaverTrapDoorController : MonoBehaviour
{
    [SerializeField] private TrapDoorController[] trapDoors;
    [SerializeField] private GameObject interactionKeyUi;

    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>())
        {
            interactionKeyUi.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>())
        {
            interactionKeyUi.SetActive(false);
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange)
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