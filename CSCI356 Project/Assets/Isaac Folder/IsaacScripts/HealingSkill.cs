using UnityEngine;
using System.Collections;

public class HealingSkill : MonoBehaviour
{
    public int healAmount = 50;   // Amount of health to heal
    public float cooldownTime = 10f; // Cooldown time in seconds

    private bool isCooldown = false;

    public GameObject settingsPopup; // settings popup GameObject to pause

    void Update()
    {
        // Check if settings menu is active
        if (settingsPopup.activeSelf)
        {
            return; // Skip processing if settings menu is open
        }
        if (Input.GetKeyDown(KeyCode.Q) && !isCooldown)
        {
            StartCoroutine(ActivateHealing());
        }

    }

    private IEnumerator ActivateHealing()
    {
        // Heal the player
        HealPlayer();

        // Start cooldown
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

    private void HealPlayer()
    {
        // Assuming you have a Health component on the player
        Health playerHealth = GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
            Debug.Log("Player healed by " + healAmount + " HP.");
        }
    }
}
