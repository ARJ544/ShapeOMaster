using UnityEngine;
using UnityEngine.UI;

public class SoundManagerForLV : MonoBehaviour
{
    public Image whenMute_or_crossline_lv;
    private bool isMuted_lv = false;
    private const string MuteKey_lv = "isMuted_lv"; 

    public AudioSource backgroundMusic;
    public AudioSource enemyAttackSound;
    public AudioSource playerShootSound;
    public AudioSource winSound;
    public AudioSource loseSound;

    // Start is called before the first frame update
    void Start()
    {
        // Initially hide the mute icon
        whenMute_or_crossline_lv.enabled = false;

        // Load the mute preference from PlayerPrefs
        isMuted_lv = PlayerPrefs.GetInt(MuteKey_lv, 0) == 1;

        // Set the audio listener volume based on the loaded preference
        AudioListener.volume = isMuted_lv ? 0f : 1f;

        // Update the mute icon based on the mute state
        whenMute_or_crossline_lv.enabled = isMuted_lv;

        // Log the initial state
        Debug.Log(isMuted_lv ? "Sound is muted." : "Sound is unmuted.");

        // Start playing background music if set and not muted
        if (backgroundMusic != null && !isMuted_lv)
        {
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }

    // Method to mute/unmute all sounds in the game
    public void ToggleMute_lv()
    {
        isMuted_lv = !isMuted_lv;

        // Mute or unmute based on the 'isMuted_lv' flag
        AudioListener.volume = isMuted_lv ? 0f : 1f;

        // Update the mute icon based on the mute state
        whenMute_or_crossline_lv.enabled = isMuted_lv;

        // Log the state change
        Debug.Log(isMuted_lv ? "Sound is now muted." : "Sound is now unmuted.");

        // Save the preference in PlayerPrefs
        PlayerPrefs.SetInt(MuteKey_lv, isMuted_lv ? 1 : 0);

        // Save PlayerPrefs to make sure the change is persisted
        PlayerPrefs.Save();

        // Stop or play background music based on mute state
        if (backgroundMusic != null)
        {
            if (isMuted_lv)
                backgroundMusic.Pause();
            else
                backgroundMusic.Play();
        }
    }

    public void PlayEnemyAttackSound()
    {
        if (enemyAttackSound != null && !isMuted_lv)
            enemyAttackSound.Play();
    }

    public void PlayPlayerShootSound()
    {
        if (playerShootSound != null && !isMuted_lv)
        {
            Debug.Log("ShootisPlaying");
            playerShootSound.Play();
        }
            
        else
        {
            Debug.LogWarning("Player Shoot Sound is not assigned.");
        }
    }

    public void PlayWinSound()
    {
        if (winSound != null && !isMuted_lv)
        {
            backgroundMusic.Stop();
            winSound.Play();
            Debug.Log("winisPlaying");
        }
            
        else
        {
            Debug.LogWarning("Win Sound is not assigned.");
        }
    }

    public void PlayLoseSound()
    {
        if (loseSound != null && !isMuted_lv)
        {
            backgroundMusic.Stop();
            loseSound.Play();
            Debug.Log("LoseSoudisPlaying");
            
        }
            
        else
        {
            Debug.LogWarning("Lose Sound is not assigned.");
        }
    }
}
