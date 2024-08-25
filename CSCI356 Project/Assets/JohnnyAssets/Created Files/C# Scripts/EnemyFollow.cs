using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;

    [SerializeField] private float timer = 5;
    private float shotTimer;

    public GameObject Bullet;
    public Transform bulletSpawnPoint;
    public float enemySpeed;
    public float sightDistance = 10;
    public float sightAngle = 45;

    // Start is called before the first frame update
    void Start()
    {
        // Find the active player object by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        shotTimer = timer;
    }


    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.position);
        if (CanSeePlayer())
        {
            HandleShooting();
        }
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        if (Vector3.Angle(transform.forward, directionToPlayer) < sightAngle / 2f)
        {
            if (directionToPlayer.magnitude < sightDistance)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, sightDistance))
                {
                    if (hit.transform == player)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void HandleShooting()
    {
        shotTimer -= Time.deltaTime;
        if (shotTimer <= 0)
        {
            ShootAtPlayer();
            shotTimer = timer;
        }
    }

    void ShootAtPlayer()
    {

        GameObject bulletObj = Instantiate(Bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();

        bulletRig.AddForce(bulletRig.transform.forward * enemySpeed);
        Destroy(bulletObj, 5f);
    }
}
