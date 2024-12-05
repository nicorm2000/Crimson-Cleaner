using UnityEngine;

public class AudioTest : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string audioEvent;
    [SerializeField] private string stopAudioEvent;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            if (audioEvent != null)
                audioManager.PlaySound(audioEvent, gameObject);
        if (Input.GetKeyDown(KeyCode.K))
            if (stopAudioEvent != null)
                audioManager.PlaySound(stopAudioEvent, gameObject);
    }
}