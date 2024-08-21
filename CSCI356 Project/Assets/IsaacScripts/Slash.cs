using System.Collections;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public Animator animator;
    public string slashTriggerName = "Slash"; // Name of the trigger parameter in the Animator

    private bool isAttacking = false;

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

        // Trigger the slash animation
        animator.SetTrigger(slashTriggerName);

        // Wait for the animation to complete (adjust the time as needed)
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        isAttacking = false;
    }
}
