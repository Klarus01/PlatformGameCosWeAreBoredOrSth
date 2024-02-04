using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEvent : MonoBehaviour
{
    [SerializeField] private GameObject deathScreenCanvas;
    [SerializeField] private AudioSource deathSound;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += PlayDeathSound;
        PlayerHealth.OnPlayerDeath += EnableDeathScreen;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= PlayDeathSound;
        PlayerHealth.OnPlayerDeath -= EnableDeathScreen;
    }
    
    
    public void PlayDeathSound()
    {
        deathSound.Play();
    }

    public void EnableDeathScreen()
    {
        deathScreenCanvas.SetActive(true);
    }
}
