using UnityEngine;
using UnityEngine.UI;

public class HealthBarSquare : MonoBehaviour
{
    public Slider greenFill;

    public void SetHealth(int health)
    {
        greenFill.value = health;
    }
    public void SetMaxHealth(int health)
    {
        greenFill.maxValue = health;  // Set maximum slider value
        greenFill.value = health;     // Set slider to full value
    }

}
