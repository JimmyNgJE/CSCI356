using UnityEngine;

public class HeadAndBodyMovement : MonoBehaviour
{
    public Transform cameraTransform;   // Reference to the camera
    public Transform playerBody;        // Reference to the player's body
    public float bodyPitchMultiplier = 0.5f;  // Multiplier for body pitch, adjust as needed

    private float verticalRotation;

    void Update()
    {
        // Get the vertical rotation of the camera
        verticalRotation = cameraTransform.localEulerAngles.x;

        // Clamp the vertical rotation to avoid flipping
        if (verticalRotation > 180)
        {
            verticalRotation -= 360;
        }

        // Apply the vertical rotation to the player's body
        playerBody.localRotation = Quaternion.Euler(verticalRotation * bodyPitchMultiplier, playerBody.localEulerAngles.y, 0);
    }
}
