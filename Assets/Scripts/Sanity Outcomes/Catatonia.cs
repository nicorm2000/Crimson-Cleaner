using System.Collections;
using UnityEngine;

public class Catatonia : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator animator;
    [SerializeField] private string idleClosedName;

    public float idleClosedDuration;

    public void ToggleEyes()
    {
        StartCoroutine(TriggerOpenAnimation());
    }

    private IEnumerator TriggerOpenAnimation()
    {
        playerController.ToggleMovement(false);
        playerController.ToggleCameraMovement(false);

        animator.SetBool(idleClosedName, true);
        yield return new WaitForSeconds(idleClosedDuration);
        animator.SetBool(idleClosedName, false);

        SanityManager.Instance.isCatatoniaActive = false;
        SanityManager.Instance.shouldCatatoniaTrigger = false;

        playerController.ToggleMovement(true);
        playerController.ToggleCameraMovement(true);
    }
}