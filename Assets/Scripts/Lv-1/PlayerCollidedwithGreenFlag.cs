using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollidedWithGreenFlag : MonoBehaviour
{

    //public MovementManager movementManager;
    //public Enemy enemy;
    //public int nextLvindex;

    public GameObject LevelCompleted_Panel;
    public GameObject EnemiesNotDestroyed_Panel;

    public int coinsAmt = 20;
    public int gemsAmt = 1;

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
            GameObject[] enemies1 = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] enemies2 = GameObject.FindGameObjectsWithTag("Enemy2");
            GameObject[] enemies3 = GameObject.FindGameObjectsWithTag("Enemy3");
            GameObject[] enemies4 = GameObject.FindGameObjectsWithTag("Enemy4");
            GameObject[] enemies5 = GameObject.FindGameObjectsWithTag("Enemy5");

            int totalEnemies = enemies1.Length + enemies2.Length + enemies3.Length + enemies4.Length + enemies5.Length;

            // Check if there are any enemies remaining
            if (totalEnemies > 0)
            {
                Debug.Log("Enemies are still present. Finish them before completing the level.");

                if (!EnemiesNotDestroyed_Panel.activeSelf)
                {
                    EnemiesNotDestroyed_Panel.SetActive(true); // Show the panel
                    Invoke("DeactivateEnemiesPanel", 1f);
                }

                return; // Exit the method without proceeding to level completion
            }

            // Reward the player if DataManager is available
            if (DataManager.Instance != null)
            {
                DataManager.Instance.AddCoins(coinsAmt);
                DataManager.Instance.AddGems(gemsAmt);
            }
            else
            {
                Debug.LogWarning("DataManager.Instance is null. Ensure it is initialized in the Main Menu.");
            }

            // Display the Level Completed panel and pause the game
            Time.timeScale = 0f;
            SoundManagerForLV soundManager = FindAnyObjectByType<SoundManagerForLV>();
            if (soundManager != null)
            {
                soundManager.PlayWinSound();
            }

            LevelCompleted_Panel.SetActive(true);
            UnlockNewLevel();

            //LevelLockManager.Instance.UnlockNextLevel(nextLvindex);

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
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevelIndex);
        }
        else
        {
            Debug.LogWarning("No more levels to load.");
        }
    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

}
