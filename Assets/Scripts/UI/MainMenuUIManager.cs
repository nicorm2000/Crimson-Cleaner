using IE.RichFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("Config")]
    public PCCanvasController pCCanvasController;
    public InputManager inputManager;
    public Van van;

    [Header("UI")]
    public GameObject mainCanvas;
    [SerializeField] private Image missingKeyImageWarning;
    [SerializeField] private float missingKeyErrorDuration;

    [Header("Volume")]
    [SerializeField] private Volume sceneVolume = null;
    [SerializeField] private float durationON = 0.5f;
    [SerializeField] private float durationOFF = 0.5f;
    [SerializeField] private float startValueOFF = 0.0f;
    [SerializeField] private float endValueON = 0.3f;

    private Coroutine missingKeyWarningCoroutine;

    private void OnEnable()
    {
        inputManager.PauseEvent += OnPauseEvent;
        van.ungrabbedKey += () => OnMissingKeyWarning(ref missingKeyWarningCoroutine, missingKeyImageWarning, missingKeyErrorDuration);
    }

    private void OnDisable()
    {
        inputManager.PauseEvent -= OnPauseEvent;
        van.ungrabbedKey -= () => OnMissingKeyWarning(ref missingKeyWarningCoroutine, missingKeyImageWarning, missingKeyErrorDuration);
    }

    public void ToggleCanvas(GameObject canvas, bool active)
    {
        canvas.SetActive(active);
    }

    private void OnPauseEvent()
    {
        if (sceneVolume.profile.TryGet(out SimpleGaussianBlur simpleGaussianBlur))
        {
            if (simpleGaussianBlur.intensity.value != 0f)
                StopSimpleGaussianBlurState();
        }

        if (pCCanvasController.isPlayerOnPC)
            pCCanvasController.ShutDownPC();
    }

    public void StartSimpleGaussianBlurState()
    {
        SimpleGaussianBlurState(durationON, startValueOFF, endValueON);
    }

    public void StopSimpleGaussianBlurState()
    {
        SimpleGaussianBlurState(durationOFF, endValueON, startValueOFF);
    }

    private void SimpleGaussianBlurState(float duration, float startValue, float endValue)
    {
        if (sceneVolume.profile.TryGet(out SimpleGaussianBlur simpleGaussianBlur))
            StartCoroutine(UpdateSimpleGaussianBlur(simpleGaussianBlur, duration, startValue, endValue));
    }

    private IEnumerator UpdateSimpleGaussianBlur(SimpleGaussianBlur simpleGaussianBlur, float duration, float startValue, float endValue)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newIntensity = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            simpleGaussianBlur.intensity.value = newIntensity;
            yield return null;
        }

        simpleGaussianBlur.intensity.value = endValue;
    }

    private void OnMissingKeyWarning(ref Coroutine warningCoroutine, Image missingKeyWarning, float warningDuration)
    {
        if (warningCoroutine != null)
        {
            StopCoroutine(warningCoroutine);
        }
        warningCoroutine = StartCoroutine(ShowWarning(missingKeyWarning, warningDuration));
    }

    private IEnumerator ShowWarning(Image warningImage, float duration)
    {
        warningImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        warningImage.gameObject.SetActive(false);
    }
}
