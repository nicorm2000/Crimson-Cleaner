using UnityEngine;

public class ToolAnimatorController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Animator animator = null;

    [Header("Mop")]
    [SerializeField] private string mopToSpongeTransitionName;
    [SerializeField] private string mopToBinTransitionName;
    [SerializeField] private string mopToHandsTransitionName;

    [SerializeField] private string mopCleanName;
    [SerializeField] private string mopBucketName;

    [Header("Sponge")]
    [SerializeField] private string spongeToMopTransitionName;
    [SerializeField] private string spongeToBinTransitionName;
    [SerializeField] private string spongeToHandsTransitionName;

    [SerializeField] private string spongeCleanName;
    [SerializeField] private string spongeBucketName;

    [Header("Hands")]
    [SerializeField] private string handsToMopTransitionName;
    [SerializeField] private string handsToBinTransitionName;
    [SerializeField] private string handsToSpongeTransitionName;

    [SerializeField] private string handsPickupName;
    [SerializeField] private string handsThrowName;
    [SerializeField] private string handsDropName;
    [SerializeField] private string handsGrabName;

    [Header("Bin")]
    [SerializeField] private string binToMopTransitionName;
    [SerializeField] private string binToSpongeTransitionName;
    [SerializeField] private string binToHandsTransitionName;

    [SerializeField] private string binRemoveName;
    [SerializeField] private string binCleanName;

    [Header("Swap")]
    [SerializeField] private string mopOffName;
    [SerializeField] private string spongeOffName;
    [SerializeField] private string handsOffName;
    [SerializeField] private string binOffName;

    private string previousBool = "";

    public void TransitionStateByBool(string transitionName)
    {
        animator.SetBool(previousBool, false);

        animator.SetBool(transitionName,true);

        previousBool = animator.GetBool(transitionName).ToString();
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

