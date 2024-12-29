using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public Button buyButton;
    public Button selectButton;
    public TextMeshProUGUI buttonText;
    public int weaponCost;
    public bool isDefaultWeapon = false;  // Flag to mark the default weapon
    private bool isPurchased = false;
    private bool isSelected = false;

    public WeaponMenu weaponMenu;

    private string weaponKey;  // Unique key for saving the weapon's state
    //private bool isFirstLoad = true;  // Flag to check if this is the first load (start of the game)

    void Start()
    {
        //PlayerPrefs.DeleteAll();

        weaponKey = "Weapon_" + gameObject.name;  // Unique key based on the weapon's name
        LoadWeaponState();  // Load the saved state of the weapon

        // Don't auto-select the default weapon anymore
        if (isPurchased)
        {
            buttonText.text = isSelected ? "SELECTED" : "SELECT";
            UpdateButtonState(isSelected);
        }
        else
        {
            UpdateButtonState();
        }

        buyButton?.onClick.AddListener(OnBuyButtonClicked);
        selectButton.onClick.AddListener(OnSelectButtonClicked);
    }

    void OnBuyButtonClicked()
    {
        if (!isPurchased && DataManager.Instance.SpendCoins(weaponCost))
        {
            isPurchased = true;
            buttonText.text = "SELECT";
            UpdateButtonState();
            SaveWeaponState();  // Save the state after purchasing
        }
    }

    void OnSelectButtonClicked()
    {
        if (isPurchased)
        {
            // Deselect all weapons before selecting the current one
            weaponMenu.DeselectAll();
            isSelected = true;  // Mark this weapon as selected
            buttonText.text = "SELECTED";
            UpdateButtonState(true);
            SaveWeaponState();  // Save the selection state

        }
    }

    public void Deselect()
    {
        if (isPurchased)
        {
            isSelected = false;  // Mark this weapon as deselected
            buttonText.text = "SELECT";
            UpdateButtonState(false);
            SaveWeaponState();  // Save the deselection state
        }
    }

    void UpdateButtonState(bool isSelected = false)
    {
        if (isDefaultWeapon)
        {
            buyButton?.gameObject.SetActive(false);  // Default weapon doesn't show the "buy" button
        }
        else
        {
            buyButton?.gameObject.SetActive(!isPurchased);  // Show buy button if not purchased
        }

        selectButton.gameObject.SetActive(isPurchased);  // Show select button if purchased

        if (isPurchased)
        {
            selectButton.interactable = !isSelected;  // Disable select button if already selected
        }
    }

    // Save weapon purchase and selection state
    void SaveWeaponState()
    {
        PlayerPrefs.SetInt(weaponKey + "_Purchased", isPurchased ? 1 : 0);
        PlayerPrefs.SetInt(weaponKey + "_Selected", isSelected ? 1 : 0);
        PlayerPrefs.Save();  // Save the changes
    }

    // Load weapon purchase and selection state
    void LoadWeaponState()
    {
        isPurchased = PlayerPrefs.GetInt(weaponKey + "_Purchased", 0) == 1;
        isSelected = PlayerPrefs.GetInt(weaponKey + "_Selected", 0) == 1;

        // If it's the default weapon, force it to be selected when loading (Only one selected weapon allowed)
        if (isDefaultWeapon && isSelected)
        {
            isSelected = true;
            buttonText.text = "SELECTED";
        }
        else if (isSelected)
        {
            // Ensure the last selected weapon is selected
            buttonText.text = "SELECTED";
        }
    }
}