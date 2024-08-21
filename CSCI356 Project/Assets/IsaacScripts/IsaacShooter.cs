using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsaacShooter : MonoBehaviour
{
    private Camera cam;     // stores camera component
    public float impulseStrength = 5.0f;
    public GameObject particleSysPrefab;

    public GameObject grenadePrefab;
    public float grenadeImpulse = 5.0f;

    private IEnumerator GeneratePS(RaycastHit hit)
    {
        GameObject ps = Instantiate(particleSysPrefab, hit.point,
        Quaternion.LookRotation(hit.normal));
        yield return new WaitForSeconds(1);
        Destroy(ps);
    }
    // Start is called before the first frame update
    void Start()
    {
        // gets the GameObject's camera component
        cam = GetComponent<Camera>();

        // hide the mouse cursor at the centre of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnGUI()
    {
        int size = 12;

        // centre of screen and caters for font size
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;

        // displays "*" on screen
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    // Update is called once per frame
    void Update()
    {
        // on left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            // get point in the middle of the screen
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);

            // create a ray from the point in the direction of the camera
            Ray ray = cam.ScreenPointToRay(point);

            RaycastHit hit; // stores ray intersection information

            // ray cast will obtain hit information if it intersects anything
            if (Physics.Raycast(ray, out hit))
            {
                // get the GameObject that was hit
                GameObject hitObject = hit.transform.gameObject;
                Shootable target = hitObject.GetComponent<Shootable>();

                if (target != null)
                {
                    Vector3 impulse = Vector3.Normalize(hit.point - transform.position)
                    * impulseStrength;
                    hit.rigidbody.AddForceAtPosition(impulse, hit.point, ForceMode.Impulse);
                    StartCoroutine(GeneratePS(hit));
                }

            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            GameObject grenade = Instantiate(grenadePrefab, transform);
            grenade.transform.position = cam.transform.position + cam.transform.forward * 2;

            Rigidbody target = grenade.GetComponent<Rigidbody>();

            Vector3 impulse = cam.transform.forward * grenadeImpulse;
            target.AddForceAtPosition(impulse, cam.transform.position, ForceMode.Impulse);
        }
    }
}
