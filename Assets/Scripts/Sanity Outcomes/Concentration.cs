using IE.RichFX;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Concentration : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private Animator animator;
    [SerializeField] private int blinkingAmmount;
    [SerializeField] private float minBlinkingInterval;
    [SerializeField] private float maxBlinkingInterval;
    [SerializeField] private float blinkDuration;
    [SerializeField] private string idleClosedName;

    [Header("Blur")]
    [SerializeField] private Volume playerVolume = null;
    [SerializeField,Range(0,1)] private float blurMax = 0.5f;

    private int counter = 0;
    private SimpleGaussianBlur newSimpleGaussianBlur;

    private Coroutine coroutine = null;

    private void Start()
    {
        if (playerVolume.profile.TryGet(out SimpleGaussianBlur simpleGaussianBlur))
            newSimpleGaussianBlur = simpleGaussianBlur;

        //TriggerConstantBlinking();
    }

    public void TriggerConstantBlinking()
    {
        coroutine ??= StartCoroutine(Blink(blinkDuration));
    }

    private IEnumerator Blink(float duration)
    {
        if (newSimpleGaussianBlur != null)
            newSimpleGaussianBlur.intensity.value = 0f;

        float elapsedTime = 0f;

        if (newSimpleGaussianBlur != null)
        {
            while (elapsedTime < duration)
            {
                newSimpleGaussianBlur.intensity.value = Mathf.Lerp(0f, blurMax, elapsedTime / duration);
                elapsedTime += Time.deltaTime;

                if (elapsedTime > duration / 2)
                    animator.SetBool(idleClosedName, true);
                yield return null; 
            }
            newSimpleGaussianBlur.intensity.value = blurMax;
        }

        yield return new WaitForSeconds(duration);
        counter++;

        animator.SetBool(idleClosedName, false);

        elapsedTime = 0f;
        if (newSimpleGaussianBlur != null)
        {
            while (elapsedTime < duration)
            {
                newSimpleGaussianBlur.intensity.value = Mathf.Lerp(blurMax, 0f, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null; 
            }
            newSimpleGaussianBlur.intensity.value = 0f;
        }

        float random = Random.Range(minBlinkingInterval, maxBlinkingInterval);
        Debug.Log(random);
        yield return new WaitForSeconds(random);

        coroutine = null;

        if (counter < blinkingAmmount)
        {
            TriggerConstantBlinking();
        }
        else
        {
            gameStateManager.SetDefaultLayer();
            SanityManager.Instance.isConcentrationActive = false;
        }
    }
}
