using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    //public TeleportationScript teleportationScript;
    public MapSettingsController mapSettingsController;
    public GameOverController gameOverController;
    void Start()
    {
        currentHealth = maxHealth;
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
            //currentHealth = 100;
            mapSettingsController.UpdateDeathDisplay();
            gameOverController.ShowYouDieText();

        }
        Debug.Log("Current Health: " + currentHealth);
    }
}
