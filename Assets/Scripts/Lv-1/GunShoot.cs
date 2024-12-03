using UnityEngine;
using UnityEngine.UI;

public class GunShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject triangle;
    public GameObject bulletPrefab;      // Bullet prefab to instantiate
    public Button shootButton;           // Button to trigger shooting
    public float bulletSpeed = 10f;      // Speed of the bullet
    public Transform shootPoint;
    private bool canShoot = true;

    private void Start()
    {
        // Ensure the shootButton is assigned
        if (shootButton != null)
        {
            shootButton.onClick.RemoveAllListeners(); // Clear any previous listeners
            shootButton.onClick.AddListener(Shoot);  // Add the Shoot method as a listener
        }
        else
        {
            Debug.LogError("Shoot button is not assigned.");
        }

        // Ensure the shootPoint is assigned
        if (shootPoint == null)
        {
            Debug.LogError("Shoot Point is not assigned. Assign the Gun's transform.");
        }
    }

    public void Shoot()
    {
        if (!canShoot) return;

        canShoot = false; // Block further shooting
        Debug.Log("Shoot method called");

        if (shootPoint != null && bulletPrefab != null)
        {
            // Create bullet instance
            GameObject BulletIns = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

            // Check the scale of the triangle to determine shooting direction
            Vector2 forceDirection;

            // If triangle's scale on x-axis is greater than 1, it means it's facing right
            if (triangle.transform.localScale.x > 1)
            {
                forceDirection = shootPoint.right;  // Shoot in the right direction
            }
            else  // Otherwise, it's facing left
            {
                forceDirection = -shootPoint.right; // Shoot in the left direction
            }

            // Apply the force to the bullet in the calculated direction
            BulletIns.GetComponent<Rigidbody2D>().AddForce(forceDirection * bulletSpeed, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning("BulletPrefab or ShootPoint is not assigned!");
        }

        // Re-enable shooting after a short delay
        Invoke(nameof(ResetShoot), 0.2f);
    }

    private void ResetShoot()
    {
        canShoot = true;
    }
}
