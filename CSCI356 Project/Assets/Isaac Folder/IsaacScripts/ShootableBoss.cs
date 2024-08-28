using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShootableBoss : MonoBehaviour
{
    [SerializeField] int health = 300;
    [SerializeField] GameObject destructionEffectPrefab;
    [SerializeField] GameObject hpBarPrefab; // Reference to the HP bar prefab
    private GameObject hpBarInstance;
    private Canvas canvas;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public GameOverController GameOverController;

    public float fullWidth = 20f;
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
        // Update the HP bar position as the boss moves
        UpdateHPBarPosition();
        UpdateHPBarVisibility();  // Handle visibility based on distance to player
        UpdateHPBarFill();  // Update the HP bar fill based on current health
        UpdateHealthBarWidth();
    }

    public void SetHealth(int damage)
    {
        health -= damage;
        Debug.Log($"Health after damage: {health}");

        if (health <= 0)
        {
            HandleDestruction();
            GameOverController.OnEnemyDeath(gameObject);
            Destroy(hpBarInstance); // Destroy the HP bar first
            Destroy(gameObject); // Then destroy the boss
            Debug.Log("Boss destroyed.");
        }
    }

    private void HandleDestruction()
    {
        if (destructionEffectPrefab != null)
        {
            // Instantiate the effect at the boss's position and rotation
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
        health = 300;
    }

    private void UpdateHPBarPosition()
    {
        // Convert the boss's world position to screen position
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        // Set the position of the HP bar instance (with a downward offset)
        hpBarInstance.transform.position = screenPosition + new Vector3(0, 50, 0); // Adjust the offset as needed
    }

    private void UpdateHPBarFill()
    {
        if (hpBarInstance != null)
        {
            Image hpBarFill = hpBarInstance.transform.Find("Fill Area/Fill").GetComponent<Image>();
            hpBarFill.fillAmount = (float)health / 300f; // Assuming the full health is 300

            TMP_Text healthText = hpBarInstance.transform.Find("Health Text").GetComponent<TMP_Text>();
            healthText.text = $"{health}";
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
    private void UpdateHealthBarWidth()
    {
        float healthPercentage = (float)health / 300;
        float newWidth = fullWidth * healthPercentage;

        // Set the width of the fillRect
        RectTransform rect = hpBarInstance.transform.Find("Fill Area/Fill").GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(newWidth, rect.sizeDelta.y);
    }
}
