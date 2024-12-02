using UnityEngine;

public class ShopPanel_btn_manager : MonoBehaviour
{
    public void coin50_btn_Clicked()
    {
        if (DataManager.Instance.GetGems() >= 1) // Check if enough gems
        {
            DataManager.Instance.AddCoins(50);
            DataManager.Instance.SpendGems(1);
            //DataManager.Instance.SaveData();
        }
        else
        {
            Debug.LogWarning("Not enough gems to buy 50 coins!");
        }
    }

    public void coin100_btn_Clicked()
    {
        if (DataManager.Instance.GetGems() >= 2) // Check if enough gems
        {
            DataManager.Instance.AddCoins(100);
            DataManager.Instance.SpendGems(2);
            //DataManager.Instance.SaveData();
        }
        else
        {
            Debug.LogWarning("Not enough gems to buy 100 coins!");
        }
    }

    public void coin150_btn_Clicked()
    {
        if (DataManager.Instance.GetGems() >= 3) // Check if enough gems
        {
            DataManager.Instance.AddCoins(150);
            DataManager.Instance.SpendGems(3);
            //DataManager.Instance.SaveData();
        }
        else
        {
            Debug.LogWarning("Not enough gems to buy 150 coins!");
        }
    }

    public void gems1_btn_clicked()
    {
        if (DataManager.Instance.GetCoins() >= 200) // Check if enough coins
        {
            DataManager.Instance.AddGems(1);
            DataManager.Instance.SpendCoins(200);
            //DataManager.Instance.SaveData();
        }
        else
        {
            Debug.LogWarning("Not enough coins to buy 1 gem!");
        }
    }

    public void gems2_btn_clicked()
    {
        if (DataManager.Instance.GetCoins() >= 400) // Check if enough coins
        {
            DataManager.Instance.AddGems(2);
            DataManager.Instance.SpendCoins(400);
            //DataManager.Instance.SaveData();
        }
        else
        {
            Debug.LogWarning("Not enough coins to buy 2 gems!");
        }
    }

    public void gems3_btn_clicked()
    {
        if (DataManager.Instance.GetCoins() >= 600) // Check if enough coins
        {
            DataManager.Instance.AddGems(3);
            DataManager.Instance.SpendCoins(600);
            //DataManager.Instance.SaveData();
        }
        else
        {
            Debug.LogWarning("Not enough coins to buy 3 gems!");
        }
    }

    public void Closeshop_Panel()
    {
        MainMenuButtonManager.Instance.hideGO(MainMenuButtonManager.Instance.shop_panel);
        MainMenuButtonManager.Instance.showGO(MainMenuButtonManager.Instance.Main_menu);
    }
}
