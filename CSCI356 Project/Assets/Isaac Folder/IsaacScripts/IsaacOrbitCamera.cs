using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsaacOrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    public float rotSpeed = 1.5f;
    public float distance = 5.0f; // Distance from the player
    public float height = 2.0f; // Height above the player

    private float rotY;
    private float rotX;

    void Start()
    {
        rotY = transform.eulerAngles.y;
        rotX = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        rotY += Input.GetAxis("Mouse X") * rotSpeed;
        rotX -= Input.GetAxis("Mouse Y") * rotSpeed;
        rotX = Mathf.Clamp(rotX, -20f, 60f); // Limit vertical angle

        Quaternion rotation = Quaternion.Euler(rotX, rotY, 0);

        // Calculate the desired camera position
        Vector3 position = target.position - (rotation * Vector3.forward * distance);
        position.y += height;

        transform.position = position;
        transform.LookAt(target);

        // Make the player rotate with the camera
        target.rotation = Quaternion.Euler(0, rotY, 0);
    }
}
