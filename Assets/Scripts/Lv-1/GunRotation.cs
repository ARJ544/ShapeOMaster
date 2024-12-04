using UnityEngine;

public class GunRotation : MonoBehaviour
{
    [Header("Gun Rotation Settings")]
    public Transform gunTransform;       // Reference to the gun's transform
    public VirtualJoystick joystick;     // Reference to the VirtualJoystick script
    public float rotationSpeed = 200f;   // Speed of rotation (not used here, can smoothen rotation if needed)

    [Header("Fixed Rotation Angles")]
    public float minRotation = -90f;     // Minimum rotation angle
    public float maxRotation = 90f;      // Maximum rotation angle
    public float angleStep = 5f;         // Step size for rotation angles

    private float[] fixedAngles;         // Array of fixed angles for snapping

    private void Start()
    {
        // Generate the fixed angles array dynamically based on the range and step
        int steps = Mathf.RoundToInt((maxRotation - minRotation) / angleStep) + 1;
        fixedAngles = new float[steps];
        for (int i = 0; i < steps; i++)
        {
            fixedAngles[i] = minRotation + i * angleStep;
        }
    }

    void Update()
    {
        // Get joystick input direction
        Vector2 joystickInput = joystick.inputDirection;

        if (joystickInput.magnitude > 0.1f) // Only rotate if there's significant joystick movement
        {
            // Calculate the angle based on joystick input
            float angle = Mathf.Atan2(joystickInput.y, joystickInput.x) * Mathf.Rad2Deg;

            // Snap to the closest valid angle
            float snappedAngle = GetClosestAngle(angle);

            // Apply the snapped angle as gun rotation
            gunTransform.rotation = Quaternion.Euler(0, 0, snappedAngle);
        }
    }

    // Function to get the closest valid angle from the fixedAngles array
    float GetClosestAngle(float angle)
    {
        float closestAngle = fixedAngles[0];
        float minDifference = Mathf.Abs(Mathf.DeltaAngle(angle, closestAngle));

        foreach (float fixedAngle in fixedAngles)
        {
            float difference = Mathf.Abs(Mathf.DeltaAngle(angle, fixedAngle));
            if (difference < minDifference)
            {
                minDifference = difference;
                closestAngle = fixedAngle;
            }
        }

        return closestAngle;
    }
}
