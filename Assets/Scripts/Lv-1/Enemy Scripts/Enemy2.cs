using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour
{
    public static Enemy2 Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Enemy2.cs Instance created.");
        }
        else
        {
            Debug.LogWarning("Multiple Enemy2.cs instances detected. Destroying duplicate.");
            Destroy(gameObject);
        }
        if (circleChild2 == null)
        {
            var circleObj = GameObject.Find("Circle_player_2d");
            if (circleObj != null) circleChild2 = circleObj.transform;
            else Debug.LogError("Circle_player_2d not found!");
        }

        if (squareChild2 == null)
        {
            var squareObj = GameObject.Find("Square_player_2d");
            if (squareObj != null) squareChild2 = squareObj.transform;
            else Debug.LogError("Square_player_2d not found!");
        }

        if (triangleChild2 == null)
        {
            var triangleObj = GameObject.Find("Triangle_player_2d");
            if (triangleObj != null) triangleChild2 = triangleObj.transform;
            else Debug.LogError("Triangle_player_2d not found!");
        }

    }

    public GameObject enemyreal2;
    public int Enemyhealth2 = 3;

    public Animator EnemyAnimator2; // Reference to the enemy's Animator

    //public Transform Playere; // Player's Transform (Parent)
    public Transform enemy2; // Enemy's Transform
    public float SeeingEnemy2 = 28f; // Detection range
    public LayerMask attackLayer2; // Layer to detect the player
    public float Speed2 = 3f; // Movement speed
    public float attackRange2 = 6f; // Range to trigger attack animation
    public bool isChasingPlayer2 = false; // Track if the enemy is chasing the player

    // References to player's child objects (circle, square, and triangle)
    public Transform circleChild2;
    public Transform squareChild2;
    public Transform triangleChild2;

    private Transform currentActiveChild2; // The active child of the player (circle, square, or triangle)

    public Button shootBtn;

    void Update()
    {
        // Automatically determine which sprite is active
        SetActiveChild();

        // Detect if the player is within range
        Collider2D ComeNearEnemy = Physics2D.OverlapCircle(transform.position, SeeingEnemy2, attackLayer2);

        if (ComeNearEnemy != null && !isChasingPlayer2) // Player is in range
        {
            isChasingPlayer2 = true; // Start chasing the player
            Debug.Log("Player detected! Starting chase...");
        }

        if (isChasingPlayer2 && currentActiveChild2 != null)
        {
            // Use the active child's position for chasing logic
            Vector2 playerPosition = currentActiveChild2.position;

            // Calculate the distance between the player and the enemy
            float distanceToPlayer = Vector2.Distance(playerPosition, enemy2.position);




            if (distanceToPlayer <= attackRange2)
            {
                // Bool attack animation
                EnemyAnimator2.SetBool("Attack", true);
                Debug.Log("Enemy is attacking!");

            }
            else
            {
                EnemyAnimator2.SetBool("Attack", false);
                Debug.Log("Enemy is Moving Towards Player!");
            }

            // Check if player is to the right or left of the enemy
            if (playerPosition.x > enemy2.position.x)
            {
                // Move enemy right
                enemy2.position = new Vector2(enemy2.position.x + Speed2 * Time.deltaTime, enemy2.position.y);

                // Flip to face the player (right)
                enemy2.eulerAngles = new Vector3(0f, -180f, 0f); // Face right
            }
            else if (playerPosition.x < enemy2.position.x)
            {
                // Move enemy left
                enemy2.position = new Vector2(enemy2.position.x - Speed2 * Time.deltaTime, enemy2.position.y);

                // Flip to face the player (left)
                enemy2.eulerAngles = new Vector3(0f, 0f, 0f); // Face left
            }
        }
        else if (isChasingPlayer2 && currentActiveChild2 == null)
        {
            Debug.LogWarning("Active child is null. Cannot chase player.");
        }

        if (Enemyhealth2 <= 0)
        {
            Die();
        }
    }

    public void DamageCollectedByEnemy2(int damage)
    {
        if (Enemyhealth2 <= 0)
        {
            return;
        }
        Enemyhealth2 -= damage;
    }

    private void Die()
    {
        shootBtn.interactable = false;
        StartCoroutine(DieAfterAnimation());

        Debug.Log("Enemy2.cs script destroyed.");
    }

    private IEnumerator DieAfterAnimation()
    {
        EnemyAnimator2.SetTrigger("Die");

        yield return new WaitForSeconds(1.301f);
        Destroy(enemyreal2);
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
        if (circleChild2 != null && circleChild2.gameObject.activeSelf)
        {
            currentActiveChild2 = circleChild2; // Set active to circle
        }
        else if (squareChild2 != null && squareChild2.gameObject.activeSelf)
        {
            currentActiveChild2 = squareChild2; // Set active to square
        }
        else if (triangleChild2 != null && triangleChild2.gameObject.activeSelf)
        {
            currentActiveChild2 = triangleChild2; // Set active to triangle
        }
        else
        {
            currentActiveChild2 = null; // No active child
            Debug.LogWarning("No active child found. Ensure player objects are initialized.");
        }
    }


    private void OnDrawGizmosSelected()
    {
        // Draw the detection range in the scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SeeingEnemy2);

        // Draw the attack range in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange2);
    }
}