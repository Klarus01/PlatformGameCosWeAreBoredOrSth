using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinsAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.PlayCoinParitcle(transform);
            player.GetCoins(coinsAmount);
            Destroy(gameObject);
        }
    }
}