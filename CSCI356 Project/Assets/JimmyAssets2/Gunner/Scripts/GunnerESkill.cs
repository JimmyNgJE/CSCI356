using System.Collections;
using UnityEngine;

public class GunnerESkill : MonoBehaviour
{
    public GameObject pelletPrefab;        // Pellet prefab to be shot
    public Transform firePoint;            // Point from where pellets will be fired
    public float skillCooldown = 7.0f;     // Cooldown time in seconds
    public int pelletCount = 8;            // Number of pellets to fire
    public float spreadAngle = 15.0f;      // Spread angle in degrees
    public float pelletSpeed = 100f;       // Speed of each pellet

    private bool canUseSkill = true;       // To check if the skill is on cooldown

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUseSkill)
        {
            StartCoroutine(UseSkill());
        }
    }

    private IEnumerator UseSkill()
    {
        canUseSkill = false;

        // Fire pellets with spread pattern
        FirePellets();

        // Wait for cooldown to complete
        yield return new WaitForSeconds(skillCooldown);

        canUseSkill = true;
    }

    private void FirePellets()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            // Calculate horizontal spread
            float angle = Random.Range(-spreadAngle, spreadAngle);
            // Create a forward direction with horizontal spread
            Vector3 direction = Quaternion.Euler(0, angle, 0) * firePoint.forward;

            // Instantiate the pellet
            GameObject pellet = Instantiate(pelletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody rb = pellet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Set the velocity of the pellet directly
                rb.velocity = direction * pelletSpeed;
            }
        }
    }
}
