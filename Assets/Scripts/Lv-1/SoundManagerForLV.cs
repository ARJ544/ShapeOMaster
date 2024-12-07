using UnityEngine;
using UnityEngine.UI;  // Make sure you include this for the Image component

public class SoundManagerForLV : MonoBehaviour
{
    public Image whenMute_or_crossline_lv;  // Reference to the Image that will show mute/crossline icon
    private bool isMuted_lv = false;  // Renamed isMuted to isMuted_lv
    private const string MuteKey_lv = "isMuted_lv"; // Renamed MuteKey to MuteKey_lv

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
    }
}
