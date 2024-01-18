using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gunpoint;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bulletPrefab, gunpoint.transform);
        }
    }
}