using UnityEngine;

public class WeaponsPanel_btn_manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Closeweapons_Panel()
    {
        MainMenuButtonManager.Instance.hideGO(MainMenuButtonManager.Instance.weapon_panel);
        MainMenuButtonManager.Instance.showGO(MainMenuButtonManager.Instance.Main_menu);
    }
}
