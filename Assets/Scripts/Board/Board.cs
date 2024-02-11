using TMPro;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private TMP_Text interactionText;
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private BoardSO boardSO;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private PlayerController playerController;
    private bool isActivated;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActivated)
        {
            ChangeTutorialText();
            ToggleTutorialPanel();
            ChangeRespawnPoint();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            return;
        }

        playerController = player;
        isActivated = true;
        interactionText.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.GetComponent<PlayerController>())
        {
            return;
        }

        isActivated = false;
        interactionText.gameObject.SetActive(false);
        ToggleTutorialPanel();
    }

    private void ToggleTutorialPanel()
    {
        tutorialPanel.SetActive(isActivated);
    }

    private void ChangeTutorialText()
    {
        tutorialText.SetText(boardSO.boardText);
    }

    private void ChangeRespawnPoint()
    {
        playerController.SetNewRespawnPoint(transform);
    }
}