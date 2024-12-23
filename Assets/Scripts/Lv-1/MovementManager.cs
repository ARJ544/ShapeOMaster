using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementManager : MonoBehaviour
{
    public Enemy enemy;

    [Header("Player Health")]
    [SerializeField] public int circle_health = 5;
    [SerializeField] public int square_health = 10;
    [SerializeField] public int triangle_health = 5;

    [Header("Button")]
    [SerializeField] public Button shootBtn;

    [Header("Game Objects")]
    [SerializeField] public GameObject PlayerGameObject;
    [SerializeField] public GameObject EnemyGameObject;
    [SerializeField] public GameObject circle;
    [SerializeField] public GameObject triangle;
    [SerializeField] public GameObject square;
    [SerializeField] public GameObject currentActiveSprite;

    [Header("Animator")]
    [SerializeField] public Animator circle_animator;
    [SerializeField] public Animator square_animator;
    [SerializeField] public Animator triangle_animator;
    [SerializeField] private Animator currentActiveSprite_animator;

    [Header("RigidBody")]
    [SerializeField] public Rigidbody2D circle_rb;
    [SerializeField] public Rigidbody2D square_rb;
    [SerializeField] public Rigidbody2D triangle_rb;
    [SerializeField] private Rigidbody2D currentActiveRb;

    [Header("Float")]
    [SerializeField] public float jumpvalue = 10f;
    [SerializeField] public float circleSpeed = 25f;
    [SerializeField] public float triangleSpeed = 15f;
    [SerializeField] public float squareSpeed = 10f;
    [SerializeField] private float move;
    [SerializeField] private float currentSpeed;

    [Header("Bool")]
    [SerializeField] public bool currentPlayerIsOnGround = true;
    [SerializeField] public bool currentPlayerIsFacingRight = true;

    [Header("Panel")]
    [SerializeField] public GameObject GameOverPanel;

    public HealthBarCircle healthBarCircle;
    public HealthBarSquare healthBarSquare;
    public HealthBarTriangle healthBarTriangle;

    void Start()
    {
        Time.timeScale = 1f;
        GameOverPanel.SetActive(false);
        healthBarCircle.SetMaxHealth(circle_health);

        ActivateSprite(circle, circleSpeed);

        // Get Rigidbody2D components for each sprite
        circle_rb = circle.GetComponent<Rigidbody2D>();
        triangle_rb = triangle.GetComponent<Rigidbody2D>();
        square_rb = square.GetComponent<Rigidbody2D>();

        // Set the initial Rigidbody2D reference to the active sprite's Rigidbody2D
        currentActiveRb = circle_rb;

        // Initialize the animator for the active sprite
        currentActiveSprite_animator = circle_animator;
    }

    void Update()
    {
        // Get input
        move = SimpleInput.GetAxis("Horizontal");

        // Check if the character needs to flip direction
        if (move > 0 && !currentPlayerIsFacingRight)
        {
            Flip(circle); // Face right
            Flip(square); // Face right
            Flip(triangle); // Face right
        }
        else if (move < 0 && currentPlayerIsFacingRight)
        {
            Flip(circle);
            Flip(square);
            Flip(triangle); // Face left
        }

        MoveAllSprites(move);

        // Running Animation
        if (Mathf.Abs(move) > 0.1f)
        {
            currentActiveSprite_animator.SetFloat("Speed", 1f); // Animation is playing

            Transform parentTransform = currentActiveSprite.transform;  // Replace 'parentGameObject' with your GameObject
            foreach (Transform child in parentTransform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            currentActiveSprite_animator.SetFloat("Speed", 0f); // Animation stops

            Transform parentTransform = currentActiveSprite.transform;  // Replace 'parentGameObject' with your GameObject
            foreach (Transform child in parentTransform)
            {
                child.gameObject.SetActive(true);
            }
        }

        // Check health for each shape and call Die if any health is zero
        if (circle_health <= 0 && currentActiveSprite == circle)
        {
            Die();
        }
        if (square_health <= 0 && currentActiveSprite == square)
        {
            Die();
        }
        if (triangle_health <= 0 && currentActiveSprite == triangle)
        {
            Die();
        }
    }

    private void MoveAllSprites(float moveDirection)
    {
        // Get the current X position of the active sprite
        float currentXPosition = currentActiveSprite.transform.position.x;

        // Apply the movement to all sprites (circle, square, triangle) while keeping the Y position the same
        circle.transform.position = new Vector3(currentXPosition + moveDirection * currentSpeed * Time.deltaTime, circle.transform.position.y, circle.transform.position.z);
        triangle.transform.position = new Vector3(currentXPosition + moveDirection * currentSpeed * Time.deltaTime, triangle.transform.position.y, triangle.transform.position.z);
        square.transform.position = new Vector3(currentXPosition + moveDirection * currentSpeed * Time.deltaTime, square.transform.position.y, square.transform.position.z);
    }

    private void FixedUpdate()
    {
        // Apply horizontal movement in world space
        Vector3 movement = new Vector3(move, 0f, 0f) * currentSpeed * Time.fixedDeltaTime;
        transform.Translate(movement, Space.World);
    }

    private void Flip(GameObject sprite)
    {
        if (sprite == null)
        {
            Debug.LogWarning("Sprite to flip is null!");
            return;
        }

        // Flip the visual sprite by inverting its local scale on the X axis
        Vector3 scale = sprite.transform.localScale;
        scale.x *= -1; // Invert X scale
        sprite.transform.localScale = scale;

        // Toggle the facing direction of the character if the flipped sprite is the current active one
        if (sprite == currentActiveSprite)
        {
            currentPlayerIsFacingRight = !currentPlayerIsFacingRight;
        }
    }

    // Function to activate a specific sprite and set its speed
    // Function to activate a specific sprite and set its speed
    public void ActivateSprite(GameObject spriteToActivate, float speed)
    {
        if (circle == null || triangle == null || square == null)
        {
            Debug.LogError("One or more shape references are missing!");
            return;
        }

        // Deactivate all sprites
        circle.SetActive(false);
        triangle.SetActive(false);
        square.SetActive(false);

        // Activate the selected sprite
        if (spriteToActivate != null)
        {
            spriteToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("The sprite to activate is null!");
        }

        // Set the current active sprite and its speed
        currentActiveSprite = spriteToActivate;
        currentSpeed = speed;

        // Update the current active Rigidbody2D reference and animator based on the active sprite
        if (spriteToActivate == circle)
        {
            currentActiveRb = circle_rb;
            currentActiveSprite_animator = circle_animator;
            shootBtn.interactable = false;

            // Activate Circle health bar and deactivate others
            healthBarCircle.gameObject.SetActive(true);
            healthBarTriangle.gameObject.SetActive(false);
            healthBarSquare.gameObject.SetActive(false);

            // Update health bar
            healthBarCircle.SetHealth(circle_health);
        }
        else if (spriteToActivate == triangle)
        {
            currentActiveRb = triangle_rb;
            currentActiveSprite_animator = triangle_animator;
            shootBtn.interactable = true;

            // Activate Triangle health bar and deactivate others
            healthBarTriangle.gameObject.SetActive(true);
            healthBarCircle.gameObject.SetActive(false);
            healthBarSquare.gameObject.SetActive(false);

            // Update health bar
            healthBarTriangle.SetHealth(triangle_health);
        }
        else if (spriteToActivate == square)
        {
            currentActiveRb = square_rb;
            currentActiveSprite_animator = square_animator;
            shootBtn.interactable = false;

            // Activate Square health bar and deactivate others
            healthBarSquare.gameObject.SetActive(true);
            healthBarCircle.gameObject.SetActive(false);
            healthBarTriangle.gameObject.SetActive(false);

            // Update health bar
            healthBarSquare.SetHealth(square_health);
        }
    }



    // Example methods to switch sprites with respective speeds
    public void ActivateCircle()
    {
        ActivateSprite(circle, circleSpeed);
    }

    public void ActivateTriangle()
    {
        ActivateSprite(triangle, triangleSpeed);
    }

    public void ActivateSquare()
    {
        ActivateSprite(square, squareSpeed);
    }

    // Function to handle jump
    public void jumpbtn()
    {
        if (currentPlayerIsOnGround)
        {
            // Access current active Rigidbody2D velocity and apply jump force
            Vector2 velocity = currentActiveRb.linearVelocity;
            velocity.y = jumpvalue; // Set the Y velocity to simulate jumping
            currentActiveRb.linearVelocity = velocity;

            //currentPlayerIsOnGround = false;
        }
    }

    public void DamageCollectedByCircle(int damage)
    {
        if (circle_health <= 0)
        {
            return;
        }
        circle_health -= damage;
        healthBarCircle.SetHealth(circle_health);

        // Check if health is zero or less
        if (circle_health <= 0)
        {
            Die();
        }
    }

    public void DamageCollectedBySquare(int damage)
    {
        if (square_health <= 0)
        {
            return;
        }
        square_health -= damage;
        healthBarSquare.SetHealth(square_health);

        // Check if health is zero or less
        if (square_health <= 0)
        {
            Die();
        }
    }

    public void DamageCollectedByTriangle(int damage)
    {
        if (triangle_health <= 0)
        {
            return;
        }
        triangle_health -= damage;
        healthBarTriangle.SetHealth(triangle_health);

        // Check if health is zero or less
        if (triangle_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(currentActiveSprite.transform.name + " died");

        // Disable the current active sprite
        currentActiveSprite.SetActive(false);

        // Switch to another sprite based on remaining health
        if (triangle_health > 0)
        {
            ActivateTriangle(); // Switch to triangle
            healthBarTriangle.SetHealth(triangle_health);
        }
        else if (square_health > 0)
        {
            ActivateSquare(); // Switch to square
            healthBarSquare.SetHealth(square_health);
        }
        else if (circle_health > 0)
        {
            ActivateCircle(); // Switch to circle
            healthBarCircle.SetHealth(circle_health);
        }
        else
        {
            Debug.Log("Game Over! All shapes are dead.");
            
            SoundManagerForLV soundManager = FindAnyObjectByType<SoundManagerForLV>();
            if (soundManager != null)
            {
                soundManager.PlayLoseSound();
            }
            Time.timeScale = 0f;
            GameOverPanel.SetActive(true);
            Destroy(EnemyGameObject);
            Destroy(PlayerGameObject);
            Destroy(this);
            Debug.Log("movement.cs script destroyed.");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            currentPlayerIsOnGround = true;
            Debug.Log("On Ground");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            currentPlayerIsOnGround = false;
            Debug.Log("Not on Ground");
        }
    }
}
