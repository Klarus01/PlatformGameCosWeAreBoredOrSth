using UnityEngine;

public class LeaverTrapDoorController : MonoBehaviour
{
    [SerializeField] private TrapDoorController[] trapDoors;

    private void OnTriggerStay2D(Collider2D other)
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