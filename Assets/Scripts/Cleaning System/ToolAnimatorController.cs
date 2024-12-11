using System.Collections;
using System.Collections.Generic;
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

    [Header("Sponge")]
    [SerializeField] private string spongeToMopTransitionName;
    [SerializeField] private string spongeToBinTransitionName;
    [SerializeField] private string spongeToHandsTransitionName;
    [SerializeField] private string spongeToTabletTransitionName;

    [Header("Hands")]
    [SerializeField] private string handsToMopTransitionName;
    [SerializeField] private string handsToBinTransitionName;
    [SerializeField] private string handsToSpongeTransitionName;
    [SerializeField] private string handsToTabletTransitionName;

    [SerializeField] private string handsPickupName;
    [SerializeField] private string handsDropName;
    [SerializeField] private string handsGrabName;
    [SerializeField] private string handsThrowName;

    [Header("Bin")]
    [SerializeField] private string binToMopTransitionName;
    [SerializeField] private string binToSpongeTransitionName;
    [SerializeField] private string binToHandsTransitionName;
    [SerializeField] private string binToTabletTransitionName;
    [SerializeField] private string binCleanName;
    [SerializeField] private string binRemoveName;

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

    [Header("Particular Actions")]
    [SerializeField] private string cleaning;
    [SerializeField] private string cleaningRage;
    [SerializeField] private string bucket;
    [SerializeField] private string pc;
    [SerializeField] private string grabKeysName;
    [SerializeField] private string petCatName;
    [SerializeField] private string sitDownName;
    [SerializeField] private string startCarTriggerName;

    public bool canTriggerAction = true;

    private int previousOffBoolHash = 0;
    private Dictionary<(int, int), int> toolTransitionHashes;
    private Dictionary<int, int> offToolHashes;

    [SerializeField] private List<string> particularActions = new List<string>();

    private Coroutine triggerActionCoroutine;

    private void Awake()
    {
        InitializeTransitionHashes();
    }

    private void InitializeTransitionHashes()
    {
        toolTransitionHashes = new Dictionary<(int, int), int>
        {
            // Desde Hands
            { (0, 1), Animator.StringToHash(handsToMopTransitionName) },
            { (0, 2), Animator.StringToHash(handsToSpongeTransitionName) },
            { (0, 3), Animator.StringToHash(handsToBinTransitionName) },
            { (0, 4), Animator.StringToHash(handsToTabletTransitionName) },

            // Desde Mop
            { (1, 0), Animator.StringToHash(mopToHandsTransitionName) },
            { (1, 2), Animator.StringToHash(mopToSpongeTransitionName) },
            { (1, 3), Animator.StringToHash(mopToBinTransitionName) },
            { (1, 4), Animator.StringToHash(mopToTabletTransitionName) },

            // Desde Sponge
            { (2, 0), Animator.StringToHash(spongeToHandsTransitionName) },
            { (2, 1), Animator.StringToHash(spongeToMopTransitionName) },
            { (2, 3), Animator.StringToHash(spongeToBinTransitionName) },
            { (2, 4), Animator.StringToHash(spongeToTabletTransitionName) },

            // Desde Bin
            { (3, 0), Animator.StringToHash(binToHandsTransitionName) },
            { (3, 1), Animator.StringToHash(binToMopTransitionName) },
            { (3, 2), Animator.StringToHash(binToSpongeTransitionName) },
            { (3, 4), Animator.StringToHash(binToTabletTransitionName) },

            // Desde Tablet
            { (4, 0), Animator.StringToHash(tabletToHandsTransitionName) },
            { (4, 1), Animator.StringToHash(tabletToMopTransitionName) },
            { (4, 2), Animator.StringToHash(tabletToSpongeTransitionName) },
            { (4, 3), Animator.StringToHash(tabletToBinTransitionName) }
        };

        offToolHashes = new Dictionary<int, int>
        {
            { 0, Animator.StringToHash(handsOffName) },
            { 1, Animator.StringToHash(mopOffName) },
            { 2, Animator.StringToHash(spongeOffName) },
            { 3, Animator.StringToHash(binOffName) },
            { 4, Animator.StringToHash(tabletOffName) }
        };
    }

    public void HandleOffTool(int previousToolIndex)
    {
        if (offToolHashes.TryGetValue(previousToolIndex, out int offToolHash))
        {
            if (previousOffBoolHash != 0)
                animator.SetBool(previousOffBoolHash, false);

            animator.SetBool(offToolHash, true);
            previousOffBoolHash = offToolHash;
        }
    }

    public void HandleToolSwitched(int currentToolIndex)
    {
        int previousToolIndex = cleaningManager.GetToolSelector().GetPreviousToolIndex();

        if (toolTransitionHashes.TryGetValue((previousToolIndex, currentToolIndex), out int transitionHash))
        {
            animator.SetBool(transitionHash, true);
            StartCoroutine(ResetTransition(transitionHash));
            StartCoroutine(WaitForToolSwitchEnd());
        }
    }

    public void TriggerParticularAction(string actionName, bool active)
    {
        if (animator)
        {
            animator.SetBool(actionName, active);
        }
    }

    public void TriggerParticularAction(string triggerName)
    {
        if (animator)
        {
            animator.SetTrigger(triggerName);
        }
    }

    private IEnumerator WaitForToolSwitchEnd()
    {
        canTriggerAction = false;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        canTriggerAction = true; 
    }

    private IEnumerator ResetTransition(int transitionHash)
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool(transitionHash, false);
    }

    public void TriggerActionCoroutine(float animationDuration)
    {
        if (triggerActionCoroutine != null)
        {
            StopCoroutine(triggerActionCoroutine);
        }
        triggerActionCoroutine = StartCoroutine(ActionCoroutine(animationDuration));
    }

    private IEnumerator ActionCoroutine(float animationDuration)
    {
        canTriggerAction = false;
        yield return new WaitForSeconds(animationDuration);
        canTriggerAction = true;
    }

    public Animator GetAnimator() => animator;

    public string GetCleaningName() => cleaning;
    public string GetCleaningRageName() => cleaningRage;
    public string GetBucketName() => bucket;

    public string GetHandsPickupName() => handsPickupName;
    public string GetHandsDropName() => handsDropName;
    public string GetHandsGrabName() => handsGrabName;
    public string GetHandsThrowName() => handsThrowName;

    public string GetBinCleanName() => binCleanName;
    public string GetBinRemoveName() => binRemoveName;
    public string GetPCName() => pc;
    public string GetGrabKeysName() => grabKeysName;
    public string GetPetCatName() => petCatName;
    public string GetSitDownName() => sitDownName;
    public string GetStartCarTriggerName() => startCarTriggerName;


}
