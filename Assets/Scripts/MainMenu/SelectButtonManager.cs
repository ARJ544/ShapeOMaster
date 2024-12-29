using UnityEngine;
using UnityEngine.UI;

public class SelectButtonManager : MonoBehaviour
{
    public static SelectButtonManager Instance;  // Static reference for global access

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("DataManager Instance created.");
        }
        else
        {
            Debug.LogWarning("Multiple SelectBTNMananger instances detected. Destroying duplicate.");
            Destroy(gameObject);
        }

    }

    public Button selectBtn0;
    public Button selectBtn1;
    public Button selectBtn2;
    public Button selectBtn3;
    public Button selectBtn4;
    public Button selectBtn5;
    public Button selectBtn6;
    public Button selectBtn7;
    public Button selectBtn8;
    public Button selectBtn9;

}
