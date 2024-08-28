using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class Shootable : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] GameObject destructionEffectPrefab;
    [SerializeField] GameObject hpBarPrefab; // Reference to the HP bar prefab
    private GameObject hpBarInstance;
    private Canvas canvas;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Find the Canvas in the scene
        canvas = FindObjectOfType<Canvas>();

        // Instantiate the HP bar and set it as a child of the canvas
        hpBarInstance = Instantiate(hpBarPrefab, canvas.transform);
        UpdateHPBarPosition();
        UpdateHPBarFill();
    }

    void Update()
    {
        // Update the HP bar position as the enemy moves
        UpdateHPBarPosition();
        UpdateHPBarVisibility();  // New method to handle visibility
        UpdateHPBarFill();
    }

    public void SetHealth(int damage)
    {
        health -= damage;
        Debug.Log($"Health after damage: {health}");
        UpdateHPBarFill(); // Update the HP bar fill amount

        if (health <= 0)
        {
            //HandleDestruction();
            gameObject.SetActive(false);
            hpBarInstance.SetActive(false); // Hide the HP bar
            Debug.Log("Object destroyed.");
            Invoke("respawnShootable", 5.0f);
        }
    }

    private void HandleDestruction()
    {
        if (destructionEffectPrefab != null)
        {
            // Instantiate the effect at the object's position and rotation
            GameObject effect = Instantiate(destructionEffectPrefab, transform.position, transform.rotation);

            // Destroy the effect after 5 seconds
            Destroy(effect, 5f);

            Debug.Log("Destruction effect instantiated and will be destroyed after 5 seconds.");
        }
    }

    void respawnShootable()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        gameObject.SetActive(true);
        health = 100;
        UpdateHPBarFill(); // Reset the HP bar fill amount
        hpBarInstance.SetActive(true); // Show the HP bar
    }

    private void UpdateHPBarPosition()
    {
        // Convert the enemy's world position to screen position
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        // Set the position of the HP bar instance (with a downward offset)
        hpBarInstance.transform.position = screenPosition - new Vector3(0, 50, 0); // Adjust the offset as needed
    }

    private void UpdateHPBarFill()
    {
        if (hpBarInstance != null)
        {
            Image hpBarFill = hpBarInstance.transform.Find("Fill Area/Fill").GetComponent<Image>();
            hpBarFill.fillAmount = (float)health / 100; // Assuming the full health is 100
            TMP_Text healthText = hpBarInstance.transform.Find("Health Text").GetComponent<TMP_Text>();
            healthText.text = $"{health}/100";
        }
    }

    private void UpdateHPBarVisibility()
    {
        // Assuming you have a reference to the player in your scene
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            float maxVisibleDistance = 20f; // Set this to the max distance where you want the HP bar to be visible

            CanvasGroup canvasGroup = hpBarInstance.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = hpBarInstance.AddComponent<CanvasGroup>();
            }

            if (distanceToPlayer > maxVisibleDistance)
            {
                // Hide or make the HP bar transparent if the player is too far
                canvasGroup.alpha = 0;
            }
            else
            {
                // Show the HP bar if the player is within the visible range
                canvasGroup.alpha = 1;
            }
        }
    }
}
