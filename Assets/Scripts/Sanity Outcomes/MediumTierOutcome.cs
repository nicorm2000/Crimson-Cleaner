using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MediumTierOutcome : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string soundEvent = null;

    public UnityEvent TriggerOutcome;
    [SerializeField] private VolumeController volumeController;
    [SerializeField] private GameObject visualObject;

    public float duration;
    public bool isActiveNow = false;
    public bool shouldVolumeLerp = false;

    public void CallAudio()
    {
        audioManager.PlaySound(soundEvent);
    }

    public void ToggleVisualObjectState(bool active)
    {
        if (visualObject)
            visualObject.SetActive(active);
    }

    public void TriggerAudio()
    {
        audioManager.PlaySound(soundEvent);
    }

    public void ToggleVolumeController(bool active)
    {
        if (active)
        {
            volumeController.gameObject.SetActive(active);
            volumeController.GetVolume().gameObject.SetActive(active);
            volumeController.StartVolumeVFX();
        }
        else
        {
            if (shouldVolumeLerp)
                volumeController.EndVolumeVFX();
            else
                volumeController.GetVolume().gameObject.SetActive(active);
        }
    }

    public void StartVolumeControllerCoroutine()
    {
        StartCoroutine(ToggleVolumeControllerCoroutine());
    }
    private IEnumerator ToggleVolumeControllerCoroutine()
    {
        yield return new WaitForSeconds(volumeController.endTogglingDuration);
        volumeController.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

}
