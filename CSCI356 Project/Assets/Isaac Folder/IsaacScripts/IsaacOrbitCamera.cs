using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsaacOrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target; // The player to follow
    public float rotSpeed = 1.5f; // Rotation sensitivity
    public float distance = 4.0f; // Distance from the player
    public float height = 1.8f; // Height above the player

    public float positionDamping = 0.1f; // Smooth damping for position
    public float rotationDamping = 0.1f; // Smooth damping for rotation

    private float rotY;
    private float rotX;
    private Vector3 velocity = Vector3.zero; // For smoothing position

    void Start()
    {
        rotY = transform.eulerAngles.y;
        rotX = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        // Update rotation based on mouse input
        rotY += Input.GetAxis("Mouse X") * rotSpeed;
        rotX -= Input.GetAxis("Mouse Y") * rotSpeed;
        rotX = Mathf.Clamp(rotX, -20f, 60f); // Limit vertical angle

        Quaternion desiredRotation = Quaternion.Euler(rotX, rotY, 0);

        // Calculate the desired camera position
        Vector3 desiredPosition = target.position - (desiredRotation * Vector3.forward * distance);
        desiredPosition.y += height;

        // Smoothly interpolate the camera's position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, positionDamping);

        // Smoothly interpolate the camera's rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationDamping);

        // Make the player rotate with the camera
        target.rotation = Quaternion.Euler(0, rotY, 0);
    }
}