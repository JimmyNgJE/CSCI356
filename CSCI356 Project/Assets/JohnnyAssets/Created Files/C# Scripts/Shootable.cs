using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shootable : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] GameObject destructionEffectPrefab;
    public void SetHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (destructionEffectPrefab != null)
            {
                Instantiate(destructionEffectPrefab, transform.position, transform.rotation);
            }
        }
        Destroy(gameObject);
    }
}
