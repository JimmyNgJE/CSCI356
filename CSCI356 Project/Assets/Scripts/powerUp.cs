using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    public float PowerUp = 10.0f;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame


    void OnTriggerEnter(Collider other)
    {
        RelativeMovement rm = player.GetComponent<RelativeMovement>();
        rm.moveSpeed += PowerUp;
        Destroy(this.gameObject);
    }
}
