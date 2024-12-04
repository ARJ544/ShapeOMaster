using UnityEngine;

public class PlayerCollidedWithGreenFlag : MonoBehaviour
{
    public GameObject LevelCompleted_Panel;
    void Start()
    {
        LevelCompleted_Panel.SetActive(false);
    }

    void Update()
    {
    }

    // Trigger method for detecting collisions with the Player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelCompleted_Panel.SetActive(true);
            Debug.Log("Player Wins");
        }
    }

    public void closebtn()
    {
        LevelCompleted_Panel.SetActive(false);
    }
}
