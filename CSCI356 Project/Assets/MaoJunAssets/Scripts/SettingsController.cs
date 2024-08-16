using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [SerializeField] Image settingsPopup;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider mousesenSlider;
    // Start is called before the first frame update
    void Start()
    {
        // don't display the popup on start
        settingsPopup.gameObject.SetActive(false);
    }

    public void Settings()
    {
        // display the settings popup
        settingsPopup.gameObject.SetActive(true);
    }
    public void closeSettings()
    {
        // don't display the settings popup
        settingsPopup.gameObject.SetActive(false);
    }
}
