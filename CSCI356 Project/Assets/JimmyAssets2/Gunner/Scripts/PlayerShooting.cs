using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public enum WeaponType { Handgun, MachineGun, Shotgun, Timebomb }
    public WeaponType currentWeapon = WeaponType.Handgun;

    public GameObject bulletPrefab;
    public GameObject timebombPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;
    public float machineGunFireRate = 0.1f;
    public int shotgunPellets = 5;
    public float shotgunSpreadAngle = 10f;
    public float maxThrowForce = 1f;
    public float minThrowForce = 0.2f;
    public float throwChargeTime = 2f;

    private float nextFireTime;
    private float currentThrowForce;
    private float throwChargeStartTime;

    private void Update()
    {
        switch (currentWeapon)
        {
            case WeaponType.Handgun:
                HandleHandgunShooting();
                break;
            case WeaponType.MachineGun:
                HandleMachineGunShooting();
                break;
            case WeaponType.Shotgun:
                HandleShotgunShooting();
                break;
            case WeaponType.Timebomb:
                HandleTimebombThrowing();
                break;
        }
    }

    private void HandleHandgunShooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void HandleMachineGunShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + machineGunFireRate;
        }
    }

    private void HandleShotgunShooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            for (int i = 0; i < shotgunPellets; i++)
            {
                ShootWithSpread();
            }
        }
    }

    private void HandleTimebombThrowing()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button pressed
        {
            throwChargeStartTime = Time.time;
        }

        if (Input.GetMouseButton(1)) // Right mouse button held
        {
            currentThrowForce = Mathf.Lerp(minThrowForce, maxThrowForce, (Time.time - throwChargeStartTime) / throwChargeTime);
        }

        if (Input.GetMouseButtonUp(1)) // Right mouse button released
        {
            ThrowTimebomb();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
    }

    private void ShootWithSpread()
    {
        float angleStep = shotgunSpreadAngle / (shotgunPellets - 1);
        float currentAngle = -shotgunSpreadAngle / 2;

        for (int i = 0; i < shotgunPellets; i++)
        {
            Quaternion bulletRotation = Quaternion.Euler(0, currentAngle, 0) * firePoint.rotation;
            Instantiate(bulletPrefab, firePoint.position, bulletRotation);
            currentAngle += angleStep;
        }
    }

    private void ThrowTimebomb()
    {
        // Offset position to avoid immediate collision with the player
        Vector3 spawnPosition = firePoint.position + firePoint.forward * 0.5f;

        GameObject timebomb = Instantiate(timebombPrefab, spawnPosition, firePoint.rotation);
        Rigidbody rb = timebomb.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply a small force to throw the timebomb a short distance
            rb.AddForce(firePoint.forward * currentThrowForce, ForceMode.Impulse);
        }

        currentThrowForce = minThrowForce; // Reset throw force
    }

}
