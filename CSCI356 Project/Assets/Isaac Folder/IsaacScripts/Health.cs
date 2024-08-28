using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    [SerializeField] private Transform teleportTarget;
    public MapSettingsController mapSettingsController;
    public GameOverController gameOverController;

    private CharacterController charController;
    void Start()
    {
        currentHealth = maxHealth;
        charController = GetComponent<CharacterController>();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Current Health: " + currentHealth);
    }

    public void MinusHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            mapSettingsController.UpdateDeathDisplay();
            gameOverController.ShowYouDieText();
            TeleportPlayer(teleportTarget.position);
        }
        Debug.Log("Current Health: " + currentHealth);
    }

    private void TeleportPlayer(Vector3 newPosition)
    {
        if (charController != null)
        {
            charController.enabled = false; // Disable the CharacterController
            transform.position = newPosition; // Teleport to the new position
            charController.enabled = true; // Re-enable the CharacterController
        }
    }
}
