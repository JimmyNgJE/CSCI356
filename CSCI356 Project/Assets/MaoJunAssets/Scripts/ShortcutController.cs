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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsController != null)
            {
                // Pause the game
                Time.timeScale = 0f;
                settingsController.openSettings();
                // Enable the cursor and unlock it
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        // Check if settings menu is active
        if (settingsPopup.activeSelf){
            if((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)))
            {
                resumeGame();
                settingsController.closeSettings();
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