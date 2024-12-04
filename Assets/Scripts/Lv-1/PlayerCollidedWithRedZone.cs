using UnityEngine;

public class PlayerCollidedWithRedZone : MonoBehaviour
{
    public GameObject GameOver_Panel;
    void Start()
    {
        GameOver_Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameOver_Panel.SetActive(true);
            Debug.Log("Player Game Over");
        }
    }

    public void closebtn()
    {
        GameOver_Panel.SetActive(false);
    }
}
