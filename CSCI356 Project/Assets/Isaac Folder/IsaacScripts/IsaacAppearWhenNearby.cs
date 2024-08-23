using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsaacAppearWhenNearby : MonoBehaviour
{
    public GameObject objectToAppear;
    public float appearanceDistance = 1f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        objectToAppear.SetActive(false);
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.position, transform.position);

            if (distance <= appearanceDistance)
            {
                objectToAppear.SetActive(true);
            }
            else
            {
                objectToAppear.SetActive(false);
            }
        }
    }
}
