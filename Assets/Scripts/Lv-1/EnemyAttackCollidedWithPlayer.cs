using UnityEngine;

public class EnemyAttackCollidedWithPlayer : MonoBehaviour
{
    public Transform enemyAttackingPoint;
    public float enemyAttackingPoint_Radius = 1f;
    public LayerMask attackLayer;

    // Reference to MovementManager
    public MovementManager movementManager;

    void Start()
    {
        if (movementManager == null)
        {
            movementManager = FindObjectOfType<MovementManager>(); // If not set in Inspector, try to find it
        }
    }

    void Update()
    {

    }

    public void AttackOnPlayer()
    {
        Collider2D playerBeingAttacked = Physics2D.OverlapCircle(enemyAttackingPoint.position, enemyAttackingPoint_Radius, attackLayer);

        if (playerBeingAttacked)
        {
            // Log the attack on the player
            Debug.Log(playerBeingAttacked.transform.name + " takes damage");

            // Get the current active sprite from MovementManager
            GameObject currentActiveSprite = movementManager.currentActiveSprite;

            // Check which sprite is currently active and apply damage accordingly
            if (currentActiveSprite == movementManager.circle)
            {
                // Apply damage to the circle
                movementManager.DamageCollectedByCircle(1); // You can adjust the damage value here
            }
            else if (currentActiveSprite == movementManager.square)
            {
                // Apply damage to the square
                movementManager.DamageCollectedBySquare(1); // You can adjust the damage value here
            }
            else if (currentActiveSprite == movementManager.triangle)
            {
                // Apply damage to the triangle
                movementManager.DamageCollectedByTriangle(1); // You can adjust the damage value here
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the detection range in the scene view
        Gizmos.color = new Color(1f, 0.647f, 0f);
        Gizmos.DrawWireSphere(enemyAttackingPoint.position, enemyAttackingPoint_Radius);
    }
}
