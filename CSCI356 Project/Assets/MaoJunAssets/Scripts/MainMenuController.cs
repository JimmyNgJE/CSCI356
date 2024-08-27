using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour
{
    [SerializeField] Image HowToPlayPopup;
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource UISound;
    void Start()
    {
        music = GameObject.Find("Music").GetComponent<AudioSource>();
        UISound = GameObject.Find("UI Sound").GetComponent<AudioSource>();
        if (music != null){music.Play();}
        // don't display the popup on start
        if (HowToPlayPopup != null)
        { HowToPlayPopup.gameObject.SetActive(false); }
    }
    public void NewGame()
    {
        if (UISound != null) { UISound.Play(); } // play UI sound
            // Load the CharacterSelectScene
            SceneManager.LoadScene("CharacterSelectScene");
    }

    public void LoadGame()
    {
        if (UISound != null) { UISound.Play(); } // play UI sound
        // Implement load game logic here
        SceneManager.LoadScene("Map Scene");
    }

    public void openHowToPlay()
    {
        if (UISound != null) { UISound.Play(); } // play UI sound
        // display the how to play popup
        HowToPlayPopup.gameObject.SetActive(true);
    }
    public void closeHowToPlay()
    {
        if (UISound != null) { UISound.Play(); } // play UI sound
        // don't display the how to play popup
        HowToPlayPopup.gameObject.SetActive(false);
    }



    public void ExitGame()
    {
        if (UISound != null) { UISound.Play(); } // play UI sound
        // Exit the game or stop in unity editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void BacktoMainMenu()
    {
        if (UISound != null) { UISound.Play(); } // play UI sound
        // Load the MainMenuScene
        SceneManager.LoadScene("MainMenuScene");
    }
}
