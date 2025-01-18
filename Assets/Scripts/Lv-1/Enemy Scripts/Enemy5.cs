using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy5 : MonoBehaviour
{
    public static Enemy5 Instance { get; private set; }

    private void Awake()
    {
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
        if (circleChild5 == null)
        {
            var circleObj = GameObject.Find("Circle_player_2d");
            if (circleObj != null) circleChild5 = circleObj.transform;
            else Debug.LogError("Circle_player_2d not found!");
        }

        if (squareChild5 == null)
        {
            var squareObj = GameObject.Find("Square_player_2d");
            if (squareObj != null) squareChild5 = squareObj.transform;
            else Debug.LogError("Square_player_2d not found!");
        }

        if (triangleChild5 == null)
        {
            var triangleObj = GameObject.Find("Triangle_player_2d");
            if (triangleObj != null) triangleChild5 = triangleObj.transform;
            else Debug.LogError("Triangle_player_2d not found!");
        }

    }

    public GameObject enemyreal5;
    public int Enemyhealth5 = 3;

    public Animator EnemyAnimator5; // Reference to the enemy's Animator

    //public Transform Playere; // Player's Transform (Parent)
    public Transform enemy5; // Enemy's Transform
    public float SeeingEnemy5 = 28f; // Detection range
    public LayerMask attackLayer5; // Layer to detect the player
    public float Speed5 = 3f; // Movement speed
    public float attackRange5 = 6f; // Range to trigger attack animation
    public bool isChasingPlayer5 = false; // Track if the enemy is chasing the player

    // References to player's child objects (circle, square, and triangle)
    public Transform circleChild5;
    public Transform squareChild5;
    public Transform triangleChild5;

    private Transform currentActiveChild5; // The active child of the player (circle, square, or triangle)

    public Button shootBtn;
    void Update()
    {
        // Automatically determine which sprite is active
        SetActiveChild();

        // Detect if the player is within range
        Collider2D ComeNearEnemy = Physics2D.OverlapCircle(transform.position, SeeingEnemy5, attackLayer5);

        if (ComeNearEnemy != null && !isChasingPlayer5) // Player is in range
        {
            isChasingPlayer5 = true; // Start chasing the player
            Debug.Log("Player detected! Starting chase...");
        }

        if (isChasingPlayer5 && currentActiveChild5 != null)
        {
            // Use the active child's position for chasing logic
            Vector2 playerPosition = currentActiveChild5.position;

            // Calculate the distance between the player and the enemy
            float distanceToPlayer = Vector2.Distance(playerPosition, enemy5.position);




            if (distanceToPlayer <= attackRange5)
            {
                // Bool attack animation
                EnemyAnimator5.SetBool("Attack", true);
                Debug.Log("Enemy is attacking!");

            }
            else
            {
                EnemyAnimator5.SetBool("Attack", false);
                Debug.Log("Enemy is Moving Towards Player!");
            }

            // Check if player is to the right or left of the enemy
            if (playerPosition.x > enemy5.position.x)
            {
                // Move enemy right
                enemy5.position = new Vector2(enemy5.position.x + Speed5 * Time.deltaTime, enemy5.position.y);

                // Flip to face the player (right)
                enemy5.eulerAngles = new Vector3(0f, -180f, 0f); // Face right
            }
            else if (playerPosition.x < enemy5.position.x)
            {
                // Move enemy left
                enemy5.position = new Vector2(enemy5.position.x - Speed5 * Time.deltaTime, enemy5.position.y);

                // Flip to face the player (left)
                enemy5.eulerAngles = new Vector3(0f, 0f, 0f); // Face left
            }
        }
        else if (isChasingPlayer5 && currentActiveChild5 == null)
        {
            Debug.LogWarning("Active child is null. Cannot chase player.");
        }

        if (Enemyhealth5 <= 0)
        {
            Die();
        }
    }

    public void DamageCollectedByEnemy5(int damage)
    {
        if (Enemyhealth5 <= 0)
        {
            return;
        }
        Enemyhealth5 -= damage;
    }

    private void Die()
    {
        shootBtn.interactable = false;
        StartCoroutine(DieAfterAnimation());

        Debug.Log("Enemy.cs script destroyed.");
    }

    private IEnumerator DieAfterAnimation()
    {
        EnemyAnimator5.SetTrigger("Die");

        yield return new WaitForSeconds(1.301f);
        Destroy(enemyreal5);
        Destroy(this);

        if (shootBtn != null)
        {
            shootBtn.interactable = true;
            Debug.Log("Shoot button enabled.");
        }
    }

    public void SetActiveChild()
    {
        // Validate objects before accessing their properties
        if (circleChild5 != null && circleChild5.gameObject.activeSelf)
        {
            currentActiveChild5 = circleChild5; // Set active to circle
        }
        else if (squareChild5 != null && squareChild5.gameObject.activeSelf)
        {
            currentActiveChild5 = squareChild5; // Set active to square
        }
        else if (triangleChild5 != null && triangleChild5.gameObject.activeSelf)
        {
            currentActiveChild5 = triangleChild5; // Set active to triangle
        }
        else
        {
            currentActiveChild5 = null; // No active child
            Debug.LogWarning("No active child found. Ensure player objects are initialized.");
        }
    }


    private void OnDrawGizmosSelected()
    {
        // Draw the detection range in the scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SeeingEnemy5);

        // Draw the attack range in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange5);
    }
}