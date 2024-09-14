using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityManager : MonoBehaviourSingleton<SanityManager>
{
    [Header("Config")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private HighTierOutcome[] highTierOutcomes;

    [Header("Timers")]
    [SerializeField] private float lowTierDurationMin = 0f;
    [SerializeField] private float lowTierDurationMax = 0f;
    [SerializeField] private float mediumTierDurationMin = 0f;
    [SerializeField] private float mediumTierDurationMax = 0f;
    [SerializeField] private float highTierDurationMin = 0f;
    [SerializeField] private float highTierDurationMax = 0f;

    [Header("Increment Sanity scalers")]
    [Header("Clean Action")]
    [SerializeField] private float cleanScaler = 1f;
    public float CleanScaler => cleanScaler;
    
    [Header("Wash Tool")]
    [SerializeField] private float grabObjectScaler = 1f;
    public float GrabObjectScaler => grabObjectScaler;

    [Header("Decrement Sanity scalers")]
    [Header("Drop Dirty Bucket")]
    [SerializeField] private float dropBucketScaler= 1f;
    public float DropBucketScaler => dropBucketScaler;

    [Header("Dispose Object")]
    [SerializeField] private float burnObjectScaler = 1f;
    public float BurnObjectScaler => burnObjectScaler;

    [Header("Wash Tool")]
    [SerializeField] private float washToolScaler = 1f;
    public float WashToolScaler => washToolScaler;

    [Header("Outcomes")]
    [SerializeField] private Outcome[] lowTierOutcomes;
    [SerializeField] private Outcome[] mediumTierOutcomes;
    //[SerializeField] private Outcome[] highTierOutcomes;

    public bool isRageActive = false;
    public bool isCatatoniaActive = false;
    public bool shouldRageTrigger = false;
    public bool shouldCatatoniaTrigger = false;
    public float scalarAddition = 0f;

    private float lowTierDuration = 0f;
    private float mediumTierDuration = 0f;
    private float highTierDuration = 0f;

    private float lowTierTimer = 0f;
    private float mediumTierTimer = 0f;
    private float highTierTimer = 0f;

    private bool isHighTierActive = false;
    private bool lowTierOutcomeActive = false;
    private bool mediumTierOutcomeActive = false;


    private GameObject newVisualEffect;

    private void Awake()
    {
        lowTierDuration = Random.Range(lowTierDurationMin, lowTierDurationMax);
        mediumTierDuration = Random.Range(mediumTierDurationMin, mediumTierDurationMax);
        highTierDuration = Random.Range(highTierDurationMin, highTierDurationMax);

        foreach (var lowTier in lowTierOutcomes) lowTier.isActiveNow = false;
        foreach (var mediumTier in mediumTierOutcomes) mediumTier.isActiveNow = false;
        //foreach (var highTier in highTierOutcomes) highTier.isActiveNow = false;
    }

    private void Update()
    {
        if (!isHighTierActive)
        {
            IncreaseSanityBars();
        }

        CheckAndTriggerOutcomes();

        // Quick rage test
        //if (shouldRageTrigger && !isRageActive)
        //    highTierOutcomes[0].gameObject.SetActive(true);

        // Quick catatonia test
        if (shouldCatatoniaTrigger && !isCatatoniaActive)
            highTierOutcomes[1].gameObject.SetActive(true);

        //if (Input.GetKeyDown(KeyCode.J))
        //    highTierOutcomes[1].gameObject.SetActive(true);

        //Debug.Log("High tier: " + highTierTimer);
    }

    private void IncreaseSanityBars()
    {
        if (!lowTierOutcomeActive && !isHighTierActive)
            lowTierTimer += Time.deltaTime  + scalarAddition;

        if (!mediumTierOutcomeActive && !isHighTierActive)
            mediumTierTimer += Time.deltaTime  + scalarAddition;

        if (!isHighTierActive)
            highTierTimer += Time.deltaTime  + scalarAddition;
    }

    private void CheckAndTriggerOutcomes()
    {
        if (lowTierTimer >= lowTierDuration && !mediumTierOutcomeActive && !isHighTierActive)
        {
            TriggerOutcome(Tiers.Low, lowTierOutcomes, ref lowTierOutcomeActive);
        }

        if (mediumTierTimer >= mediumTierDuration && !lowTierOutcomeActive && !isHighTierActive)
        {
            TriggerOutcome(Tiers.Medium, mediumTierOutcomes, ref mediumTierOutcomeActive);
        }

        if (highTierTimer >= highTierDuration && !isHighTierActive)
        {
            if (!lowTierOutcomeActive && !mediumTierOutcomeActive)
            {
                // Proper high tier tests
                //int randomIndex = Random.Range(0, highTierOutcomes.Length);
                //highTierOutcomes[randomIndex].TriggerOutcome?.Invoke();

                //Quick rage test
                //highTierOutcomes[0].gameObject.SetActive(true);

                //Quick catatonia test
                //highTierOutcomes[1].gameObject.SetActive(true);

                //TriggerOutcome(Tiers.High, highTierOutcomes, ref isHighTierActive);
            }
            else
            {
                Debug.LogWarning("Esperando a que los outcomes de los tiers bajo o medio terminen antes de iniciar el high tier (Brote).");
            }
        }
    }

    private void TriggerOutcome(Tiers tier, Outcome[] outcomes, ref bool outcomeActive)
    {
        Outcome chosenOutcome = ChooseOutcome(outcomes);

        if (chosenOutcome != null)
        {
            outcomeActive = true;

            StartCoroutine(StartOutcome(chosenOutcome));
        }
    }

    private Outcome ChooseOutcome(Outcome[] outcomes)
    {
        List<Outcome> validOutcomes = new List<Outcome>();

        foreach (var outcome in outcomes)
        {
            if (CanTriggerOutcome(outcome, outcomes))
            {
                validOutcomes.Add(outcome);
            }
        }

        if (validOutcomes.Count == 0)
        {
            return null; 
        }

        validOutcomes.Sort((a, b) => b.priority.CompareTo(a.priority));

        return validOutcomes[Random.Range(0, validOutcomes.Count)];
    }

    private bool CanTriggerOutcome(Outcome outcome, Outcome[] outcomes)
    {
        foreach (var item in outcomes)
        {
            if (item.isActiveNow && !item.isStackable)
                return false;
        }

        return true;
    }

    private IEnumerator StartOutcome(Outcome outcome)
    {
        outcome.isActiveNow = true;

        switch (outcome.tier)
        {
            case Tiers.Low:
                lowTierOutcomeActive = true;
                if (outcome.audioEventPlay != null)
                    audioManager.PlaySound(outcome.audioEventPlay);
                break;
            case Tiers.Medium:
                mediumTierOutcomeActive = true;
                newVisualEffect = Instantiate(outcome.visualOutcome);
                break;
            case Tiers.High:
                isHighTierActive = true;

                break;
            default:
                break;
        }

        Debug.Log(outcome.tier + ": " + outcome.eventID + " started");

        yield return new WaitForSeconds(outcome.duration);

        switch (outcome.tier)   
        {
            case Tiers.Low:
                lowTierOutcomeActive = false;
                lowTierDuration = Random.Range(lowTierDurationMin, lowTierDurationMax);
                lowTierTimer = 0f;
                if (outcome.audioEventStop != null)
                    audioManager.PlaySound(outcome.audioEventStop);
                break;
            case Tiers.Medium:
                mediumTierOutcomeActive = false;
                mediumTierDuration =  Random.Range(mediumTierDurationMin, mediumTierDurationMax);
                mediumTierTimer = 0f;
                Destroy(newVisualEffect.gameObject);
                break;
            case Tiers.High:
                isHighTierActive = false;

                lowTierDuration = Random.Range(lowTierDurationMin, lowTierDurationMax);
                mediumTierDuration = Random.Range(mediumTierDurationMin, mediumTierDurationMax);
                highTierDuration = Random.Range(highTierDurationMin, highTierDurationMax);

                lowTierTimer = 0f;
                mediumTierTimer = 0f;
                highTierTimer = 0f;
                break;
            default:
                break;
        }

        outcome.isActiveNow = false;
        Debug.Log(outcome.tier + ": " + outcome.eventID + " : finished");
    }

    public void NewRoomSanityEvent(float scalarAddition)
    {
        ModifySanityScalar(scalarAddition);
    }

    public void ModifySanityScalar(float scalarAddition)
    {
        this.scalarAddition += scalarAddition;

        StartCoroutine(ResetScalar(scalarAddition));
    }

    private IEnumerator ResetScalar(float scalarAddition)
    {
        yield return new WaitForSeconds(0.01f);

        this.scalarAddition -= scalarAddition;
    }

    public void TriggerRageState()
    {
        isRageActive = true;
    }
    
    public void TriggerCatatoniaState()
    {
        isCatatoniaActive = true;
    }
}

