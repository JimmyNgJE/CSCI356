using UnityEngine;
using System.Collections;

public class GameSelect : MonoBehaviour
{
    public GameObject character1;
    public GameObject character2;
    string characterSelection;
    string difficultySelection;
    void Start()
    {
        if (GameSelect2.Instance != null) {
        characterSelection = GameSelect2.Instance.SelectedCharacter;
        difficultySelection = GameSelect2.Instance.SelectedDifficulty;
        }
        // Deactivate both characters first
        character1.SetActive(false);
        character2.SetActive(false);
        // Instantiate or activate the selected character and difficulty
        // Activate the selected character
        if (characterSelection == "Character1")
        {
            character1.SetActive(true);
        }
        else
        {
            character2.SetActive(true);
        }
    }

}