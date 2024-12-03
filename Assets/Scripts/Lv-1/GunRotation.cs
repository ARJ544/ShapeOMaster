using UnityEngine;

public class GunRotation : MonoBehaviour
{
    [Header("Gun Rotation Settings")]
    public Transform gunTransform;  // Reference to the gun's transform
    public VirtualJoystick joystick;  // Reference to the VirtualJoystick script

    [Header("Rotation Range")]
    public float minRotation = -90f;  // Minimum rotation (in degrees)
    public float maxRotation = 90f;   // Maximum rotation (in degrees)

    void Update()
    {
        // Get joystick input direction (horizontal movement only)
        float joystickInputX = joystick.inputDirection.x;

        if (Mathf.Abs(joystickInputX) > 0.1f)  // Only rotate if there's significant joystick movement
        {
            // Calculate the angle based on joystick input (horizontal only)
            float angle = joystickInputX * maxRotation;  // Scale the joystick input with maxRotation

            // Clamp the angle between the defined min and max range
            angle = Mathf.Clamp(angle, minRotation, maxRotation);

            // Apply the rotation to the gun (Z axis rotation)
            gunTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
