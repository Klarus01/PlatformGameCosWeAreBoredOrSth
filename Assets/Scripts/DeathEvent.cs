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


    public void PlayDeathSound()
    {
        deathSound.Play();
    }

    public void ToggleDeathScreen()
    {
        deathScreenCanvas.SetActive(!deathScreenCanvas.activeSelf);
    }

    private void Respawn()
    {
        GetComponent<PlayerController>().RespawnPlayer();
        ToggleDeathScreen();
    }
}