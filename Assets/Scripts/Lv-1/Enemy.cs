using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Playere; // Player's Transform
    public float SeeingEnemy = 3f; // Detection range
    public LayerMask attackLayer; // Layer to detect the player
    public float Speed = 3f; // Movement speed
    private bool facingLeft = true; // To track which direction the enemy is facing
    private bool isChasingPlayer = false; // Track if the enemy is chasing the player

    public Transform DetectPoint; // Detection point for patrolling
    public float Distance; // Distance for detecting edges
    public LayerMask targetLayer; // Layer for ground detection

    void Update()
    {
        if (isChasingPlayer)
        {
            // Calculate direction toward the player
            Vector2 direction = (Playere.position - transform.position).normalized;

            // Move toward the player
            transform.Translate(new Vector2(direction.x, 0f) * Speed * Time.deltaTime);

            // Flip the enemy to face the player
            if (Playere.position.x > transform.position.x && facingLeft)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f); // Face right
                facingLeft = false;
            }
            else if (Playere.position.x < transform.position.x && !facingLeft)
            {
                transform.eulerAngles = new Vector3(0f, -180f, 0f); // Face left
                facingLeft = true;
            }
        }
        else
        {
            // Detect if the player is within range
            Collider2D ComeNearEnemy = Physics2D.OverlapCircle(transform.position, SeeingEnemy, attackLayer);

            if (ComeNearEnemy != null) // Player is in range
            {
                isChasingPlayer = true; // Switch to chasing the player
                Debug.Log("Player detected! Starting chase...");
            }
            else
            {
                // Patrolling logic
                transform.Translate(Vector2.left * Time.deltaTime * Speed);

                RaycastHit2D collInfo = Physics2D.Raycast(DetectPoint.position, Vector2.down, Distance, targetLayer);

                if (collInfo.collider == null) // Flip when there's no ground
                {
                    if (facingLeft)
                    {
                        transform.eulerAngles = new Vector3(0f, -180f, 0f);
                        facingLeft = false;
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0f, 0f, 0f);
                        facingLeft = true;
                    }
                }
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        // Draw the detection range in the scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SeeingEnemy);

        if (DetectPoint == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawRay(DetectPoint.position, Vector2.down * Distance);
    }
}