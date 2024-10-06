using IE.RichFX;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Volume volume;

    public float startTogglingDuration = 1f;
    public float startTogglingDurationVFX = 1f;
    public float endTogglingDuration = 1f;

    public void StartVolumeVFX()
    {
        volume.gameObject.SetActive(true);
        StartCoroutine(ToggleVolumeVFX(0f, 1f, startTogglingDuration));
    }

    public void EndVolumeVFX()
    {
        StartCoroutine(ToggleVolumeVFX(1f, 0f, endTogglingDuration));
        StartCoroutine(EndRageVFXCoroutine(endTogglingDuration));
    }

    public IEnumerator ToggleVolumeVFX(float min, float max, float duration)
    {
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(t / duration);
            volume.weight = Mathf.Lerp(min, max, normalizedTime);
            yield return null;
        }
        volume.weight = max;
    }

    private IEnumerator ToggleBlurVFX(float min, float max, float duration)
    {
        float t = 0.0f;
        if (volume.profile.TryGet(out RadialBlur radialBlur))
        {
            while (t < duration)
            {
                t += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(t / duration);
                radialBlur.intensity.value = Mathf.Lerp(min, max, normalizedTime);
                yield return null;
            }
            radialBlur.intensity.value = max;
        }
        else
        {
            Debug.Log("No radial blur!");
        }
    }

    private IEnumerator EndRageVFXCoroutine(float endDuration)
    {
        yield return new WaitForSeconds(endDuration);
        volume.gameObject.SetActive(false);
    }

    public void BlurVolume()
    {
        StartCoroutine(ToggleBlurVFX(0, 0.075f, startTogglingDuration));
    }
}
