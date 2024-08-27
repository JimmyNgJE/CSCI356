using UnityEngine;
using TMPro;
using UnityEngine.UI; // For UI components
public class MapSettingsController : MonoBehaviour
{
    public TMP_Text difficultyText;
    public TMP_Text deathText;
    public TMP_Text characterText;
    public Image characterThumbnail;
    public Sprite slasherThumbnail;
    public Sprite gunnerThumbnail;
    public int death = 0;

    void Start()
    {
        if (GameSelect2.Instance != null)
        {
            // Retrieve the selected difficulty from the singleton
            string difficultySelection = GameSelect2.Instance.SelectedDifficulty;
            // Update the text component to show the selected difficulty
            UpdateDifficultyDisplay(difficultySelection);

            // Retrieve the selected character from the singleton
            string characterSelection = GameSelect2.Instance.SelectedCharacter;
            // Update the text component to show the selected difficulty
            UpdateCharacterTextDisplay(characterSelection);
            UpdateCharacterThumbnailDisplay(characterSelection);
        }
        else
        {
            Debug.LogError("GameSelect2 instance is not available.");
        }
    }

    void UpdateDifficultyDisplay(string difficulty)
    {
        if (difficultyText != null)
        {
            // Update the UI text based on the difficulty value
            difficultyText.text = $"Difficulty: {difficulty}";
        }
    }
    public void UpdateDeathDisplay()
    {
        death += 1;
        if (deathText != null)
        {
            // Update the UI text based on the death value
            deathText.text = $"Death: {death}";
        }
    }
    void UpdateCharacterTextDisplay(string character)
    {
        if (characterText != null)
        {
            if (character == "Character1")
            {
                // Update the UI text based on the character
                characterText.text = "Slasher";
            }
            else
            {
                // Update the UI text based on the character
                characterText.text = "Gunner";
            }

            
        }
    }
    void UpdateCharacterThumbnailDisplay(string character)
    {

        if (characterThumbnail != null)
        {
            if (character == "Character1")
            {
                // Update the UI text based on the character
                characterThumbnail.sprite = slasherThumbnail;
            }
            else
            {
                // Update the UI text based on the character
                characterThumbnail.sprite = gunnerThumbnail;
            }
        }
    }
}