using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class IsaacOutOfBounds : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget; // The target location to teleport to
    [SerializeField] private string targetTag = "TeleportTrigger"; // Tag for the objects that trigger teleport
    public MapSettingsController mapSettingsController;
    public GameOverController gameOverController;
    public Health HealthScript;

    private CharacterController charController;

    void Start()
    {
        // Get the CharacterController component
        charController = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the target tag
        if (other.CompareTag(targetTag))
        {
            HealthScript.currentHealth = 100;
            TeleportPlayer(teleportTarget.position);
            mapSettingsController.UpdateDeathDisplay();
            gameOverController.ShowYouDieText();
        }
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
