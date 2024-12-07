using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance; /*{ get; private set; }*/ // Singleton instance

    [Header(" Data ")]
    private int coins = 100;
    private int gems = 2;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("DataManager Instance created.");
        }
        else
        {
            Debug.LogWarning("Multiple DataManager instances detected. Destroying duplicate.");
            Destroy(gameObject);
        }
        LoadData();
    }


    public void LoadData()
    {
        // Check if PlayerPrefs keys exist; if not, set default values
        if (!PlayerPrefs.HasKey("coins"))
        {
            coins = 100; // Default value for coins
            PlayerPrefs.SetInt("coins", coins); // Save the default value
            Debug.Log("Coins key not found. Setting default value: 50");
        }
        else
        {
            coins = PlayerPrefs.GetInt("coins"); // Load saved value
            Debug.Log($"Loaded coins from PlayerPrefs: {coins}");
        }

        if (!PlayerPrefs.HasKey("gems"))
        {
            gems = 2; // Default value for gems
            PlayerPrefs.SetInt("gems", gems); // Save the default value
            Debug.Log("Gems key not found. Setting default value: 2");
        }
        else
        {
            gems = PlayerPrefs.GetInt("gems"); // Load saved value
            Debug.Log($"Loaded gems from PlayerPrefs: {gems}");
        }

        // Ensure PlayerPrefs is saved after initialization
        PlayerPrefs.Save();
    }


    public void SaveData()
    {
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.SetInt("gems", gems);
        PlayerPrefs.Save(); // Ensures data is saved immediately
    }

    public int GetCoins()
    {
        return coins;
    }

    public int GetGems()
    {
        return gems;
    }

    public void AddCoins(int amount)
    {
        coins += amount; // Increase coins
        SaveData();      // Save updated value

        // Update UI
        MainMenuButtonManager.Instance.UpdateCoinsUI(coins);
    }

    public void SpendCoins(int amount)
    {
        if (coins < amount)
        {
            Debug.LogWarning("Not enough coins to spend!");
            return; // Exit early if there aren't enough coins
        }

        coins -= amount; // Decrease coins
        SaveData();      // Save updated value

        // Update UI
        MainMenuButtonManager.Instance.UpdateCoinsUI(coins);
    }



    public void AddGems(int amount)
    {
        gems += amount; // Increase gems
        Debug.Log($"Gems increased by {amount}. Current gems: {gems}"); // Debug log
        SaveData();     // Save updated value

        // Update UI
        MainMenuButtonManager.Instance.UpdateGemsUI(gems);
    }

    public void SpendGems(int amount)
    {
        if (gems < amount)
        {
            Debug.LogWarning($"Not enough gems to spend! Current gems: {gems}, tried to spend: {amount}");
            return; // Exit early if there aren't enough gems
        }

        gems -= amount; // Decrease gems
        SaveData();     // Save updated value

        // Update UI
        MainMenuButtonManager.Instance.UpdateGemsUI(gems);
    }
}
