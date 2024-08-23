using UnityEngine;

public class IsaacOrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target; // The player to follow
    public float distance = 5.0f; // Distance behind the player
    public float height = 2.0f; // Height above the player
    public float lookAtHeight = 1.5f; // Height offset for LookAt target
    public float rotSpeed = 1.5f; // Rotation sensitivity

    private float rotY; // Horizontal rotation
    private float rotX; // Vertical rotation

    void Start()
    {
        // Initialize rotation based on current camera angles
        rotY = transform.eulerAngles.y;
        rotX = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        // Update rotation based on mouse input
        rotY += Input.GetAxis("Mouse X") * rotSpeed;
        rotX -= Input.GetAxis("Mouse Y") * rotSpeed;

        // Clamp vertical rotation to avoid flipping
        rotX = Mathf.Clamp(rotX, -20f, 60f);

        // Create rotation based on updated angles
        Quaternion rotation = Quaternion.Euler(rotX, rotY, 0);

        // Calculate the desired camera position
        Vector3 position = target.position - (rotation * Vector3.forward * distance);
        position.y += height;

        // Set the camera position
        transform.position = position;

        // Adjust the LookAt target height
        Vector3 lookAtTarget = target.position + Vector3.up * lookAtHeight;

        // Make the camera look at the adjusted target
        transform.LookAt(lookAtTarget);

        // Make the player rotate with the camera
        target.rotation = Quaternion.Euler(0, rotY, 0);
    }
}