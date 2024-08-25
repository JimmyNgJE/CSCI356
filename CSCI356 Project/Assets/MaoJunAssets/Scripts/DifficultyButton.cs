using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public Color selectedColor = Color.green;
    public Color normalColor = Color.white;
    private Button button;
    private Image buttonImage;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        button.onClick.AddListener(OnButtonClicked);
        // Ensure the button starts with the normal color
        buttonImage.color = normalColor;
    }

    void OnButtonClicked()
    {
        // Call a method to update the selected difficulty
        DifficultyController.Instance.SetDifficulty(gameObject.name);
        // Update visual state
        DifficultyController.Instance.UpdateButtonVisuals(this);
    }

    public void SetSelected(bool isSelected)
    {
        buttonImage.color = isSelected ? selectedColor : normalColor;
    }
}
