using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class WarningManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject gameTitle = null;
    [SerializeField] private Volume sceneVolume = null;

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
        gameTitle.SetActive(true);
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
        StartCoroutine(SetIsReady(duration));;
    }

    private IEnumerator SetIsReady(float duration)
    {
        if (!AudioManager.muteSFX)
            audioManager.PlaySound(gameIntro);
        yield return new WaitForSeconds(duration);
        isReady = true;
    }
}