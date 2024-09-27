using System.Collections;
using UnityEngine;

public class Catatonia : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private VolumeController volumeController;
    [SerializeField] private GameObject handsGO;
    [SerializeField] private Animator animator;
    [SerializeField] private string idleClosedName;

    public float idleClosedDuration;
    public float closeEyesDelayDuration;

    public void StartCatatonia()
    {
        StartCoroutine(TriggerHandsAnimation());
    }

    private IEnumerator TriggerHandsAnimation()
    {
        handsGO.SetActive(true);
        yield return new WaitForSeconds(closeEyesDelayDuration);
        StartCoroutine(TriggerVolumeVFX());
    }

    private IEnumerator TriggerVolumeVFX()
    {
        volumeController.StartVolumeVFX();

        yield return new WaitForSeconds(volumeController.startRageTogglingDuration);
        StartCoroutine(TriggerOpenAnimation());
    }

    private IEnumerator TriggerOpenAnimation()
    {
        playerController.ToggleMovement(false);
        playerController.ToggleCameraMovement(false);
       
        animator.SetBool(idleClosedName, true);
        yield return new WaitForSeconds(idleClosedDuration);
        animator.SetBool(idleClosedName, false);
        handsGO.SetActive(false);
        playerController.TeleportPlayer();

        SanityManager.Instance.isCatatoniaActive = false;
        SanityManager.Instance.shouldCatatoniaTrigger = false;

        volumeController.EndVolumeVFX();

        playerController.ToggleMovement(true);
        playerController.ToggleCameraMovement(true);
    }
}