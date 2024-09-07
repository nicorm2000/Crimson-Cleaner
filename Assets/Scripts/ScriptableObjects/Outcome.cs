using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tiers
{
    Low = 0,
    Medium,
    High
}

public enum Priorities
{
    Low = 0,
    MediumLow,
    Medium,
    MediumHigh,
    High
}

[CreateAssetMenu(fileName = "new outcome", menuName = "Outcomes")]
public class Outcome : ScriptableObject
{
    public int eventID = 0;
    public Tiers tier;
    public int executionTimes = 1;
    public bool isActiveNow = false;
    public bool isStackable = false;
    public float duration = 0f;
    public Priorities priority;
    public string audioEventPlay = "";
    public string audioEventStop = "";
    public GameObject visualOutcome = null;
}
