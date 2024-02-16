using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private List<CoinPickup> coins = new();
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TMP_Text finalCoinsText;
    private int allCoins = 0;

    private void Start()
    {
        foreach (CoinPickup coin in coins)
        {
            allCoins += coin.coinsAmount;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            endGamePanel.SetActive(true);
            SetFinalCoinsText(player);
        }
    }

    private void SetFinalCoinsText(PlayerController player)
    {
        finalCoinsText.SetText("Coins: " + player.coins + "/" + allCoins);
    }
}