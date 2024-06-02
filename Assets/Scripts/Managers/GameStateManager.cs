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

    [SerializeField] private List<DisposableObject> disposableObjects;
    public List<DisposableObject> DisposableObjects => disposableObjects;

    public SceneTimer sceneTimer;

    private IGameState currentState;
    private Dictionary<string, IGameState> states;

    public bool isTimerCompleted;
    public int cleanedCount = 0;
    public int disposedCount = 0;

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
        cleanedCount++;
    }
    
    public void OnObjectDisposed()
    {
        disposedCount++;
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

        foreach (var disposableObject in gameStateManager.DisposableObjects)
        {
            disposableObject.Disposed += gameStateManager.OnObjectDisposed;
        }

        gameStateManager.cleanedCount = 0;
        gameStateManager.disposedCount = 0;

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
        if (gameStateManager.cleanedCount == gameStateManager.CleanableObjects.Count && gameStateManager.disposedCount == gameStateManager.DisposableObjects.Count)
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
        foreach (var cleanableObject in gameStateManager.CleanableObjects)
        {
            cleanableObject.Cleaned -= gameStateManager.OnObjectCleaned;
        }

        foreach (var disposableObject in gameStateManager.DisposableObjects)
        {
            disposableObject.Disposed -= gameStateManager.OnObjectDisposed;
        }

        gameStateManager.CleanableObjects.Clear();
        gameStateManager.DisposableObjects.Clear();
    }

    public void UpdateState(GameStateManager gameStateManager)
    {

    }

    public void ExitState(GameStateManager gameStateManager)
    {
        
    }
}
