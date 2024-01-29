using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private TMP_Text interactionText;
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private GameObject tutorialPanel;
    private bool isActivated;

    [SerializeField] private List<string> TutorialTexts = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActivated)
        {
            ChangeTutorialText();
            ToggleTutorialPanel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isActivated = true;
        interactionText.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
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
        tutorialText.SetText(TutorialTexts[0]);
    }
}