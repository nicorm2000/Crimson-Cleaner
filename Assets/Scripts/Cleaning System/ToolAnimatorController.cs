using System.Collections;
using UnityEngine;

public class ToolAnimatorController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Animator animator = null;
    [SerializeField] private CleaningManager cleaningManager = null;

    [Header("Mop")]
    [SerializeField] private string mopToSpongeTransitionName;
    [SerializeField] private string mopToBinTransitionName;
    [SerializeField] private string mopToHandsTransitionName;
    [SerializeField] private string mopToTabletTransitionName;

    [SerializeField] private string mopCleanName;
    [SerializeField] private string mopBucketName;

    [Header("Sponge")]
    [SerializeField] private string spongeToMopTransitionName;
    [SerializeField] private string spongeToBinTransitionName;
    [SerializeField] private string spongeToHandsTransitionName;
    [SerializeField] private string spongeToTabletTransitionName;

    [SerializeField] private string spongeCleanName;
    [SerializeField] private string spongeBucketName;

    [Header("Hands")]
    [SerializeField] private string handsToMopTransitionName;
    [SerializeField] private string handsToBinTransitionName;
    [SerializeField] private string handsToSpongeTransitionName;
    [SerializeField] private string handsToTabletTransitionName;

    [SerializeField] private string handsPickupName;
    [SerializeField] private string handsThrowName;
    [SerializeField] private string handsDropName;
    [SerializeField] private string handsGrabName;

    [Header("Bin")]
    [SerializeField] private string binToMopTransitionName;
    [SerializeField] private string binToSpongeTransitionName;
    [SerializeField] private string binToHandsTransitionName;
    [SerializeField] private string binToTabletTransitionName;

    [SerializeField] private string binRemoveName;
    [SerializeField] private string binCleanName;

    [Header("Tablet")]
    [SerializeField] private string tabletToHandsTransitionName;
    [SerializeField] private string tabletToMopTransitionName;
    [SerializeField] private string tabletToSpongeTransitionName;
    [SerializeField] private string tabletToBinTransitionName;

    [Header("Swap")]
    [SerializeField] private string mopOffName;
    [SerializeField] private string spongeOffName;
    [SerializeField] private string handsOffName;
    [SerializeField] private string binOffName;
    [SerializeField] private string tabletOffName;

    private string previousOffBool = "";

    public void HandleOffTool(int previousToolIndex)
    {
        string offToolTransition = GetOffToolTransitionName(previousToolIndex);

        if (!string.IsNullOrEmpty(offToolTransition))
        {
            animator.SetBool(previousOffBool, false);
            animator.SetBool(offToolTransition, true); 
            previousOffBool = offToolTransition;
        }
    }

    public void HandleToolSwitched(int currentToolIndex)
    {
        string transitionName = GetToolTransitionName(currentToolIndex);

        if (!string.IsNullOrEmpty(transitionName))
        {
            animator.SetBool(transitionName, true);
            StartCoroutine(ResetTransition(transitionName));
        }
    }

    private string GetOffToolTransitionName(int toolIndex)
    {
        return toolIndex switch
        {
            0 => handsOffName,
            1 => mopOffName,
            2 => spongeOffName,
            3 => binOffName,
            4 => tabletOffName,
            _ => null,
        };
    }

    private string GetToolTransitionName(int toolIndex)
    {
        switch (cleaningManager.GetToolSelector().GetPreviousToolIndex())
        {
            case 0: // Desde Hands
                return toolIndex switch
                {
                    1 => handsToMopTransitionName,
                    2 => handsToSpongeTransitionName,
                    3 => handsToBinTransitionName,
                    4 => handsToTabletTransitionName,
                    _ => null,
                };
            case 1: // Desde Mop
                return toolIndex switch
                {
                    0 => mopToHandsTransitionName,
                    2 => mopToSpongeTransitionName,
                    3 => mopToBinTransitionName,
                    4 => mopToTabletTransitionName,
                    _ => null,
                };
            case 2: // Desde Sponge
                return toolIndex switch
                {
                    0 => spongeToHandsTransitionName,
                    1 => spongeToMopTransitionName,
                    3 => spongeToBinTransitionName,
                    4 => spongeToTabletTransitionName,
                    _ => null,
                };
            case 3: // Desde Bin
                return toolIndex switch
                {
                    0 => binToHandsTransitionName,
                    1 => binToMopTransitionName,
                    2 => binToSpongeTransitionName,
                    4 => binToTabletTransitionName,
                    _ => null,
                };
            case 4: // Desde Tablet
                return toolIndex switch
                {
                    0 => tabletToHandsTransitionName,
                    1 => tabletToMopTransitionName,
                    2 => tabletToSpongeTransitionName,
                    3 => tabletToBinTransitionName,
                    _ => null,
                };
            default:
                return null;
        }
    }

    private IEnumerator ResetTransition(string transitionName)
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool(transitionName, false);
    }

    public Animator GetAnimator() => animator;

    public string GetMopToSpongeTransicionName() => mopToSpongeTransitionName;
    public string GetMopToBinTransicionName() => mopToBinTransitionName;
    public string GetMopToHandsTransicionName() => mopToHandsTransitionName;
    public string GetMopCleanName() => mopCleanName;
    public string GetMopBucketName() => mopBucketName;

    public string GetSpongeToMopTransicionName() => spongeToMopTransitionName;
    public string GetSpongeToBinTransicionName() => spongeToBinTransitionName;
    public string GetSpongeToHandsTransicionName() => spongeToHandsTransitionName;
    public string GetSpongeCleanName() => spongeCleanName;
    public string GetSpongeBucketName() => spongeBucketName;

    public string GetHandsToMopTransicionName() => handsToMopTransitionName;
    public string GetHandsToBinTransicionName() => handsToBinTransitionName;
    public string GetHandsToSpongeTransicionName() => handsToSpongeTransitionName;
    public string GetHandsPickupName() => handsPickupName;
    public string GetHandsThrowName() => handsThrowName;
    public string GetHandsDropName() => handsDropName;
    public string GetHandsGrabName() => handsGrabName;

    public string GetBinToMopTransicionName() => binToMopTransitionName;
    public string GetBinToSpongeTransicionName() => binToSpongeTransitionName;
    public string GetBinToHandsTransicionName() => binToHandsTransitionName;
    public string GetBinRemoveName() => binRemoveName;
    public string GetBinCleanName() => binCleanName;

    public string GetMopOffName() => mopOffName;
    public string GetSpongeOffName() => spongeOffName;
    public string GetHandsOffName() => handsOffName;
    public string GetBinOffName() => binOffName;

}

