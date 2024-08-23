using System.Collections;
using UnityEngine;

public class JimmyShooter : MonoBehaviour
{
    public float impulseStrength = 5.0f;
    public GameObject particleSystemPrefab;
    public GameObject bulletPrefab; // Bullet prefab
    public Transform bulletSpawnPoint; // Spawn point for the bullet
    public float bulletSpeed = 20.0f; // Speed of the bullet
    public float fireRate = 0.5f;
    public int damage = 10;

    private bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        // Hide the mouse cursor at the centre of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnGUI()
    {
        int size = 12;

        // Centre of screen and caters for font size
        float posX = Screen.width / 2 - size / 4;
        float posY = Screen.height / 2 - size / 2;

        // Displays "*" on screen
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    // Update is called once per frame
    void Update()
    {
        // On left mouse button click
        if (Input.GetMouseButtonDown(0) && !isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootContinuously());
        }

        if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
            StopCoroutine(ShootContinuously());
        }
    }

    private IEnumerator ShootContinuously()
    {
        while (isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void Shoot()
    {
        // Create a bullet and set its position and rotation
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Get the Rigidbody component of the bullet and apply force
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = bulletSpawnPoint.forward * bulletSpeed;

        // Raycast to detect what the bullet might hit
        Ray ray = new Ray(bulletSpawnPoint.position, bulletSpawnPoint.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Shootable target = hit.transform.GetComponent<Shootable>();

            if (target != null)
            {
                target.SetHealth(damage);
            }

            StartCoroutine(GeneratePS(hit));
        }
    }

    private IEnumerator GeneratePS(RaycastHit hit)
    {
        GameObject ps = Instantiate(particleSystemPrefab, hit.point, Quaternion.LookRotation(hit.normal));

        yield return new WaitForSeconds(1);

        Destroy(ps);
    }
}
