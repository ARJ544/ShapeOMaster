using UnityEngine;

public class SettingsPanel_btn_manager : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseSettings_Panel()
    {
        //DataManager.Instance.SpendCoins(10); for testing
        //DataManager.Instance.SpendGems(1); for testing
        MainMenuButtonManager.Instance.hideGO(MainMenuButtonManager.Instance.settings_panel);
        MainMenuButtonManager.Instance.showGO(MainMenuButtonManager.Instance.Main_menu);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
