using UnityEngine;

public class TrapDoorController : MonoBehaviour
{
    private SpriteRenderer thisSpriteRenderer;
    private BoxCollider2D thisBoxCollider2D;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closedSprite;

    private void Start()
    {
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        thisBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void SwitchState(bool isOpen)
    {
        if (isOpen)
        {
            thisSpriteRenderer.sprite = openSprite;
            thisBoxCollider2D.isTrigger = true;
        }
        else
        {
            thisSpriteRenderer.sprite = closedSprite;
            thisBoxCollider2D.isTrigger = false;
        }
    }
}