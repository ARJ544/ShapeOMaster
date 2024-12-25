using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 10f;
    private bool canShoot = true;

    public void Shoot()
    {
        if (!canShoot) return;

        canShoot = false;

        if (shootPoint != null && bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * bulletSpeed, ForceMode2D.Impulse);
            Destroy(bullet, 2f); // Destroy bullet after 2 seconds
        }
        else
        {
            Debug.LogWarning("ShootPoint or BulletPrefab is not assigned!");
        }

        Invoke(nameof(ResetShoot), 0.2f); // Delay to prevent spamming
    }

    private void ResetShoot()
    {
        canShoot = true;
    }
}
