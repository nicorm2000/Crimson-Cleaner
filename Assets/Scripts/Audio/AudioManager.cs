using System.Threading;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static bool muteSFX = false;

    /// <summary>
    /// Plays a sound using the specified audio event and game object.
    /// </summary>
    /// <param name="audioEvent">The name of the audio event to play.</param>
    public void PlaySound(string audioEvent)
    {
        if (!muteSFX)
            AkSoundEngine.PostEvent(audioEvent, gameObject);
    }

    /// <summary>
    /// Plays a sound using the specified audio event and game object.
    /// </summary>
    /// <param name="audioEvent">The name of the audio event to play.</param>
    /// <param name="gameObject">The game object associated with the sound.</param>
    public void PlaySound(string audioEvent, GameObject gameObject)
    {
        if (!muteSFX)
            AkSoundEngine.PostEvent(audioEvent, gameObject);
    }

    /// <summary>
    /// Stops all currently playing sounds.
    /// </summary>
    public void StopSounds()
    {
        AkSoundEngine.StopAll();
    }

    /// <summary>
    /// Stop playing specific sound.
    /// </summary>
    public void StopSpecificSound(string audioEvent, GameObject gameObject)
    {
        AkSoundEngine.StopPlayingID(AkSoundEngine.PostEvent(audioEvent, gameObject));
    }

    public void ToggleMute()
    {
        muteSFX = !muteSFX;
    }
}