using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class JimmyShootable : MonoBehaviour
{
    public int curHealth = 10;
    [SerializeField] int health = 10;
    public void SetHealth(int damage)
    {
        health -= damage;
    }
    void Update()
    {
        AdjustCurrentHealth(0);
    }
    public void AdjustCurrentHealth(int adj)
    {
        health += adj;

        if (curHealth < 0)
            Destroy(gameObject);

        if (curHealth > health)
            curHealth = health;

        if (health < 1)
            health = 1;

        //healthBarLength = (Screen.width / 2) * (curHealth / (float)maxHealth);
    }
}
