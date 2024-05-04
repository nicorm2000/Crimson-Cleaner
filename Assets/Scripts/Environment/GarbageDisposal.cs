using UnityEngine;
using System.Collections;

public class GarbageDisposal : MonoBehaviour
{
    private bool animationPlaying = false; 
    private Animator garbageDisposalAnimator;

    private void Start()
    {
        garbageDisposalAnimator = gameObject.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!animationPlaying && other.CompareTag("Trash")) 
        {
            Animator animator = other.GetComponentInParent<Animator>();

            if (animator != null) 
            {
                StartCoroutine(PlayAnimationAndDestroy(animator)); 
            }
            else
            {
                Debug.LogWarning("Trash object does not have an Animator component.");
            }
        }
    }

    IEnumerator PlayAnimationAndDestroy(Animator animator)
    {
        animationPlaying = true;
        animator.SetTrigger("DisposalTrigger");
        garbageDisposalAnimator.SetBool("isGarbageDisposalOpening", true);

        if (animator.runtimeAnimatorController != null && animator.runtimeAnimatorController.animationClips.Length > 0)
        {
            float animationDuration = animator.runtimeAnimatorController.animationClips[0].length;

            yield return new WaitForSeconds(animationDuration);
        }
        else
        {
            Debug.LogWarning("Animator doesn't have any animation clips.");
        }

        Destroy(animator.gameObject); 
        animationPlaying = false; 
        garbageDisposalAnimator.SetBool("isGarbageDisposalOpening", false);
    }
}
