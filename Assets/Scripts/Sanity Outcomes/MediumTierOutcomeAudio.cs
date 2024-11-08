using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumTierOutcomeAudio : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string soundEvent = null;

    public void CallAudio()
    {
        audioManager.PlaySound(soundEvent);
    }
}
