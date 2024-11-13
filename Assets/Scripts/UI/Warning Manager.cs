using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;

public class WarningManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject gameTitle = null;
    [SerializeField] private Volume sceneVolume = null;
    [SerializeField] private Image gameBG = null;
    [SerializeField] private float colorLerpDuration = 2f;
    [SerializeField] private float endRedValue = 200f;
    [SerializeField] private TextMeshProUGUI title = null;
    [SerializeField] private float alphaLerpDuration = 2f;
    [SerializeField] private TextMeshProUGUI pressAnyKey = null;
    [SerializeField] private float firstAlphaLerpDurationKey = 2f;
    [SerializeField] private float alphaLerpDurationKey = 2f;
    [SerializeField] private AnimationCurve alphaCurve;
    public Coroutine pressAnyKeyCoroutine = null;

    [Header("Config")]
    [SerializeField] private GameObject mike = null;
    [SerializeField] private GameObject blood = null;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform startTransform;
    [SerializeField] private float mikeDuration = 2f;
    [SerializeField] private AnimationCurve movementCurve;

    [Header("Breathing Effect Config")]
    [SerializeField] private float minWeight = 0.5f;
    [SerializeField] private float maxWeight = 0.8f;
    [SerializeField] private float speed = 3f;

    [Header("Audio")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string gameIntro = null;

    public bool isReady = false;


    private void OnEnable()
    {
        sceneVolume.weight = 0;
        isReady = false;
        blood.SetActive(true);
        //gameTitle.SetActive(true);
    }

    private void Update()
    {
        if (!isReady)
            return;
        float weight = Mathf.Lerp(minWeight, maxWeight, (Mathf.Sin(Time.time * speed) + 1f) / 2f);
        sceneVolume.weight = weight;
    }

    public void SetIsReadyCaller(float duration)
    {
        StartCoroutine(SetIsReady(duration)); ;
        StartCoroutine(LerpToTarget());
    }

    private IEnumerator SetIsReady(float duration)
    {
        if (!AudioManager.muteSFX)
            audioManager.PlaySound(gameIntro);
        yield return new WaitForSeconds(duration);
        isReady = true;
    }

    public IEnumerator LerpColor(float rValue, float duration)
    {
        Color initialColor = gameBG.color;
        Color targetColor = new(rValue / 255f, initialColor.g, initialColor.b, initialColor.a);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            gameBG.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);
            yield return null;
        }

        gameBG.color = targetColor;

        StartCoroutine(LerpAlpha());
    }

    private IEnumerator LerpAlpha()
    {
        Color initialColor = title.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
        float elapsedTime = 0f;

        while (elapsedTime < alphaLerpDuration)
        {
            elapsedTime += Time.deltaTime;
            title.color = Color.Lerp(initialColor, targetColor, elapsedTime / alphaLerpDuration);
            yield return null;
        }

        title.color = targetColor;

        pressAnyKey.gameObject.SetActive(true);
        StartCoroutine(InitialFadeIn());
    }

    private IEnumerator InitialFadeIn()
    {
        Color originalColor = pressAnyKey.color;
        float elapsedTime = 0f;

        while (elapsedTime < firstAlphaLerpDurationKey)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 50f / 255f, elapsedTime / firstAlphaLerpDurationKey);
            pressAnyKey.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        pressAnyKey.color = new Color(originalColor.r, originalColor.g, originalColor.b, 50f / 255f);

        pressAnyKey.GetComponent<AnyKeyMenu>().enabled = true;
        pressAnyKeyCoroutine = StartCoroutine(PulseAlpha());
    }

    private IEnumerator PulseAlpha()
    {
        Color originalColor = pressAnyKey.color;

        while (true)
        {
            float elapsedTime = 0f;

            while (elapsedTime < alphaLerpDurationKey)
            {
                elapsedTime += Time.deltaTime;
                float alphaValue = Mathf.Lerp(50f / 255f, 1f, alphaCurve.Evaluate(elapsedTime / alphaLerpDurationKey));
                pressAnyKey.color = new Color(originalColor.r, originalColor.g, originalColor.b, alphaValue);
                yield return null;
            }
        }
    }

    private IEnumerator LerpToTarget()
    {
        float elapsedTime = 0f;
        while (elapsedTime < mikeDuration)
        {
            float t = movementCurve.Evaluate(elapsedTime / mikeDuration);

            mike.transform.position = Vector3.Lerp(startTransform.position, targetTransform.position, t);
            mike.transform.rotation = Quaternion.Euler(Vector3.Lerp(startTransform.rotation.eulerAngles, targetTransform.rotation.eulerAngles, t));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mike.transform.position = targetTransform.position;
        mike.transform.rotation = targetTransform.rotation;

        StartCoroutine(LerpColor(endRedValue, colorLerpDuration));
    }

    public IEnumerator FadeAlphaToZero(float duration)
    {
        Color currentColor = pressAnyKey.color;
        float currentAlpha = currentColor.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float newAlpha = Mathf.Lerp(currentAlpha, 0f, elapsedTime / duration);

            pressAnyKey.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

            yield return null;
        }

        pressAnyKey.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);
    }
}