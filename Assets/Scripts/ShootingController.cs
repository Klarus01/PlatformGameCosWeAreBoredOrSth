using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.2f;

    private float timeUIntilFire;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && timeUIntilFire < Time.time)
        {
            Shoot();
            timeUIntilFire = Time.time + fireRate;
        }
    }

    public void Shoot()
    {
        float angle = playerController.isFacingRight ? 0f : 180f;
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));
    }
}