using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause_btn_clicked : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] public GameObject Pause_Panel;
    [SerializeField] public GameObject Home_btn_clicked_Panel;
    void Start()
    {
        Time.timeScale = 1f;
        Pause_Panel.SetActive(false);
        Home_btn_clicked_Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pause_btn_clicked()
    {
        Home_btn_clicked_Panel.SetActive(false);
        Pause_Panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GotoHome_btn_clicked()
    {
        Home_btn_clicked_Panel.SetActive(true);
        
    }

    public void Restart_btn_clicked()
    {
        SceneManager.LoadScene("Lv-1");
    }

    public void Resume_btn_clicked()
    {
        Pause_Panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void homebtnPanel_YES_btn()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void homebtnPanel_NO_btn()
    {
        Home_btn_clicked_Panel.SetActive(false);
    }

}
