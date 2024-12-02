using UnityEngine;
using UnityEngine.UI;

public class Lv1_btn_manager : MonoBehaviour
{
    //public Button JumpBtn;
    [Header("Game Objects")]
    [SerializeField] public GameObject circle;
    [SerializeField] public GameObject triangle;
    [SerializeField] public GameObject square;
    [SerializeField] private GameObject currentActiveSprite;


    [Header("RigidBody")]
    [SerializeField] public Rigidbody2D circle_rb;  // Corrected the variable name
    [SerializeField] public Rigidbody2D square_rb;
    [SerializeField] public Rigidbody2D triangle_rb;
    [SerializeField] private Rigidbody2D currentActiveRb;


    [Header("Float")]
    [SerializeField] public float jumpvalue = 7f;
    [SerializeField] public float circleSpeed = 25f;
    [SerializeField] public float triangleSpeed = 15f;
    [SerializeField] public float squareSpeed = 10f;
    [SerializeField] private float move;
    [SerializeField] private float currentSpeed;


    [Header("Bool")]
    [SerializeField] public bool currentPlayerIsOnGround = true;
    [SerializeField] public bool currentPlayerIsFacingRight = true;


    void Start()
    {
        ActivateSprite(circle, circleSpeed);

        // Get Rigidbody2D components for each sprite
        circle_rb = circle.GetComponent<Rigidbody2D>(); 
        triangle_rb = triangle.GetComponent<Rigidbody2D>();
        square_rb = square.GetComponent<Rigidbody2D>();

        // Set the initial Rigidbody2D reference to the active sprite's Rigidbody2D
        currentActiveRb = circle_rb;
    }

    void Update()
    {
        // Get input
        move = SimpleInput.GetAxis("Horizontal");

        // Check if the character needs to flip direction
        if (move > 0 && !currentPlayerIsFacingRight)
        {
            Flip(); // Face right
        }
        else if (move < 0 && currentPlayerIsFacingRight)
        {
            Flip(); // Face left
        }
    }

    private void FixedUpdate()
    {
        // Apply horizontal movement in world space
        Vector3 movement = new Vector3(move, 0f, 0f) * currentSpeed * Time.fixedDeltaTime;
        transform.Translate(movement, Space.World);
    }

    private void Flip()
    {
        currentPlayerIsFacingRight = !currentPlayerIsFacingRight; // Toggle direction

        // Flip only the visual sprite without changing position
        Vector3 scale = currentActiveSprite.transform.localScale;
        scale.x *= -1; // Invert X scale
        currentActiveSprite.transform.localScale = scale;
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
            currentActiveRb.linearVelocity = velocity;
            // Apply the modified velocity

            //currentPlayerIsOnGround = false;  // Set the player state to not on the ground

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") // Check if the collided GameObject is the ground
        {
            
            currentPlayerIsOnGround = true;
            Debug.Log("On Ground");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // When the object exits collision with the ground, set the player state to not on the ground
        if (collision.gameObject.tag == "Ground")
        {
            currentPlayerIsOnGround = false;
            Debug.Log("no Ground");
        }
    }
}
