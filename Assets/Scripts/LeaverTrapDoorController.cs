using UnityEngine;

public class LeaverTrapDoorController : MonoBehaviour
{
    [SerializeField] private TrapDoorController[] trapDoors;
    [SerializeField] private GameObject interactionKeyUi;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isOpen = false;

    private bool playerInRange = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isOpen)
                {
                    isOpen = false;
                }
                else
                {
                    isOpen = true;
                }

                animator.SetBool("isOpen", isOpen);

                foreach (TrapDoorController trapDoor in trapDoors)
                {
                    trapDoor.SwitchState(isOpen);
                }
            }
        }
    }

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
}