using UnityEngine;

public class Cockroaches : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string cockroachEvent = null;

    public void CallCockroachAudio()
    {
        audioManager.PlaySound(cockroachEvent);
    }
}