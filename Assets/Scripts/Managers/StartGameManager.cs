using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    public event Action startGameFinishedEvent;

    public void InvokeStartGameFinishedEvent()
    {
        startGameFinishedEvent?.Invoke();
    }

}
