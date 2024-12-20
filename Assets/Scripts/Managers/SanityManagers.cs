using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityManager : MonoBehaviourSingleton<SanityManager>
{
    [Header("Config")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameStateManager gameStateManager;

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
    [SerializeField] private MediumTierOutcome[] mediumTierOutcomes;
    [SerializeField] private HighTierOutcome[] highTierOutcomes;
    [SerializeField] private MediumTierOutcome humansOutcome;

    public bool isRageActive = false;
    public bool isCatatoniaActive = false;
    public bool isConcentrationActive = false;
    public bool shouldRageTrigger = false;
    public bool shouldCatatoniaTrigger = false;
    public float scalarAddition = 0f;

    private float lowTierDuration = 0f;
    private float mediumTierDuration = 0f;
    private float highTierDuration = 0f;

    private float lowTierTimer = 0f;
    private float mediumTierTimer = 0f;
    private float highTierTimer = 0f;

    public bool isHighTierActive = false;
    private bool lowTierOutcomeActive = false;
    private bool mediumTierOutcomeActive = false;

    //public bool isTabletActive = false;
    public bool isHumansOutcomeActive = false;

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
        if (!isHighTierActive && gameStateManager.GetCurrentState() is not PauseState)
        {
            IncreaseSanityBars();
        }

        CheckAndTriggerOutcomes();

        // Quick rage test
        //if (shouldRageTrigger && !isRageActive)
        //    highTierOutcomes[0].gameObject.SetActive(true);

        // Quick catatonia test
        //if (shouldCatatoniaTrigger && !isCatatoniaActive)
        //    highTierOutcomes[1].gameObject.SetActive(true);

        //if (Input.GetKeyDown(KeyCode.J))
        //    highTierOutcomes[1].gameObject.SetActive(true);
        //
        //if (Input.GetKeyDown(KeyCode.I))
        //    highTierOutcomes[2].gameObject.SetActive(true);

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

        if (mediumTierTimer >= mediumTierDuration && !lowTierOutcomeActive && !mediumTierOutcomeActive && !isHighTierActive)
        {
            //TriggerOutcome(Tiers.Medium, mediumTierOutcomes, ref mediumTierOutcomeActive);

            int randomIndex = Random.Range(0, mediumTierOutcomes.Length);
            mediumTierOutcomes[randomIndex].gameObject.SetActive(true);
            StartCoroutine(StartOutcome(mediumTierOutcomes[randomIndex]));
        }

        if (highTierTimer >= highTierDuration && !isHighTierActive)
        {
            if (!lowTierOutcomeActive && !mediumTierOutcomeActive)
            {
                // Proper high tier tests
                int randomIndex = Random.Range(0, highTierOutcomes.Length);
                highTierOutcomes[randomIndex].gameObject.SetActive(true);
                StartCoroutine(StartOutcome(highTierOutcomes[randomIndex]));
            }
            else
            {
                Debug.LogWarning("Esperando a que los outcomes de los tiers bajo o medio terminen antes de iniciar el high tier (Brote).");
            }
        }
    }

    private IEnumerator StartOutcome(MediumTierOutcome outcome)
    {
        //isTabletActive = gameStateManager.GetTablet().activeSelf;

        if (outcome == humansOutcome)
            isHumansOutcomeActive = true;

        outcome.gameObject.SetActive(true);
        outcome.TriggerOutcome?.Invoke();
        outcome.isActiveNow = true;



        //Debug.Log(outcome.tier + ": " + outcome.eventID + " started");

        mediumTierOutcomeActive = true;


        yield return new WaitForSeconds(outcome.duration);

        mediumTierOutcomeActive = false;

        lowTierDuration = Random.Range(lowTierDurationMin, lowTierDurationMax);
        mediumTierDuration = Random.Range(mediumTierDurationMin, mediumTierDurationMax);
        highTierDuration = Random.Range(highTierDurationMin, highTierDurationMax);

        lowTierTimer = 0f;
        mediumTierTimer = 0f;

        outcome.isActiveNow = false;

        if (outcome == humansOutcome)
            isHumansOutcomeActive = false;

        outcome.ToggleVolumeController(false);
        
        if(outcome.GetVisualObject())
        outcome.ToggleVisualObjectState(false);
        else if (outcome.GetVisualObjects() != null)
        {
            foreach(var obj in outcome.GetVisualObjects())
                obj.SetActive(false);
        }

        outcome.StartVolumeControllerCoroutine();
        //Debug.Log(outcome.tier + ": " + outcome.eventID + " : finished");
    }

    private IEnumerator StartOutcome(HighTierOutcome outcome)
    {
        outcome.isActiveNow = true;

        isHighTierActive = true;

        //Debug.Log(outcome.tier + ": " + outcome.eventID + " started");

        yield return new WaitForSeconds(outcome.duration);

        isHighTierActive = false;

        lowTierDuration = Random.Range(lowTierDurationMin, lowTierDurationMax);
        mediumTierDuration = Random.Range(mediumTierDurationMin, mediumTierDurationMax);
        highTierDuration = Random.Range(highTierDurationMin, highTierDurationMax);

        lowTierTimer = 0f;
        mediumTierTimer = 0f;
        highTierTimer = 0f;

        outcome.isActiveNow = false;
        //Debug.Log(outcome.tier + ": " + outcome.eventID + " : finished");
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
                {
                    Debug.Log("Nombre del audio start:" + outcome.audioEventPlay);
                    audioManager.PlaySound(outcome.audioEventPlay);
                }
                else
                {
                    Debug.Log("Start Audio is null");
                }
                break;
            case Tiers.Medium:
                mediumTierOutcomeActive = true;
                //newVisualEffect = Instantiate(outcome.visualOutcome);
                outcome.visualOutcome.SetActive(true);
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
                if (!string.IsNullOrWhiteSpace(outcome.audioEventStop))
                {
                    Debug.Log("Nombre del audio end:" + outcome.audioEventStop);
                    audioManager.PlaySound(outcome.audioEventStop);
                }
                else
                {
                    Debug.Log("Stop Audio is null");
                }
                break;
            case Tiers.Medium:
                mediumTierOutcomeActive = false;
                mediumTierDuration =  Random.Range(mediumTierDurationMin, mediumTierDurationMax);
                mediumTierTimer = 0f;
                //Destroy(newVisualEffect.gameObject);
                outcome.visualOutcome.SetActive(false);
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

    public void TriggerConcentrationState()
    {
        isConcentrationActive = true;
    }
}