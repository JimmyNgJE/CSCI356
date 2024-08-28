using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget; // The target location to teleport to
    [SerializeField] private string targetTag = "TeleportTrigger"; // Tag for the objects that trigger teleport
    public MapSettingsController mapSettingsController;
    public GameOverController gameOverController;

    private CharacterController charController;

    void Start()
    {
        charController = GetComponent<CharacterController>();

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the target tag
        if (other.CompareTag(targetTag))
        {
            TeleportPlayer(teleportTarget.position);
        }
    }

    private void TeleportPlayer(Vector3 newPosition)
    {
        if (charController != null)
        {
            mapSettingsController.UpdateDeathDisplay();
            gameOverController.ShowYouDieText();
            charController.enabled = false; // Disable the CharacterController
            transform.position = newPosition; // Teleport to the new position
            charController.enabled = true; // Re-enable the CharacterController
        }
    }
}
