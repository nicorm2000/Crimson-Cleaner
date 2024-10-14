using IE.RichFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainMenuUIManager : MonoBehaviour
{
    public PCCanvasController pCCanvasController;
    public InputManager inputManager;

    public GameObject pcCanvas;
    public GameObject mainCanvas;

    [SerializeField] private Volume sceneVolume = null;
    [SerializeField] private float durationON = 0.5f;
    [SerializeField] private float durationOFF = 0.5f;
    [SerializeField] private float startValueOFF = 0.0f;
    [SerializeField] private float endValueON = 0.3f;

    private void OnEnable()
    {
        inputManager.PauseEvent += OnPauseEvent;
    }

    private void OnDisable()
    {
        inputManager.PauseEvent -= OnPauseEvent;
    }

    public void ToggleCanvas(GameObject canvas, bool active)
    {
        canvas.SetActive(active);
    }

    private void OnPauseEvent()
    {
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
}
