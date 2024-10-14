using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private MySceneManager m_SceneManager;

    public void TriggerSceneTransition(string sceneName)
    {
        m_SceneManager.LoadSceneByName(sceneName);
    }
}
