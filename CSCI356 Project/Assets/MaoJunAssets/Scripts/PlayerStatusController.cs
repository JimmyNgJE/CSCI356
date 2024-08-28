using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatusController : MonoBehaviour
{
    public Slider healthBar;
    public TMP_Text healthText; // Reference to the TextMeshPro component
    public RectTransform fillRect; // Reference to the RectTransform of the fill area

    public float fullWidth = 10f; // Width of the health bar when full (in cm or any unit)

    private Health playerHealth;

    void Start()
    {
        // Find the active player with the Health script
        playerHealth = FindObjectOfType<Health>();
        if (playerHealth == null)
        {
            Debug.LogError("No Health script found in the scene.");
            return;
        }

        // Set the health bar's maximum value to the player's maximum health
        healthBar.maxValue = playerHealth.maxHealth;
        // Initialize the health bar's value to the player's current health
        healthBar.value = playerHealth.currentHealth;
        // Initialize the health text
        UpdateHealthText();
        // Initialize the health bar width
        UpdateHealthBarWidth();
    }

    void Update()
    {
        if (playerHealth != null)
        {
            // Update the health bar's value to match the player's current health
            healthBar.value = playerHealth.currentHealth;
            // Update the health text
            UpdateHealthText();
            // Update the health bar width
            UpdateHealthBarWidth();
        }
    }

    // Method to update the health text
    void UpdateHealthText()
    {
        healthText.text = $"{playerHealth.currentHealth}/{playerHealth.maxHealth}";
    }

    // Method to update the health bar width based on health percentage
    void UpdateHealthBarWidth()
    {
        float healthPercentage = playerHealth.currentHealth / (float)playerHealth.maxHealth;
        float newWidth = fullWidth * healthPercentage;

        // Set the width of the fillRect
        fillRect.sizeDelta = new Vector2(newWidth, fillRect.sizeDelta.y);
    }
}
