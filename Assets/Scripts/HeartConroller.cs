using System.Collections.Generic;
using UnityEngine;

public class HeartConroller : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private PlayerHealth playerHealth;

    private List<HealthHeart> hearts = new List<HealthHeart>();

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDamaged += DrawHearts;
        PlayerHealth.OnPlayerHeal += DrawHearts;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamaged -= DrawHearts;
        PlayerHealth.OnPlayerHeal -= DrawHearts;
    }

    public void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();

        //check how many hearts to draw

        float maxHealthReminder = playerHealth.maxHealth % 2;
        int heartsToDraw = (int)((playerHealth.maxHealth / 2) + maxHealthReminder);

        for (int i = 0; i < heartsToDraw; i++)
        {
            CreatEmptyHeart();
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusReminder = (int)Mathf.Clamp(playerHealth.health - (i * 2), 0, 2);
            hearts[i].SetHeatImage((HeartStatus)heartStatusReminder);
        }
    }

    public void ClearHearts()
    {
        foreach (Transform transform in transform)
        {
            Destroy(transform.gameObject);
        }

        hearts = new List<HealthHeart>();
    }

    public void CreatEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab, transform, true);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeatImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }
}