using UnityEngine;

public class WeaponMenu : MonoBehaviour
{
    public WeaponButton[] weaponButtons;

    void Start()
    {
        foreach (WeaponButton button in weaponButtons)
        {
            button.weaponMenu = this;
        }
    }

    public void DeselectAll()
    {
        foreach (WeaponButton button in weaponButtons)
        {
            button.Deselect();  // Deselect all buttons
        }
    }
}
