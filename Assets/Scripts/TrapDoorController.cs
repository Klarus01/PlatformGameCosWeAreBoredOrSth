using UnityEngine;

public class TrapDoorController : MonoBehaviour
{
    private SpriteRenderer thisSpriteRenderer;
    private BoxCollider2D thisBoxCollider2D;
    [SerializeField] private bool open;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closedSprite;

    private void Start()
    {
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        thisBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void SwitchState()
    {
        if (open == false)
        {
            thisSpriteRenderer.sprite = openSprite;
            open = true;
            thisBoxCollider2D.isTrigger = true;
        }
        else
        {
            thisSpriteRenderer.sprite = closedSprite;
            open = false;
            thisBoxCollider2D.isTrigger = false;
        }
    }
}