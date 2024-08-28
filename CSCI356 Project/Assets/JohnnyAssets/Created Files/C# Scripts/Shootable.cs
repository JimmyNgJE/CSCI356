using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shootable : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] GameObject destructionEffectPrefab;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    public void SetHealth(int damage)
    {
        health -= damage;
        Debug.Log($"Health after damage: {health}");
        if (health <= 0)
        {
            HandleDestruction();
            //Destroy(gameObject);
            gameObject.SetActive(false);
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
    }
}
