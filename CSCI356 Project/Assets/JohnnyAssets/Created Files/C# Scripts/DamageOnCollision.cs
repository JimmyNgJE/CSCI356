using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DamageOnCollision : MonoBehaviour
{
    public int damageAmount = 10; // The amount of damage to take

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a NavMesh Agent
        if (collision.gameObject.GetComponent<NavMeshAgent>())
        {
            // Assume the game object has a health script attached
            Shootable health = GetComponent<Shootable>();

            if (health != null)
            {
                health.SetHealth(damageAmount);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the other collider belongs to a NavMesh Agent
        if (other.GetComponent<NavMeshAgent>())
        {
            // Assume the game object has a health script attached
            Shootable health = GetComponent<Shootable>();

            if (health != null)
            {
                health.SetHealth(damageAmount);
            }
        }
    }
}