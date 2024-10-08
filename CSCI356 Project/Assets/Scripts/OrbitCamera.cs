using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    // the orbit target (e.g., the player GameObject)
    [SerializeField] Transform target;

    // rotation sensitivity
    public float rotSpeed = 1.5f;

    private float rotY;     // horizontal rotation
    private float rotX;
    private Vector3 offset; // offset from the target

    // Start is called before the first frame update
    void Start()
    {
        // get transform component's yaw
        rotY = transform.eulerAngles.y;
        rotX = transform.eulerAngles.x;

        // calculate camera's offset from the target
        offset = target.position - transform.position;
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        // yaw based on horizontal mouse movement
        rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
        rotX += Input.GetAxis("Mouse Y") * rotSpeed * 3;

        // create quaternion based on rotation angle
        Quaternion rotation = Quaternion.Euler(rotX, rotY, 0);

        // set the camera's position based on the rotated offset
        transform.position = target.position - (rotation * offset);

        // rotate camera to look at the target
        transform.LookAt(target);
    }
}
