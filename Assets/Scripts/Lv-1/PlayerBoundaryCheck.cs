using UnityEngine;

public class PlayerBoundaryCheck : MonoBehaviour
{
    [Header("Game Over Settings")]
    public GameObject gameOverPanel; // Reference to the Game Over panel
    public float topBoundaryOffset = 10f; // Fixed offset above the camera

    private float fixedTopBoundary; // Store the calculated boundary

    void Start()
    {
        // Calculate the initial top boundary in world space
        fixedTopBoundary = Camera.main.transform.position.y + topBoundaryOffset;

        // Ensure Game Over panel is initially inactive
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        CheckPlayerOutOfBounds();
    }

    void CheckPlayerOutOfBounds()
    {
        // Check if the player goes beyond the fixed top boundary
        if (transform.position.y > fixedTopBoundary)
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        Debug.Log("Game Over: Player went out of bounds!");

        // Activate the Game Over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
    }
}
