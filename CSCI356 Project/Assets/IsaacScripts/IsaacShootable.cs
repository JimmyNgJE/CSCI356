using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class IsaacShootable : MonoBehaviour
{
        [SerializeField] int health = 10;
        public void SetHealth(int damage)
        {
            health -= damage;
        }
    
}
