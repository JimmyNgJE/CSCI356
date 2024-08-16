using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    public void NewGame()
    {
        // Load the CharacterSelectScene
        //SceneManager.LoadScene("CharacterSelectScene");
        SceneManager.LoadScene("Map Scene");
    }

    public void LoadGame()
    {
        // Implement load game logic here
        SceneManager.LoadScene("Map Scene");
    }

    public void HowToPlay()
    {
        // Implement how to play instructions (could be a separate scene or popup)
    }



    public void ExitGame()
    {
        // Exit the game or stop in unity editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void BacktoMainMenu()
    {
        // Load the MainMenuScene
        SceneManager.LoadScene("MainMenuScene");
    }
}
