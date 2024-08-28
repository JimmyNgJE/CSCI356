using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public TeleportationScript teleportationScript;
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
        if (currentHealth < 0)
        {
            currentHealth = 100;
            gameObject.SetActive(false);
        }
        Debug.Log("Current Health: " + currentHealth);
    }
}
