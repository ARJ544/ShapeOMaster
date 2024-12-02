using UnityEngine;

public class ShapePanel_btn_manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseShape_Panel()
    {
        MainMenuButtonManager.Instance.hideGO(MainMenuButtonManager.Instance.shape_panel);
        MainMenuButtonManager.Instance.showGO(MainMenuButtonManager.Instance.Main_menu);
    }
}
