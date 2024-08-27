using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionController : MonoBehaviour
{
    public Button character1Button;
    public Button character2Button;
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    private Button selectedCharacter;
    private Button selectedDifficulty;

    private Color defaultColor; // To store the default color of the buttons

    void Start()
    {
        // Initialize default color from the first button
        if (character1Button != null)
        {
            defaultColor = character1Button.image.color;
        }

        // Set default selections
        SelectCharacter(character1Button);
        SelectDifficulty(easyButton);


        // Add listeners to buttons
        character1Button.onClick.AddListener(() => SelectCharacter(character1Button));
        character2Button.onClick.AddListener(() => SelectCharacter(character2Button));

        easyButton.onClick.AddListener(() => SelectDifficulty(easyButton));
        normalButton.onClick.AddListener(() => SelectDifficulty(normalButton));
        hardButton.onClick.AddListener(() => SelectDifficulty(hardButton));
    }

    void SelectCharacter(Button characterButton)
    {
        if (selectedCharacter != null)
        {
            selectedCharacter.image.color = defaultColor; // Reset the previous selection
        }

        selectedCharacter = characterButton;
        selectedCharacter.image.color = selectedCharacter.colors.pressedColor; // Use the button's selected color

        // Save selection
        GameSelect2.Instance.SelectedCharacter = selectedCharacter == character1Button ? "Character1" : "Character2";
    }

    void SelectDifficulty(Button difficultyButton)
    {
        if (selectedDifficulty != null)
        {
            selectedDifficulty.image.color = defaultColor; // Reset the previous selection
        }

        selectedDifficulty = difficultyButton;
        selectedDifficulty.image.color = selectedDifficulty.colors.pressedColor; // Use the button's selected color

        // Save selection
        GameSelect2.Instance.SelectedDifficulty = difficultyButton == easyButton ? "Easy" : (difficultyButton == normalButton ? "Normal" : "Hard");
    }

    public void StartGame()
    {
        // Code to start the game
        if (UISound != null) { UISound.Play(); } // play UI sound
        SceneManager.LoadScene("Map Scene");
    }
    [SerializeField] AudioSource UISound;
    public void BacktoMainMenu()
    {
        if (UISound != null) { UISound.Play(); } // play UI sound

        // Optionally destroy the singleton if needed
        Destroy(GameSelect2.Instance.gameObject);
        Debug.Log("GameSelect2 Singleton Destroyed");
        // Load the MainMenuScene
        SceneManager.LoadScene("MainMenuScene");
    }
}
