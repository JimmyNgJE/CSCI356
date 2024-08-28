using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationScript : MonoBehaviour
{
    public Vector3 teleportLocation; // Set this in the Inspector

    public void TeleportPlayer()
    {
        transform.position = teleportLocation;
        Debug.Log("Player teleported to: " + teleportLocation);
    }
}
