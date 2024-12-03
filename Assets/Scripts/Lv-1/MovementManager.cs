using UnityEngine;
using UnityEngine.UI;

public class Lv1_btn_manager : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] public Button shootBtn;

    [Header("Game Objects")]
    [SerializeField] public GameObject circle;
    [SerializeField] public GameObject triangle;
    [SerializeField] public GameObject square;
    [SerializeField] private GameObject currentActiveSprite;

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

        //Running Animation
        if (Mathf.Abs(move) > 0.1f)
        {
            currentActiveSprite_animator.SetFloat("Speed", 1f);//Animation is playing

            Transform parentTransform = currentActiveSprite.transform;  // Replace 'parentGameObject' with your GameObject
            foreach (Transform child in parentTransform)
            {
                
                child.gameObject.SetActive(false);  
            }
        }
        else
        {
            currentActiveSprite_animator.SetFloat("Speed", 0f);//Animation stops

            Transform parentTransform = currentActiveSprite.transform;  // Replace 'parentGameObject' with your GameObject
            foreach (Transform child in parentTransform)
            {

                child.gameObject.SetActive(true);
            }
        }
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
            currentActiveSprite_animator = circle_animator;
            shootBtn.interactable = false;
        }
        else if (spriteToActivate == triangle)
        {
            currentActiveRb = triangle_rb;
            currentActiveSprite_animator = triangle_animator;
            shootBtn.interactable = true;
        }
        else if (spriteToActivate == square)
        {
            currentActiveRb = square_rb;
            currentActiveSprite_animator = square_animator;
            shootBtn.interactable = false;
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
