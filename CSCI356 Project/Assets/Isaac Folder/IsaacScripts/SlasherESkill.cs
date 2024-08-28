using System.Collections;
using UnityEngine;

public class SlasherESkill : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;
    private float comboCooldown = 7f; // Cooldown in seconds
    private float lastComboTime = -7f; // Last time the combo was used

    public float comboDuration = 2.5f; // Duration for the entire combo animation
    public float aoeRadius = 5f; // Radius within which damage will be dealt
    public int aoeDamage = 20; // Amount of damage to deal in the radius

    public AudioClip comboSound; // Audio clip for the combo
    private AudioSource audioSource; // Audio source for playing the sound

    public GameObject settingsPopup; // settings popup GameObject to pause

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // Check if settings menu is active
        if (settingsPopup.activeSelf)
        {
            return; // Skip processing if settings menu is open
        }
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= lastComboTime + comboCooldown)
        {
            if (!isAttacking)
            {
                StartCoroutine(PlayComboAnimation());
            }
        }
    }

    private IEnumerator PlayComboAnimation()
    {
        isAttacking = true;
        lastComboTime = Time.time;

        animator.SetTrigger("Combo");

        // Play the combo sound
        if (comboSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(comboSound);
        }

        yield return new WaitForSeconds(comboDuration);

        ApplyAOEDamage();

        isAttacking = false;
    }

    private void ApplyAOEDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, aoeRadius);

        foreach (var hitCollider in hitColliders)
        {
            Shootable target = hitCollider.GetComponent<Shootable>();
            if (target != null)
            {
                target.SetHealth(aoeDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aoeRadius);
    }
}
