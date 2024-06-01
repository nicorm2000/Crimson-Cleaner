using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameStateManager gameStateManager;

    private void Awake()
    {
        gameStateManager = GetComponent<GameStateManager>();
    }

    private void Update()
    {
        gameStateManager.Update();
    }
}
