using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy4 : MonoBehaviour
{
    public static Enemy4 Instance { get; private set; }

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
        if (circleChild4 == null)
        {
            var circleObj = GameObject.Find("Circle_player_2d");
            if (circleObj != null) circleChild4 = circleObj.transform;
            else Debug.LogError("Circle_player_2d not found!");
        }

        if (squareChild4 == null)
        {
            var squareObj = GameObject.Find("Square_player_2d");
            if (squareObj != null) squareChild4 = squareObj.transform;
            else Debug.LogError("Square_player_2d not found!");
        }

        if (triangleChild4 == null)
        {
            var triangleObj = GameObject.Find("Triangle_player_2d");
            if (triangleObj != null) triangleChild4 = triangleObj.transform;
            else Debug.LogError("Triangle_player_2d not found!");
        }

    }

    public GameObject enemyreal4;
    public int Enemyhealth4 = 3;

    public Animator EnemyAnimator4; // Reference to the enemy's Animator

    //public Transform Playere; // Player's Transform (Parent)
    public Transform enemy4; // Enemy's Transform
    public float SeeingEnemy4 = 28f; // Detection range
    public LayerMask attackLayer4; // Layer to detect the player
    public float Speed4 = 3f; // Movement speed
    public float attackRange4 = 6f; // Range to trigger attack animation
    public bool isChasingPlayer4 = false; // Track if the enemy is chasing the player

    // References to player's child objects (circle, square, and triangle)
    public Transform circleChild4;
    public Transform squareChild4;
    public Transform triangleChild4;

    private Transform currentActiveChild4; // The active child of the player (circle, square, or triangle)

    public Button shootBtn;
    void Update()
    {
        // Automatically determine which sprite is active
        SetActiveChild();

        // Detect if the player is within range
        Collider2D ComeNearEnemy = Physics2D.OverlapCircle(transform.position, SeeingEnemy4, attackLayer4);

        if (ComeNearEnemy != null && !isChasingPlayer4) // Player is in range
        {
            isChasingPlayer4 = true; // Start chasing the player
            Debug.Log("Player detected! Starting chase...");
        }

        if (isChasingPlayer4 && currentActiveChild4 != null)
        {
            // Use the active child's position for chasing logic
            Vector2 playerPosition = currentActiveChild4.position;

            // Calculate the distance between the player and the enemy
            float distanceToPlayer = Vector2.Distance(playerPosition, enemy4.position);




            if (distanceToPlayer <= attackRange4)
            {
                // Bool attack animation
                EnemyAnimator4.SetBool("Attack", true);
                Debug.Log("Enemy is attacking!");

            }
            else
            {
                EnemyAnimator4.SetBool("Attack", false);
                Debug.Log("Enemy is Moving Towards Player!");
            }

            // Check if player is to the right or left of the enemy
            if (playerPosition.x > enemy4.position.x)
            {
                // Move enemy right
                enemy4.position = new Vector2(enemy4.position.x + Speed4 * Time.deltaTime, enemy4.position.y);

                // Flip to face the player (right)
                enemy4.eulerAngles = new Vector3(0f, -180f, 0f); // Face right
            }
            else if (playerPosition.x < enemy4.position.x)
            {
                // Move enemy left
                enemy4.position = new Vector2(enemy4.position.x - Speed4 * Time.deltaTime, enemy4.position.y);

                // Flip to face the player (left)
                enemy4.eulerAngles = new Vector3(0f, 0f, 0f); // Face left
            }
        }
        else if (isChasingPlayer4 && currentActiveChild4 == null)
        {
            Debug.LogWarning("Active child is null. Cannot chase player.");
        }

        if (Enemyhealth4 <= 0)
        {
            Die();
        }
    }

    public void DamageCollectedByEnemy4(int damage)
    {
        if (Enemyhealth4 <= 0)
        {
            return;
        }
        Enemyhealth4 -= damage;
    }

    private void Die()
    {
        shootBtn.interactable = false;
        StartCoroutine(DieAfterAnimation());

        Debug.Log("Enemy.cs script destroyed.");
    }

    private IEnumerator DieAfterAnimation()
    {
        EnemyAnimator4.SetTrigger("Die");

        yield return new WaitForSeconds(1.301f);
        Destroy(enemyreal4);
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
        if (circleChild4 != null && circleChild4.gameObject.activeSelf)
        {
            currentActiveChild4 = circleChild4; // Set active to circle
        }
        else if (squareChild4 != null && squareChild4.gameObject.activeSelf)
        {
            currentActiveChild4 = squareChild4; // Set active to square
        }
        else if (triangleChild4 != null && triangleChild4.gameObject.activeSelf)
        {
            currentActiveChild4 = triangleChild4; // Set active to triangle
        }
        else
        {
            currentActiveChild4 = null; // No active child
            Debug.LogWarning("No active child found. Ensure player objects are initialized.");
        }
    }


    private void OnDrawGizmosSelected()
    {
        // Draw the detection range in the scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SeeingEnemy4);

        // Draw the attack range in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange4);
    }
}