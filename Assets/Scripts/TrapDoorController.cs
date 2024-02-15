using UnityEngine;

public class TrapDoorController : MonoBehaviour
{
    private BoxCollider2D thisBoxCollider2D;
    private Animator animator;

    private void Start()
    {
        thisBoxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    public void SwitchState(bool isOpen)
    {
        if (isOpen)
        {
            thisBoxCollider2D.isTrigger = true;
        }
        else
        {
            thisBoxCollider2D.isTrigger = false;
        }

        animator.SetBool("isOpen", isOpen);
    }
}