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

    // Start is called before the first frame update
    void Start()
    {
        shotTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.position);
        HandleShooting();
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
