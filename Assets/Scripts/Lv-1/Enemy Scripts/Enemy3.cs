using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy3 : MonoBehaviour
{
    public static Enemy3 Instance { get; private set; }

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
        if (circleChild3 == null)
        {
            var circleObj = GameObject.Find("Circle_player_2d");
            if (circleObj != null) circleChild3 = circleObj.transform;
            else Debug.LogError("Circle_player_2d not found!");
        }

        if (squareChild3 == null)
        {
            var squareObj = GameObject.Find("Square_player_2d");
            if (squareObj != null) squareChild3 = squareObj.transform;
            else Debug.LogError("Square_player_2d not found!");
        }

        if (triangleChild3 == null)
        {
            var triangleObj = GameObject.Find("Triangle_player_2d");
            if (triangleObj != null) triangleChild3 = triangleObj.transform;
            else Debug.LogError("Triangle_player_2d not found!");
        }

    }

    public GameObject enemyreal3;
    public int Enemyhealth3 = 3;

    public Animator EnemyAnimator3; // Reference to the enemy's Animator

    //public Transform Playere; // Player's Transform (Parent)
    public Transform enemy3; // Enemy's Transform
    public float SeeingEnemy3 = 28f; // Detection range
    public LayerMask attackLayer3; // Layer to detect the player
    public float Speed3 = 3f; // Movement speed
    public float attackRange3 = 6f; // Range to trigger attack animation
    public bool isChasingPlayer3 = false; // Track if the enemy is chasing the player

    // References to player's child objects (circle, square, and triangle)
    public Transform circleChild3;
    public Transform squareChild3;
    public Transform triangleChild3;

    private Transform currentActiveChild3; // The active child of the player (circle, square, or triangle)

    public Button shootBtn;
    void Update()
    {
        // Automatically determine which sprite is active
        SetActiveChild();

        // Detect if the player is within range
        Collider2D ComeNearEnemy = Physics2D.OverlapCircle(transform.position, SeeingEnemy3, attackLayer3);

        if (ComeNearEnemy != null && !isChasingPlayer3) // Player is in range
        {
            isChasingPlayer3 = true; // Start chasing the player
            Debug.Log("Player detected! Starting chase...");
        }

        if (isChasingPlayer3 && currentActiveChild3 != null)
        {
            // Use the active child's position for chasing logic
            Vector2 playerPosition = currentActiveChild3.position;

            // Calculate the distance between the player and the enemy
            float distanceToPlayer = Vector2.Distance(playerPosition, enemy3.position);




            if (distanceToPlayer <= attackRange3)
            {
                // Bool attack animation
                EnemyAnimator3.SetBool("Attack", true);
                Debug.Log("Enemy is attacking!");

            }
            else
            {
                EnemyAnimator3.SetBool("Attack", false);
                Debug.Log("Enemy is Moving Towards Player!");
            }

            // Check if player is to the right or left of the enemy
            if (playerPosition.x > enemy3.position.x)
            {
                // Move enemy right
                enemy3.position = new Vector2(enemy3.position.x + Speed3 * Time.deltaTime, enemy3.position.y);

                // Flip to face the player (right)
                enemy3.eulerAngles = new Vector3(0f, -180f, 0f); // Face right
            }
            else if (playerPosition.x < enemy3.position.x)
            {
                // Move enemy left
                enemy3.position = new Vector2(enemy3.position.x - Speed3 * Time.deltaTime, enemy3.position.y);

                // Flip to face the player (left)
                enemy3.eulerAngles = new Vector3(0f, 0f, 0f); // Face left
            }
        }
        else if (isChasingPlayer3 && currentActiveChild3 == null)
        {
            Debug.LogWarning("Active child is null. Cannot chase player.");
        }

        if (Enemyhealth3 <= 0)
        {
            Die();
        }
    }

    public void DamageCollectedByEnemy3(int damage)
    {
        if (Enemyhealth3 <= 0)
        {
            return;
        }
        Enemyhealth3 -= damage;
    }

    private void Die()
    {
        shootBtn.interactable = false;
        StartCoroutine(DieAfterAnimation());

        Debug.Log("Enemy.cs script destroyed.");
    }

    private IEnumerator DieAfterAnimation()
    {
        EnemyAnimator3.SetTrigger("Die");

        yield return new WaitForSeconds(1.301f);
        Destroy(enemyreal3);
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
        if (circleChild3 != null && circleChild3.gameObject.activeSelf)
        {
            currentActiveChild3 = circleChild3; // Set active to circle
        }
        else if (squareChild3 != null && squareChild3.gameObject.activeSelf)
        {
            currentActiveChild3 = squareChild3; // Set active to square
        }
        else if (triangleChild3 != null && triangleChild3.gameObject.activeSelf)
        {
            currentActiveChild3 = triangleChild3; // Set active to triangle
        }
        else
        {
            currentActiveChild3 = null; // No active child
            Debug.LogWarning("No active child found. Ensure player objects are initialized.");
        }
    }


    private void OnDrawGizmosSelected()
    {
        // Draw the detection range in the scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SeeingEnemy3);

        // Draw the attack range in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange3);
    }
}