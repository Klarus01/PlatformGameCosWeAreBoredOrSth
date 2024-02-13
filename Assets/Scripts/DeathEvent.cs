using UnityEngine;
using UnityEngine.UI;

public class DeathEvent : MonoBehaviour
{
    [SerializeField] private GameObject deathScreenCanvas;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private Button respownButton;

    private void Start()
    {
        respownButton.onClick.AddListener(Respawn);
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += ToggleDeathScreen;
        PlayerHealth.OnPlayerDeath += PlayDeathSound;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= ToggleDeathScreen;
        PlayerHealth.OnPlayerDeath -= PlayDeathSound;
    }

    public void ToggleDeathScreen()
    {
        deathScreenCanvas.SetActive(!deathScreenCanvas.activeSelf);

        if (deathScreenCanvas.activeSelf)
        {
            GetComponent<PlayerController>().PlayerDeath();
        }
    }

    public void PlayDeathSound()
    {
        deathSound.Play();
    }

    private void Respawn()
    {
        GetComponent<PlayerController>().RespawnPlayer();
        ToggleDeathScreen();
    }
}