using UnityEngine;
using UnityEngine.UI;

public class HealthBarTriangle : MonoBehaviour
{
    public Slider greenFill;
    public Gradient gradient;
    public Image fill;

    public void SetHealth(int health)
    {
        greenFill.value = health;
        fill.color = gradient.Evaluate(greenFill.normalizedValue);
    }
    public void SetMaxHealth(int health)
    {
        greenFill.maxValue = health;  // Set maximum slider value
        greenFill.value = health;     // Set slider to full value

        fill.color = gradient.Evaluate(1f);
    }

}
