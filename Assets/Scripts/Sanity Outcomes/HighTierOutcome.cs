using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HighTierOutcome : MonoBehaviour
{
    public UnityEvent TriggerOutcome;
    public float duration;
    public bool isActiveNow = false;

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        TriggerOutcome?.Invoke();
        StartCoroutine(SetHighTierActive());
    }

    private IEnumerator SetHighTierActive()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}