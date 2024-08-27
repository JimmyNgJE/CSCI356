using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    [SerializeField] Image settingsPopup;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider mousesenSlider;
    [SerializeField] AudioSource UISound;
    [SerializeField] TMP_Text volumeTMPText;
    // Start is called before the first frame update
    void Start()
    {
        // don't display the popup on start
        settingsPopup.gameObject.SetActive(false);
        // Check if the active scene is the Main Menu
        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            UISound = GameObject.Find("UI Sound").GetComponent<AudioSource>();
        }
        volumeSlider.value = AudioListener.volume*10;
        // Add a listener to the slider to call OnVolumeSliderChange whenever the value changes
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeSliderChange(volumeSlider.value); });
    }

    public void openSettings()
    {
        if (UISound != null) { UISound.Play(); } // play UI sound
        // display the settings popup
        settingsPopup.gameObject.SetActive(true);
    }
    public void closeSettings()
    {
        if (UISound != null) { UISound.Play(); } // play UI sound
        // don't display the settings popup
        settingsPopup.gameObject.SetActive(false);
    }
    // when the volume slider changes
    public void OnVolumeSliderChange(float value)
    {
        // display the volume slider value
        AudioListener.volume = volumeSlider.value/10;
        if (volumeTMPText != null)
            volumeTMPText.text = (value).ToString("0");
    }
    public void BacktoMainMenu()
    {
        if (UISound != null) { UISound.Play(); } // play UI sound
        // Load the MainMenuScene
        SceneManager.LoadScene("MainMenuScene");
    }
}
