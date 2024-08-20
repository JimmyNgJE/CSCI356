using UnityEngine;
using System.Collections.Generic;

public class DifficultyController : MonoBehaviour
{
    public static DifficultyController Instance { get; private set; }

    public DifficultyButton easyButton;
    public DifficultyButton mediumButton;
    public DifficultyButton hardButton;

    private DifficultyButton selectedButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDifficulty(string difficultyName)
    {
        // Handle difficulty setting logic
        Debug.Log("Selected Difficulty: " + difficultyName);
    }

    public void UpdateButtonVisuals(DifficultyButton selectedButton)
    {
        if (this.selectedButton != null)
        {
            this.selectedButton.SetSelected(false);
        }
        this.selectedButton = selectedButton;
        this.selectedButton.SetSelected(true);
    }
}
