using System.Collections;
using UnityEngine;

public class MeleeCombo : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;
    private float comboCooldown = 7f; // Cooldown in seconds
    private float lastComboTime = -7f; // Last time the combo was used

    public float comboDuration = 2.5f; // Duration for the entire combo animation

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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

        yield return new WaitForSeconds(comboDuration);

        ApplyAOEDamage();

        isAttacking = false;
    }

    private void ApplyAOEDamage()
    {
        float aoeRadius = 5f;
        int aoeDamage = 20;

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
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
