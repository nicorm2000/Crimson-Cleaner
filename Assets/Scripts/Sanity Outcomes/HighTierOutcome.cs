using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HighTierOutcome : MonoBehaviour
{
    public UnityEvent TriggerOutcome;

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        TriggerOutcome?.Invoke();
        gameObject.SetActive(false);
    }
}
