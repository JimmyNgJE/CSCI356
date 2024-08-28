using UnityEngine;
using UnityEngine.SceneManagement;

public class ShortcutController : MonoBehaviour
{
    public SettingsController settingsController;
    public GameObject settingsPopup; // Reference to the settings popup GameObject
    void Start()
    {
        // Ensure the settingsController is assigned in the inspector or find it in the scene
        if (settingsController == null)
        {
            settingsController = FindObjectOfType<SettingsController>();
        }
        resumeGame();
    }

    void Update()
    {
        // Check if the user presses P or Escape
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            // If the settings menu is active, close it and resume the game
            if (settingsPopup.activeSelf)
            {
                resumeGame();
                settingsController.closeSettings();
            }
            // If the settings menu is not active, open it and pause the game
            else
            {
                if (settingsController != null)
                {
                    Time.timeScale = 0f;
                    settingsController.openSettings();
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
    }
    public void resumeGame()
    {
        // Check if the current scene is the "Map" scene
        if (SceneManager.GetActiveScene().name == "Map Scene")
        {
            // Hide the cursor and lock it to the center of the screen
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            // Resume the game
            Time.timeScale = 1f;
        }
    }
}