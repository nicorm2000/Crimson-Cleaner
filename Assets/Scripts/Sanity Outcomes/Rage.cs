using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : MonoBehaviour
{
    public HighTierOutcome rageOutcome;
    public PlayerController playerController;
    public void StartRageDuration()
    {
        StartCoroutine((StartStartRageDurationCoroutine()));
    }

    private IEnumerator StartStartRageDurationCoroutine()
    {
        yield return new WaitForSeconds(rageOutcome.duration);
        SanityManager.Instance.isRageActive = false;
        playerController.ModifyPlayerSpeed(3);
    }

}