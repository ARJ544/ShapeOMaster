using UnityEngine;
using UnityEngine.UI;  // Make sure you include this for the Image component

public class SoundManager_Main_Menu : MonoBehaviour
{
    public Image whenMute_or_crossline_mainmenu;  // Reference to the Image that will show mute/crossline icon
    private bool isMuted_mainmenu = false;  // Renamed isMuted to isMuted_mainmenu
    private const string MuteKey_mainmenu = "isMuted_mainmenu"; // Renamed MuteKey to MuteKey_mainmenu

    // Start is called before the first frame update
    void Start()
    {
        // Initially hide the mute icon
        whenMute_or_crossline_mainmenu.enabled = false;

        // Load the mute preference from PlayerPrefs
        isMuted_mainmenu = PlayerPrefs.GetInt(MuteKey_mainmenu, 0) == 1;

        // Set the audio listener volume based on the loaded preference
        AudioListener.volume = isMuted_mainmenu ? 0f : 1f;

        // Update the mute icon based on the mute state
        whenMute_or_crossline_mainmenu.enabled = isMuted_mainmenu;

        // Log the initial state
        Debug.Log(isMuted_mainmenu ? "Sound is muted." : "Sound is unmuted.");
    }

    // Method to mute/unmute all sounds in the game
    public void ToggleMute_mainmenu()
    {
        isMuted_mainmenu = !isMuted_mainmenu;

        // Mute or unmute based on the 'isMuted' flag
        AudioListener.volume = isMuted_mainmenu ? 0f : 1f;

        // Update the mute icon based on the mute state
        whenMute_or_crossline_mainmenu.enabled = isMuted_mainmenu;

        // Log the state change
        Debug.Log(isMuted_mainmenu ? "Sound is now muted." : "Sound is now unmuted.");

        // Save the preference in PlayerPrefs
        PlayerPrefs.SetInt(MuteKey_mainmenu, isMuted_mainmenu ? 1 : 0);

        // Save PlayerPrefs to make sure the change is persisted
        PlayerPrefs.Save();
    }
}
