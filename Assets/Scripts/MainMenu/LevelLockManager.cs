using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLockManager : MonoBehaviour
{
    public static LevelLockManager Instance; // Singleton instance

    public Button[] levelButtons;
    public Sprite lockedSprite;
    public Sprite unlockedSprite;

    private int maxLevelUnlocked;

    void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this; // Set the singleton instance
        //    DontDestroyOnLoad(gameObject); // Persist across scenes if needed
        //}
        //else
        //{
        //    Destroy(gameObject); // Ensure only one instance exists
        //}

        int unlockedLevelUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
            levelButtons[i].GetComponent<Image>().sprite = lockedSprite;
        }
        for (int i = 0; i < unlockedLevelUnlocked; i++)
        {
            levelButtons[i].interactable = true;
            levelButtons[i].GetComponent<Image>().sprite = unlockedSprite;
        }
    }

    //void Start()
    //{
    //    //PlayerPrefs.DeleteAll(); // For testing purposes
    //    maxLevelUnlocked = PlayerPrefs.GetInt("MaxLevelUnlocked", 1);
    //    UpdateLevelButtons();
    //}

    //public void UnlockNextLevel(int currentLevel)
    //{
    //    if (currentLevel >= maxLevelUnlocked)
    //    {
    //        maxLevelUnlocked = currentLevel + 1;
    //        PlayerPrefs.SetInt("MaxLevelUnlocked", maxLevelUnlocked);
    //        PlayerPrefs.Save();
    //        UpdateLevelButtons();
    //    }
    //}

    //private void UpdateLevelButtons()
    //{
    //    for (int i = 0; i < levelButtons.Length; i++)
    //    {
    //        if (i + 1 <= maxLevelUnlocked)
    //        {
    //            levelButtons[i].interactable = true;
    //            levelButtons[i].GetComponent<Image>().sprite = unlockedSprite;
    //        }
    //        else
    //        {
    //            levelButtons[i].interactable = false;
    //            levelButtons[i].GetComponent<Image>().sprite = lockedSprite;
    //        }
    //    }
    //}

    public void LoadLevel(int levelIndex)
    {
        string levelName = "Lv-" + levelIndex;
        SceneManager.LoadScene(levelName);
    }
}
