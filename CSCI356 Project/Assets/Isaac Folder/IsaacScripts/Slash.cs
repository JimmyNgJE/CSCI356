using System.Collections;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public Animator animator;
    public string slashTriggerName = "Slash"; // Name of the trigger parameter in the Animator
    public int damage = 10;                   // Damage dealt by each slash
    public float slashRadius = 2f;            // Radius for detecting hits
    public AudioClip slashSound;              // Sound to play when slashing

    private bool isAttacking = false;
    private AudioSource audioSource;

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(SlashRoutine());
        }
    }

    private IEnumerator SlashRoutine()
    {
        isAttacking = true;

        // Play the slash sound
        if (slashSound != null)
        {
            audioSource.PlayOneShot(slashSound);
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
            // Check if the collider belongs to a Shootable object
            Shootable shootable = col.GetComponent<Shootable>();
            if (shootable != null)
            {
                // Apply damage
                shootable.SetHealth(damage);
                Debug.Log($"Damaged {col.name} for {damage} damage.");
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
