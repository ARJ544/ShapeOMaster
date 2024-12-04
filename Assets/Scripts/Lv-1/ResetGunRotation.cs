using UnityEngine;
using UnityEngine.UI;

public class ResetGunRotation : MonoBehaviour
{
    [Header("References")]
    public Transform gunTransform;    // Reference to the gun's transform
    public Button resetButton;        // Reference to the reset button

    private void Start()
    {
        // Ensure the resetButton is assigned
        if (resetButton != null)
        {
            resetButton.onClick.RemoveAllListeners(); // Clear any previous listeners
            resetButton.onClick.AddListener(ResetRotation); // Add the ResetRotation method as a listener
        }
        else
        {
            Debug.LogError("Reset button is not assigned.");
        }
    }

    // Method to reset the gun's rotation
    public void ResetRotation()
    {
        if (gunTransform != null)
        {
            gunTransform.rotation = Quaternion.Euler(0, 0, 0); // Reset z-rotation to 0
        }
        else
        {
            Debug.LogError("Gun Transform is not assigned.");
        }
    }
}
