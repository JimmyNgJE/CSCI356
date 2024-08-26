using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float detectionRadius = 50f;
    public float wanderRadius = 5f;
    public float wanderTimer = 5f;

    private float timer;

    public GameObject bulletPrefab; // Assign the bullet prefab in the Inspector
    public Transform shootingPoint; // The point from where bullets will be shot
    public float shootingRange = 10f; // Range within which the AI will shoot
    public float shootingInterval = 2f; // Time between shots
    public float bulletSpeed = 20f;
    private float shootingTimer;

    void Start()
    {
        timer = wanderTimer;
    }

    void Update()
    {
        // Increment timer and check if it's time to choose a new wander destination
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Wanderer();
            timer = 0;
        }

        // Check if the player is within the detection radius
        if (Vector3.Distance(player.position, transform.position) <= detectionRadius)
        {
            // Set the player's position as the destination
            agent.SetDestination(player.position);
            //Debug.Log("player detected");
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= shootingRange)
            {
                shootingTimer -= Time.deltaTime;
                if (shootingTimer <= 0f)
                {
                    Shoot();
                    shootingTimer = shootingInterval;
                }
            }

        }
    }

    void Wanderer()
    {
        // Get a random point on the NavMesh within the wander radius
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void Shoot()
    {
        if (bulletPrefab != null && shootingPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = shootingPoint.forward * bulletSpeed;
            }
        }
    }
}
