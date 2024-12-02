using UnityEngine;

public class Lv1_btn_manager : MonoBehaviour
{
    public Rigidbody2D circle_rb;  // Corrected the variable name
    public Rigidbody2D square_rb;
    public Rigidbody2D triangle_rb;
    public float jumpvalue = 7f;
    private float move;
    private float currentSpeed;
    private bool currentPlayerIsOnGround = true;  // Flag to check if the player is on the ground

    // References to the sprite GameObjects
    public GameObject circle;
    public GameObject triangle;
    public GameObject square;

    // Speeds for each shape
    public float circleSpeed = 25f;
    public float triangleSpeed = 15f;
    public float squareSpeed = 10f;

    private GameObject currentActiveSprite; // The currently active sprite
    private Rigidbody2D currentActiveRb;    // The current active Rigidbody2D

    void Start()
    {
        ActivateSprite(circle, circleSpeed);

        // Get Rigidbody2D components for each sprite
        circle_rb = circle.GetComponent<Rigidbody2D>();  // Use 'circle_rb' here
        triangle_rb = triangle.GetComponent<Rigidbody2D>();
        square_rb = square.GetComponent<Rigidbody2D>();

        // Set the initial Rigidbody2D reference to the active sprite's Rigidbody2D
        currentActiveRb = circle_rb;
    }

    void Update()
    {
        // Capture horizontal movement input
        move = SimpleInput.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        // Move the parent GameObject with the speed of the current active sprite
        transform.position += new Vector3(move, 0f, 0f) * currentSpeed * Time.fixedDeltaTime;
    }

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

        // Update the current active Rigidbody2D reference based on the active sprite
        if (spriteToActivate == circle)
        {
            currentActiveRb = circle_rb;
        }
        else if (spriteToActivate == triangle)
        {
            currentActiveRb = triangle_rb;
        }
        else if (spriteToActivate == square)
        {
            currentActiveRb = square_rb;
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
            Vector2 velocity = currentActiveRb.linearVelocity;  // Use '.velocity' instead of '.linearVelocity'
            velocity.y = jumpvalue;  // Set the Y velocity to simulate jumping
            currentActiveRb.linearVelocity = velocity;  // Apply the modified velocity

            currentPlayerIsOnGround = false;  // Set the player state to not on the ground
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When the object collides with an object tagged as "Ground", set the player state to on the ground
        if (collision.gameObject.tag == "Ground")
        {
            currentPlayerIsOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // When the object exits collision with the ground, set the player state to not on the ground
        if (collision.gameObject.tag == "Ground")
        {
            currentPlayerIsOnGround = false;
        }
    }
}
