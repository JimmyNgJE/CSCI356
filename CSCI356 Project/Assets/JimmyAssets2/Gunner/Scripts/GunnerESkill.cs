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
    public float pelletLifetime = 5f;      // Time after which the pellet will be destroyed
    public int damage = 50;                // Damage to be dealt
    public GameObject particleSystemPrefab; // Particle effect prefab for hits
    public AudioClip skillSound; // Skill sound clip
    private AudioSource skillAudioSource; // Specific AudioSource for the skill

    private bool canUseSkill = true;       // To check if the skill is on cooldown

    void Start()
    {
        // Add and configure the AudioSource for skill sound
        skillAudioSource = gameObject.AddComponent<AudioSource>();
        skillAudioSource.clip = skillSound;
    }

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

        // Play the skill sound
        skillAudioSource.Play();

        // Fire pellets with a spread pattern
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

                // Handle collision detection directly in the script
                PelletCollision pelletCollision = pellet.AddComponent<PelletCollision>();
                pelletCollision.Initialize(damage, particleSystemPrefab);
            }

            // Destroy the pellet after a certain time
            Destroy(pellet, pelletLifetime);
        }
    }

    private class PelletCollision : MonoBehaviour
    {
        private int damage;
        private GameObject particleEffectPrefab;

        public void Initialize(int damage, GameObject particleEffectPrefab)
        {
            this.damage = damage;
            this.particleEffectPrefab = particleEffectPrefab;
        }

        private void OnTriggerEnter(Collider other)
        {
            Shootable shootable = other.GetComponent<Shootable>();
            if (shootable != null)
            {
                shootable.SetHealth(damage);

                // Instantiate particle effect at the point of collision
                if (particleEffectPrefab != null)
                {
                    GameObject effect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
                    Destroy(effect, 1f); // Adjust duration as needed
                }

                Destroy(gameObject); // Destroy the pellet upon collision
            }
        }
    }
}