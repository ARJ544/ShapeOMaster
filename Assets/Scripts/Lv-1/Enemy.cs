using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance { get; private set; }

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Enemy.cs Instance created.");
        }
        else
        {
            Debug.LogWarning("Multiple Enemy.cs instances detected. Destroying duplicate.");
            Destroy(gameObject);
        }

    }

    public GameObject enemyreal;
    public int Enemyhealth = 3;

    public Animator EnemyAnimator; // Reference to the enemy's Animator

    public Transform Playere; // Player's Transform (Parent)
    public Transform enemy; // Enemy's Transform
    public float SeeingEnemy = 28f; // Detection range
    public LayerMask attackLayer; // Layer to detect the player
    public float Speed = 3f; // Movement speed
    public float attackRange = 6f; // Range to trigger attack animation
    private bool isChasingPlayer = false; // Track if the enemy is chasing the player

    // References to player's child objects (circle, square, and triangle)
    public Transform circleChild;
    public Transform squareChild;
    public Transform triangleChild;

    private Transform currentActiveChild; // The active child of the player (circle, square, or triangle)

    void Update()
    {
        // Automatically determine which sprite is active
        SetActiveChild();

        // Detect if the player is within range
        Collider2D ComeNearEnemy = Physics2D.OverlapCircle(transform.position, SeeingEnemy, attackLayer);

        if (ComeNearEnemy != null && !isChasingPlayer) // Player is in range
        {
            isChasingPlayer = true; // Start chasing the player
            Debug.Log("Player detected! Starting chase...");
        }

        if (isChasingPlayer)
        {
            // Use the active child's position for chasing logic
            Vector2 playerPosition = currentActiveChild.position;

            // Calculate the distance between the player and the enemy
            float distanceToPlayer = Vector2.Distance(playerPosition, enemy.position);

            


            if (distanceToPlayer <= attackRange)
            {
                // Bool attack animation
                EnemyAnimator.SetBool("Attack",true);
                Debug.Log("Enemy is attacking!");
           
            }
            else
            {
                EnemyAnimator.SetBool("Attack", false);
                Debug.Log("Enemy is Moving Towards Player!");
            }

            // Check if player is to the right or left of the enemy
            if (playerPosition.x > enemy.position.x)
            {
                // Move enemy right
                enemy.position = new Vector2(enemy.position.x + Speed * Time.deltaTime, enemy.position.y);

                // Flip to face the player (right)
                enemy.eulerAngles = new Vector3(0f, -180f, 0f); // Face right
            }
            else if (playerPosition.x < enemy.position.x)
            {
                // Move enemy left
                enemy.position = new Vector2(enemy.position.x - Speed * Time.deltaTime, enemy.position.y);

                // Flip to face the player (left)
                enemy.eulerAngles = new Vector3(0f, 0f, 0f); // Face left
            }
        }

        if(Enemyhealth<=0)
        {
            Die();
        }
    }

    public void DamageCollectedByEnemy(int damage)
    {
        if (Enemyhealth <= 0)
        {
            return;
        }
        Enemyhealth -= damage;
    }

    private void Die()
    {
        StartCoroutine(DieAfterAnimation());
    }

    private IEnumerator DieAfterAnimation()
    {
        EnemyAnimator.SetTrigger("Die");

        yield return new WaitForSeconds(1.301f);
        Destroy(enemyreal);
    }

    private void SetActiveChild()
    {
        // Check which child is active by checking visibility or GameObject active status
        if (circleChild.gameObject.activeSelf)
        {
            currentActiveChild = circleChild; // Set active to circle
        }
        else if (squareChild.gameObject.activeSelf)
        {
            currentActiveChild = squareChild; // Set active to square
        }
        else if (triangleChild.gameObject.activeSelf)
        {
            currentActiveChild = triangleChild; // Set active to triangle
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the detection range in the scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SeeingEnemy);

        // Draw the attack range in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
