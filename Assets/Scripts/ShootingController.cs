using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.2f;

    [SerializeField] private KeyCode leftShoot;
    [SerializeField] private KeyCode rightShoot;

    private float timeUntilFire;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && timeUntilFire < Time.time)
        {
            Shoot();
            timeUntilFire = Time.time + fireRate;
        }
    }

    public void Shoot()
    {
        if ((Input.GetKeyDown(leftShoot) && playerController.isFacingRight || Input.GetKeyDown(rightShoot) && !playerController.isFacingRight) && playerController.horizontal.Equals(0))
        {
            playerController.Flip();
        }

        float angle = playerController.isFacingRight ? 0f : 180f;
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));
    }
}