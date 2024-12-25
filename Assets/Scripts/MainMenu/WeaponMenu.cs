using UnityEngine;

public class WeaponMenu : MonoBehaviour
{
    public WeaponButton[] weaponButtons;  // Array of all weapon buttons
    public WeaponManager weaponManager;    // Reference to the WeaponManager

    void Start()
    {
        // Make sure that each weapon button has a reference to the WeaponMenu
        foreach (WeaponButton button in weaponButtons)
        {
            button.weaponMenu = this;
        }

        // Ensure the WeaponManager is set (either in the Inspector or dynamically found)
        if (weaponManager == null)
        {
            weaponManager = FindObjectOfType<WeaponManager>(); // Dynamically find the WeaponManager in the scene
        }

        // Check if the WeaponManager is found
        if (weaponManager != null)
        {
            // Equip the first gun by default if WeaponManager is found
            weaponManager.EquipGun(0);  // Assuming that the first gun is the default
        }
        else
        {
            Debug.LogWarning("WeaponManager not found in the scene. Please ensure it exists.");
        }
    }

    // Deselect all weapons (called from WeaponButton when another weapon is selected)
    public void DeselectAll()
    {
        foreach (WeaponButton button in weaponButtons)
        {
            button.Deselect();  // Deselect all weapon buttons
        }
    }
}
