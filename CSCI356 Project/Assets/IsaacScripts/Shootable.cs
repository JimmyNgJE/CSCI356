using UnityEngine;

public class Shootable : MonoBehaviour
{
    public int health = 100;

    public void SetHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle death (e.g., play death animation, destroy object, etc.)
        Destroy(gameObject);
    }
}
