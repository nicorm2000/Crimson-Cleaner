using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Rage : MonoBehaviour
{
    public HighTierOutcome rageOutcome;
    public PlayerController playerController;
    public CleaningManager cleaningManager;

    public VolumeController volumeController;

    private ToolAnimatorController toolAnimatorController;

    private void Start()
    {
        toolAnimatorController = cleaningManager.GetToolSelector().toolAnimatorController;
    }

    public void StartRageDuration()
    {
        StartCoroutine((StartStartRageDurationCoroutine()));
    }

    public void StopCleaningAnimation()
    {
        if (toolAnimatorController.GetAnimator().GetBool(toolAnimatorController.GetCleaningName()))
                toolAnimatorController.TriggerParticularAction(cleaningManager.GetToolSelector().toolAnimatorController.GetCleaningName(), false);
    }

    private IEnumerator StartStartRageDurationCoroutine()
    {
        yield return new WaitForSeconds(rageOutcome.duration);
        SanityManager.Instance.isRageActive = false;

        if (toolAnimatorController.GetAnimator().GetBool(toolAnimatorController.GetCleaningRageName()))
            toolAnimatorController.TriggerParticularAction(cleaningManager.GetToolSelector().toolAnimatorController.GetCleaningRageName(), false);

        playerController.ModifyPlayerSpeed(3);
        playerController.ModifyPlayerFootstepAudioSpeed(0.5f);

        PickUpDrop pickUpDrop = playerController.gameObject.GetComponent<PickUpDrop>();
        
        if (pickUpDrop)
            pickUpDrop.ReduceCurrentThrowingForce(1);

        volumeController.EndVolumeVFX();
    }
}