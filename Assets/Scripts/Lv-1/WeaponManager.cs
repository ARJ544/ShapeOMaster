//using UnityEngine;

//public class WeaponManager : MonoBehaviour
//{
//    public static WeaponManager Instance;

//    public GameObject triangle; // Reference to the triangle

//    private int activeWeaponIndex = 0; // Default weapon index

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject); // Prevent destruction on scene change
//            Debug.Log("WeaponManager marked as persistent.");
//        }
//        else
//        {
//            Debug.LogWarning("Multiple WeaponManager instances detected. Destroying duplicate.");
//            Destroy(gameObject);
//        }
//    }

//    void Start()
//    {
//        // Load the active weapon index from PlayerPrefs
//        activeWeaponIndex = PlayerPrefs.GetInt("ActiveWeaponIndex", 0);
//        ActivateWeapon(activeWeaponIndex);
//    }

//    // Activates the selected weapon for the triangle by child name
//    public void ActivateWeapon(int weaponIndex)
//    {
//        if (triangle == null)
//        {
//            Debug.LogError("Triangle reference is missing!");
//            return;
//        }

//        // Iterate through all child objects of the triangle
//        for (int i = 0; i < triangle.transform.childCount; i++)
//        {
//            GameObject child = triangle.transform.GetChild(i).gameObject;

//            // Enable the child if its name matches "Gun (X)" (e.g., "Gun (0)", "Gun (1)")
//            if (child.name == $"Gun ({weaponIndex})")
//            {
//                child.SetActive(true);
//                activeWeaponIndex = weaponIndex;

//                // Save the selected weapon
//                PlayerPrefs.SetInt("ActiveWeaponIndex", weaponIndex);
//                PlayerPrefs.Save();
//            }
//            else
//            {
//                // Disable all other children
//                child.SetActive(false);
//            }
//        }
//    }
//}
