using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour
{
    public static MainMenuButtonManager Instance;

    [Header(" Menu Elements ")]
    [SerializeField] private TextMeshProUGUI coins;//on main menu
    [SerializeField] private TextMeshProUGUI coins2;//on shop
    [SerializeField] private TextMeshProUGUI gems;
    [SerializeField] private TextMeshProUGUI gems2;

    public GameObject Main_menu;
    public GameObject selectlevel_panel;
    public GameObject weapon_panel;
    public GameObject shape_panel;
    public GameObject shop_panel;
    public GameObject settings_panel;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    public void UpdateCoinsUI(int newCoinsValue)
    {
        if (coins != null && coins2 != null)
        {
            coins.text = newCoinsValue.ToString();
            coins2.text = newCoinsValue.ToString();
        }
        else
        {
            Debug.LogWarning("Coins TextMeshProUGUI reference is missing!");
        }
    }

    public void UpdateGemsUI(int newGemsValue)
    {
        if (gems != null && gems2 != null)
        {
            gems.text = newGemsValue.ToString();
            gems2.text = newGemsValue.ToString();
            Debug.Log($"Gems UI updated to: {newGemsValue}"); // Debug log
        }
        else
        {
            Debug.LogWarning("Gems TextMeshProUGUI reference is missing!");
        }
    }




    void Start()
    {
        //DataManager.Instance.LoadData();
        if (coins != null && gems != null && coins2 != null && gems2 != null)
        {
            coins.text = DataManager.Instance.GetCoins().ToString();
            coins2.text = DataManager.Instance.GetCoins().ToString();
            gems.text = DataManager.Instance.GetGems().ToString();
            gems2.text = DataManager.Instance.GetGems().ToString();
        }
        else
        {
            Debug.LogWarning("UI Text references (coins or gems) are missing!");
        }

        showGO(Main_menu);
        hideGO(selectlevel_panel);
        hideGO(weapon_panel);
        hideGO(shape_panel);
        hideGO(shop_panel);
        hideGO(settings_panel);
    }

    void Update()
    {
        
    }

    public void play_btn_clicked()
    {
        showGO(selectlevel_panel);
    }
    public void weapons_btn_clicked()
    {
        showGO(weapon_panel);
    }
    public void shape_btn_clicked()
    {
        showGO(shape_panel);
    }
    public void shop_btn_clicked()
    {
        showGO(shop_panel);
    }
    public void settings_btn_clicked()
    {
        showGO(settings_panel);
    }


    public void hideGO(GameObject go)
    {
        go.SetActive(false);
    }

    public void showGO(GameObject go)
    {
        go.SetActive(true);
    }


}
