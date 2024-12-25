using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance; // Singleton instance
    public GameObject[] guns;  // List of all available gun prefabs
    private GameObject currentGun; // Currently equipped gun
    private int currentGunIndex = -1; // No gun equipped initially

    public Transform gunHolder; // Parent object to hold equipped guns

    private void Awake()
    {
        // Ensure only one instance of WeaponManager exists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // If another WeaponManager exists, destroy this one
        }
    }

    public void EquipGun(int gunIndex)
    {
        if (gunIndex < 0 || gunIndex >= guns.Length)
        {
            Debug.LogError("Invalid gun index.");
            return;
        }

        // Unequip current gun
        UnequipCurrentGun();

        // Equip the selected gun
        currentGunIndex = gunIndex;

        // Instantiate gun without setting it under the gunHolder initially
        GameObject tempGun = Instantiate(guns[gunIndex]);

        // Now set it under the gunHolder
        tempGun.transform.SetParent(gunHolder);

        // Optionally, reset position and rotation if needed to align the gun correctly
        tempGun.transform.localPosition = Vector3.zero; // Adjust if needed
        tempGun.transform.localRotation = Quaternion.identity; // Adjust if needed

        // Now the gun is part of the gunHolder
        currentGun = tempGun; // Update the reference to the current gun
        currentGun.SetActive(true); // Make the gun active
    }

    public void UnequipCurrentGun()
    {
        if (currentGun != null)
        {
            Destroy(currentGun); // Destroy the current gun
            currentGun = null; // Reset the current gun reference
            currentGunIndex = -1; // No gun equipped anymore
        }
    }
}
