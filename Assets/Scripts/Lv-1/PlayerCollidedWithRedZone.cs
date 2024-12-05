using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollidedWithRedZone : MonoBehaviour
{
    public GameObject GameOver_Panel;
    void Start()
    {
        GameOver_Panel.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            GameOver_Panel.SetActive(true);
            Debug.Log("Player Game Over");
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
}
