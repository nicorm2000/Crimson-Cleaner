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

    public void StartRageDuration()
    {
        StartCoroutine((StartStartRageDurationCoroutine()));
    }

    private IEnumerator StartStartRageDurationCoroutine()
    {
        yield return new WaitForSeconds(rageOutcome.duration);
        SanityManager.Instance.isRageActive = false;
        playerController.ModifyPlayerSpeed(3);
        playerController.ModifyPlayerFootstepAudioSpeed(0.5f);

        PickUpDrop pickUpDrop = playerController.gameObject.GetComponent<PickUpDrop>();
        
        if (pickUpDrop)
            pickUpDrop.ReduceCurrentThrowingForce(1);

        cleaningManager.ModifyAnimationSpeed(1);

        volumeController.EndVolumeVFX();
    }
}