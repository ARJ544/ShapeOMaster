using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollidedWithGreenFlag : MonoBehaviour
{
    public GameObject LevelCompleted_Panel;
    void Start()
    {
        LevelCompleted_Panel.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
    }

    // Trigger method for detecting collisions with the Player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            LevelCompleted_Panel.SetActive(true);

            if (DataManager.Instance != null)
            {
                DataManager.Instance.AddCoins(20);
                DataManager.Instance.AddGems(1);
            }
            else
            {
                Debug.LogWarning("DataManager.Instance is null. Ensure it is initialized in the Main Menu.");
            }

            Debug.Log("Player Wins");
        }
    }


    public void Retrybtn()
    {
        SceneManager.LoadScene("Lv-1");
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
