using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLockManager : MonoBehaviour
{
    public static LevelLockManager Instance; // Singleton instance

    public Button[] levelButtons;
    public Sprite lockedSprite;
    public Sprite unlockedSprite;

    void Awake()
    {
        int unlockedLevelUnlocked = Mathf.Clamp(PlayerPrefs.GetInt("UnlockedLevel", 1), 1, levelButtons.Length);

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
    void Start()
    {
        //PlayerPrefs.DeleteAll();
    }


    public void LoadLevel(int levelIndex)
    {
        string levelName = "Lv-" + levelIndex;
        SceneManager.LoadScene(levelName);
    }
}
