using UnityEngine;

public class GunListManager : MonoBehaviour
{
    public static GunListManager Instance;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GunListManager Instance created.");

        }
        else
        {
            Debug.LogWarning("Multiple GunListManager instances detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }


    

    

}
