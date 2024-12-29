using UnityEngine;

public class WeaponMenu : MonoBehaviour
{
    public WeaponButton[] weaponButtons;

    void Start()
    {
        foreach (WeaponButton button in weaponButtons)
        {
            if (button != null) // Ensure the button exists
            {
                button.weaponMenu = this;
            }
            else
            {
                Debug.LogError("WeaponButton reference is missing in WeaponMenu.");
            }
        }
    }


    public void DeselectAll()
    {
        foreach (WeaponButton button in weaponButtons)
        {
            button.Deselect();
        }
    }
}
