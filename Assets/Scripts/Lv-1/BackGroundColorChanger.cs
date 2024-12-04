using UnityEngine;

public class BackGroundColorChanger : MonoBehaviour
{
    [Header("Backgrounds")]
    public SpriteRenderer[] backgrounds; // Array of all background SpriteRenderers

    [Header("Color Settings")]
    public Color[] colors; // Array of custom colors to cycle through
    public float transitionDuration = 2f; // Time to transition between colors

    private int currentColorIndex = 0; // Index of the current color
    private int nextColorIndex = 1; // Index of the next color
    private float transitionTimer = 0f; // Timer for color transitions

    void Start()
    {
        if (colors.Length < 2)
        {
            Debug.LogError("You need at least two colors for the color changer to work.");
            return;
        }
    }

    void Update()
    {
        if (backgrounds == null || backgrounds.Length == 0 || colors.Length < 2) return;

        // Update the transition timer
        transitionTimer += Time.deltaTime;

        // Calculate the interpolation factor (0 to 1)
        float t = transitionTimer / transitionDuration;

        // Interpolate between the current and next color
        Color currentColor = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], t);

        // Apply the interpolated color to all backgrounds
        foreach (SpriteRenderer background in backgrounds)
        {
            background.color = currentColor;
        }

        // Check if the transition is complete
        if (t >= 1f)
        {
            // Reset the timer
            transitionTimer = 0f;

            // Update the color indices
            currentColorIndex = nextColorIndex;
            nextColorIndex = (nextColorIndex + 1) % colors.Length; // Loop back to the start
        }
    }
}
