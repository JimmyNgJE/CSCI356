using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectController : MonoBehaviour
{
    [SerializeField] AudioSource UISound;
    public void BacktoMainMenu()
    {
        if (UISound != null) { UISound.Play(); } // play UI sound
        // Load the MainMenuScene
        SceneManager.LoadScene("MainMenuScene");
    }

    public void NewGame()
    {
        if (UISound != null) { UISound.Play(); } // play UI sound
        // Load the CharacterSelectScene
        SceneManager.LoadScene("Map Scene");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
