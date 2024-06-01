using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IGameState
{
    void EnterState(GameStateManager gameStateManager);
    void UpdateState(GameStateManager gameStateManager);
    void ExitState(GameStateManager gameStateManager);
}

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private List<Clean> cleanableObjects;
    public List<Clean> CleanableObjects => cleanableObjects;
    public SceneTimer sceneTimer;

    private IGameState currentState;
    private Dictionary<string, IGameState> states;

    public bool isTimerCompleted;

    public event Action GameLost;
    public event Action GameWon;


    private void Start()
    {
        states = new Dictionary<string, IGameState>
        {
            { "Init", new InitializationState() },
            { "GamePlay", new GamePlayState() },
            { "Win", new WinState() },
            { "Lose", new LoseState() },
            { "Pause", new PauseState() },
            { "DeInit", new DeInitializationState() }
        };

        TransitionToState("Init");
    }

    public void Update()
    {
        currentState?.UpdateState(this);
    }


    public void TransitionToState(string newStateName)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        if (states.ContainsKey(newStateName))
        {
            currentState = states[newStateName];
            currentState.EnterState(this);
        }
    }

    public void OnObjectCleaned()
    {
        cleanableObjects.RemoveAll(obj => obj == null || obj.IsCleaned);
    }

    public void OnTimerFinished()
    {
        isTimerCompleted = true;
    }

    public void TriggerLoseEvent()
    {
        GameLost?.Invoke();
    }
    public void TriggerWinEvent()
    {
        GameWon?.Invoke();
    }
}


public class InitializationState : IGameState
{
    public void EnterState(GameStateManager gameStateManager)
    {
        foreach (var cleanableObject in gameStateManager.CleanableObjects)
        {
            cleanableObject.Cleaned += gameStateManager.OnObjectCleaned;
        }

        gameStateManager.isTimerCompleted = false;
        gameStateManager.sceneTimer.StartTimer();

        gameStateManager.TransitionToState("GamePlay");
    }

    public void UpdateState(GameStateManager gameStateManager)
    {
        
    }

    public void ExitState(GameStateManager gameStateManager)
    {
        
    }
}

public class GamePlayState : IGameState
{

    public void EnterState(GameStateManager gameStateManager)
    {

    }

    public void UpdateState(GameStateManager gameStateManager)
    {
        if (gameStateManager.CleanableObjects.Count == 0)
        {
            gameStateManager.TransitionToState("Win");
            return;
        }

        if (gameStateManager.isTimerCompleted)
        {
            gameStateManager.TransitionToState("Lose");
            return;
        }
    }

    public void ExitState(GameStateManager gameStateManager)
    {

    }
}

public class WinState : IGameState
{
    public void EnterState(GameStateManager gameStateManager)
    {
        gameStateManager.sceneTimer.ShowCursor();

        Debug.Log("Player won the game");
        gameStateManager.TriggerWinEvent();

        gameStateManager.TransitionToState("DeInit");
    }

    public void UpdateState(GameStateManager gameStateManager)
    {
        
    }

    public void ExitState(GameStateManager gameStateManager)
    {
       
    }
}

public class LoseState : IGameState
{
    public void EnterState(GameStateManager gameStateManager)
    {
        gameStateManager.sceneTimer.ShowCursor();

        Debug.Log("Player lost the game");
        gameStateManager.TriggerLoseEvent();

        gameStateManager.TransitionToState("DeInit");
    }

    public void UpdateState(GameStateManager gameStateManager)
    {
        
    }

    public void ExitState(GameStateManager gameStateManager)
    {
        
    }
}

public class PauseState : IGameState
{
    public void EnterState(GameStateManager gameStateManager)
    {
        
    }

    public void UpdateState(GameStateManager gameStateManager)
    {
        
    }

    public void ExitState(GameStateManager gameStateManager)
    {
        
    }
}

public class DeInitializationState : IGameState
{
    public void EnterState(GameStateManager gameStateManager)
    {
        
    }

    public void UpdateState(GameStateManager gameStateManager)
    {

    }

    public void ExitState(GameStateManager gameStateManager)
    {
        foreach (var cleanableObject in gameStateManager.CleanableObjects)
        {
            cleanableObject.Cleaned -= gameStateManager.OnObjectCleaned;
        }
    }
}
