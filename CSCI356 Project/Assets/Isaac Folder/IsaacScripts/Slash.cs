using System.Collections;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public Animator animator;
    public string slashTriggerName = "Slash"; // Name of the trigger parameter in the Animator
    public int damage = 10;                   // Damage dealt by each slash
    public float slashRadius = 2f;            // Radius for detecting hits
    public AudioClip slashSound;              // Sound to play when slashing

    [Range(0f, 1f)]
    public float volume = 1.0f;               // Volume level for the slash sound

    private bool isAttacking = false;
    private AudioSource audioSource;

    public GameObject settingsPopup;          // settings popup GameObject to pause

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = volume;          // Set the initial volume
    }

    void Update()
    {
        // Check if settings menu is active
        if (settingsPopup.activeSelf)
        {
            return; // Skip processing if settings menu is open
        }

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(SlashRoutine());
        }
    }

    private IEnumerator SlashRoutine()
    {
        isAttacking = true;

        // Play the slash sound with the specified volume
        if (slashSound != null)
        {
            audioSource.PlayOneShot(slashSound, volume);
        }

        // Trigger the slash animation
        animator.SetTrigger(slashTriggerName);

        // Wait for the animation to start
        yield return new WaitForSeconds(0.1f); // Adjust if needed

        // Check for colliders hit by the slash
        DealDamage();

        // Wait for the animation to complete
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        isAttacking = false;
    }

    private void DealDamage()
    {
        // Define the radius and center for detecting hits
        Vector3 slashCenter = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(slashCenter, slashRadius);

        foreach (Collider col in hitColliders)
        {
            // Check if the collider belongs to a Shootable object or ShootableBoss
            Shootable shootable = col.GetComponent<Shootable>();
            ShootableBoss shootableBoss = col.GetComponent<ShootableBoss>();

            if (shootable != null)
            {
                // Apply damage to Shootable
                shootable.SetHealth(damage);
                Debug.Log($"Damaged {col.name} for {damage} damage.");
            }
            else if (shootableBoss != null)
            {
                // Apply damage to ShootableBoss
                shootableBoss.SetHealth(damage);
                Debug.Log($"Damaged {col.name} (Boss) for {damage} damage.");
            }
        }
    }

    // Visualize the damage area for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, slashRadius); // Match with slashRadius
    }
}
