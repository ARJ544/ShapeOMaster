using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayPanel_btn_manager : MonoBehaviour
{
    
    void Start()
    {
        
    }


    public void CloseSelectLevel_Panel()
    {
        //DataManager.Instance.AddCoins(10); for testing
        //DataManager.Instance.AddGems(1); for testing
        MainMenuButtonManager.Instance.hideGO(MainMenuButtonManager.Instance.selectlevel_panel);
        MainMenuButtonManager.Instance.showGO(MainMenuButtonManager.Instance.Main_menu);

    }

    public void Lv1_btn_Clicked()
    {
        SceneManager.LoadScene("Lv-1");
    }
    //public void Lv2_btn_Clicked()
    //{
    //    SceneManager.LoadScene("Lv-2");
    //}
    //public void Lv3_btn_Clicked()
    //{
    //    SceneManager.LoadScene("Lv-3");
    //}
    //public void Lv4_btn_Clicked()
    //{
    //    SceneManager.LoadScene("Lv-4");
    //}
    //public void Lv5_btn_Clicked()
    //{
    //    SceneManager.LoadScene("Lv-5");
    //}
    //public void Lv6_btn_Clicked()
    //{
    //    SceneManager.LoadScene("Lv-6");
    //}
    //public void Lv7_btn_Clicked()
    //{
    //    SceneManager.LoadScene("Lv-7");
    //}
    //public void Lv8_btn_Clicked()
    //{
    //    SceneManager.LoadScene("Lv-8");
    //}
    //public void Lv9_btn_Clicked()
    //{
    //    SceneManager.LoadScene("Lv-9");
    //}
    //public void Lv10_btn_Clicked()
    //{
    //    SceneManager.LoadScene("Lv-10");
    //}



}
