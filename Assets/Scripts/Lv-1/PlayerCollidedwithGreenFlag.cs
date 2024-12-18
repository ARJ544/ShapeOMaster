using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollidedWithGreenFlag : MonoBehaviour
{
    public GameObject LevelCompleted_Panel;
    public GameObject EnemiesNotDestroyed_Panel;

    void Start()
    {
        EnemiesNotDestroyed_Panel.SetActive(false);
        LevelCompleted_Panel.SetActive(false);
        Time.timeScale = 1f;
    }

    // Trigger method for detecting collisions with the Player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Check if any enemies are left in the scene
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
            {
                Debug.Log("Enemies are still present. Finish them before completing the level.");

                if (!EnemiesNotDestroyed_Panel.activeSelf)
                {
                    EnemiesNotDestroyed_Panel.SetActive(true); // Show the panel
                    Invoke("DeactivateEnemiesPanel", 1f); // Hide it after 2 seconds (or any duration you prefer)
                }

                return; // Exit the method without proceeding to level completion
            }

            // Reward the player if DataManager is available
            if (DataManager.Instance != null)
            {
                DataManager.Instance.AddCoins(20);
                DataManager.Instance.AddGems(1);
            }
            else
            {
                Debug.LogWarning("DataManager.Instance is null. Ensure it is initialized in the Main Menu.");
            }

            // Display the Level Completed panel and pause the game
            Time.timeScale = 0f;
            LevelCompleted_Panel.SetActive(true);

            Debug.Log("Player Wins");
        }
    }

    private void DeactivateEnemiesPanel()
    {
        EnemiesNotDestroyed_Panel.SetActive(false);
    }

    public void Retrybtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void returnToHome()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void Nextlevel()
    {
        SceneManager.LoadScene("Lv-2");
    }
}
