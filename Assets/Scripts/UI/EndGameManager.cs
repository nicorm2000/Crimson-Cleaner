using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    public void TriggerSceneTransition(string sceneName)
    {
        MySceneManager.Instance.LoadSceneByName(sceneName);
    }
}
