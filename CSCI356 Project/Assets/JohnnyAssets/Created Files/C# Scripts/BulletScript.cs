using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject targetObject;
    private Health targetHealth;
    public float life = 3;

    private void Start()
    {
        targetObject = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (targetHealth.currentHealth != 0)
            {
                targetHealth.MinusHealth(10);
            }
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        Destroy(gameObject, life);
    }
}
