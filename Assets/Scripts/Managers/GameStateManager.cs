using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private List<SnappableObject> snappableObjects;
    public List<SnappableObject> SnappableObjects => snappableObjects;

    [SerializeField] private List<RetrievableObject> documents;
    public List<RetrievableObject> Documents => documents;

    [SerializeField] private List<RetrievableObject> clothes;
    public List<RetrievableObject> Clothes => clothes;

    [SerializeField] private List<RetrievableObject> miscellaneous;
    public List<RetrievableObject> Miscellaneous => miscellaneous;

    [SerializeField] private List<RetrievableObject> weapons;
    public List<RetrievableObject> Weapons => weapons;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string defaultLayerName;
    [SerializeField] private string outlineLayerName;

    public InputManager inputManager;
    public PlayerController playerController;
    public PickUpDrop pickUpDrop;
    public PlayerStats playerStats;

    private IGameState currentState;
    private Dictionary<string, IGameState> states;

    public bool isTimerCompleted;
    public int cleanedCount = 0;
    public int disposedCount = 0;

    public int retrievedDocumetsCount = 0;
    public int retrievedClothesCount = 0;
    public int retrievedMiscellaneousCount = 0;
    public int retrievedWeaponsCount = 0;

    public string totalMoneyString = "TotalMoney";

    public event Action GameLost;
    public event Action GameWon;

    private void OnEnable()
    {
        inputManager.PauseEvent += TogglePause;
    }

    private void OnDisable()
    {
        inputManager.PauseEvent -= TogglePause;
    }

    private void Start()
    {
        states = new Dictionary<string, IGameState>
        {
            { "Init", new InitializationState() },
            { "GamePlay", new GamePlayState() },
            { "Win", new WinState() },
            { "Lose", new LoseState() },
            { "Pause", new PauseState() },
            { "ToolWheel", new ToolWheelState() },
            { "Tablet", new TabletState() },
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
    
    public void OnDocumentRetrieved()
    {
        retrievedDocumetsCount++;
    }
    
    public void OnClothesRetrieved()
    {
        retrievedClothesCount++;
    }
    
    public void OnMiscellaneousRetrieved()
    {
        retrievedMiscellaneousCount++;
    }
    
    public void OnWeaponsRetrieved()
    {
        retrievedWeaponsCount++;
    }

    public void OnSnapObject()
    {
        pickUpDrop.GetObjectGrabbable().ToggleHologram(false);
        pickUpDrop.DropObject();
    }

    public void OnObjectBroken(GameObject brokenPiece, List<GameObject> brokenPieces)
    {
        disposableObjects.Remove(brokenPiece.GetComponent<DisposableObject>());

        foreach (var piece in brokenPieces)
        {
            disposableObjects.Add(piece.GetComponent<DisposableObject>());
        }
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

    public void TogglePause()
    {
        bool isPauseState = currentState is PauseState;
        TransitionToState(isPauseState ? "GamePlay" : "Pause");
        inputManager.ToggleGameplayMap(isPauseState);
    }

    public void SetOutlineLayer()
    {
        SetLayer(outlineLayerName, Documents);
        SetLayer(outlineLayerName, Clothes);
        SetLayer(outlineLayerName, Weapons);
        SetLayer(outlineLayerName, Weapons);
    }

    public void SetDefaultLayer()
    {
        SetLayer(defaultLayerName, Documents);
        SetLayer(defaultLayerName, Clothes);
        SetLayer(defaultLayerName, Weapons);
        SetLayer(defaultLayerName, Weapons);
    }

    private void SetLayer(string layerName, List<RetrievableObject> retrievableObjects)
    {
        foreach(var retrievableObject in retrievableObjects)
        {
            if (retrievableObject == null) return;

            if (retrievableObject.gameObject.layer != LayerMask.NameToLayer(layerName))
                retrievableObject.gameObject.layer = LayerMask.NameToLayer(layerName);
        }
    }
}

public class ToolWheelState : IGameState
{
    public void EnterState(GameStateManager gameStateManager)
    {
        gameStateManager.inputManager.ShowCursor();
        gameStateManager.playerController.isCameraMovable = false;
    }

    public void UpdateState(GameStateManager gameStateManager)
    {

    }

    public void ExitState(GameStateManager gameStateManager)
    {
        gameStateManager.inputManager.HideCursor();
        gameStateManager.playerController.isCameraMovable = true;
    }
}

public class TabletState : IGameState
{
    public void EnterState(GameStateManager gameStateManager)
    {
        gameStateManager.inputManager.ShowCursor();
        gameStateManager.playerController.isCameraMovable = false;
        gameStateManager.playerController.isMovable = false;
    }

    public void UpdateState(GameStateManager gameStateManager)
    {

    }

    public void ExitState(GameStateManager gameStateManager)
    {
        gameStateManager.inputManager.HideCursor();
        gameStateManager.playerController.isCameraMovable = true;
        gameStateManager.playerController.isMovable = true;
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
            disposableObject.Broken += gameStateManager.OnObjectBroken;
        }

        foreach (var snappableObject in gameStateManager.SnappableObjects)
        {
            snappableObject.Snapped += gameStateManager.OnSnapObject;
        }
        
        foreach (var retrievableObject in gameStateManager.Documents)
        {
            retrievableObject.ObjectRetrievedEvent += gameStateManager.OnDocumentRetrieved;
        }
        
        foreach (var retrievableObject in gameStateManager.Clothes)
        {
            retrievableObject.ObjectRetrievedEvent += gameStateManager.OnClothesRetrieved;
        }
        
        foreach (var retrievableObject in gameStateManager.Miscellaneous)
        {
            retrievableObject.ObjectRetrievedEvent += gameStateManager.OnMiscellaneousRetrieved;
        }
        
        foreach (var retrievableObject in gameStateManager.Weapons)
        {
            retrievableObject.ObjectRetrievedEvent += gameStateManager.OnWeaponsRetrieved;
        }





        gameStateManager.cleanedCount = 0;
        gameStateManager.disposedCount = 0;

        gameStateManager.TransitionToState("GamePlay");

        gameStateManager.playerStats.currentMoney = 0;
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
        if (gameStateManager.cleanedCount == gameStateManager.CleanableObjects.Count && 
            gameStateManager.disposedCount == gameStateManager.DisposableObjects.Count &&
            gameStateManager.retrievedDocumetsCount == gameStateManager.Documents.Count &&
            gameStateManager.retrievedClothesCount == gameStateManager.Clothes.Count &&
            gameStateManager.retrievedMiscellaneousCount == gameStateManager.Miscellaneous.Count &&
            gameStateManager.retrievedWeaponsCount == gameStateManager.Weapons.Count)
        {
            gameStateManager.TransitionToState("Win");
            return;
        }

        if (gameStateManager.isTimerCompleted) //Update in the future
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
        gameStateManager.inputManager.ShowCursor();
        gameStateManager.inputManager.ToggleGameplayMap(false);

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
        gameStateManager.inputManager.ShowCursor();

        Debug.Log("Player lost the game");
        gameStateManager.TriggerLoseEvent();
        gameStateManager.inputManager.ToggleGameplayMap(false);

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
        gameStateManager.inputManager.ShowCursor();
        Debug.Log("Game Paused");
    }

    public void UpdateState(GameStateManager gameStateManager)
    {
        
    }

    public void ExitState(GameStateManager gameStateManager)
    {
        gameStateManager.inputManager.HideCursor();
        Debug.Log("Game Resumed");
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
            disposableObject.Broken -= gameStateManager.OnObjectBroken;
        }

        foreach (var snappableObject in gameStateManager.SnappableObjects)
        {
            snappableObject.Snapped -= gameStateManager.OnSnapObject;
        }

        foreach (var retrievableObject in gameStateManager.Documents)
        {
            retrievableObject.ObjectRetrievedEvent -= gameStateManager.OnDocumentRetrieved;
        }

        foreach (var retrievableObject in gameStateManager.Clothes)
        {
            retrievableObject.ObjectRetrievedEvent -= gameStateManager.OnClothesRetrieved;
        }

        foreach (var retrievableObject in gameStateManager.Miscellaneous)
        {
            retrievableObject.ObjectRetrievedEvent -= gameStateManager.OnMiscellaneousRetrieved;
        }

        foreach (var retrievableObject in gameStateManager.Weapons)
        {
            retrievableObject.ObjectRetrievedEvent -= gameStateManager.OnWeaponsRetrieved;
        }

        gameStateManager.CleanableObjects.Clear();
        gameStateManager.DisposableObjects.Clear();

        gameStateManager.playerStats.totalMoney += gameStateManager.playerStats.currentMoney;
        gameStateManager.playerStats.currentMoney = 0;
        PlayerPrefs.SetFloat(gameStateManager.totalMoneyString, gameStateManager.playerStats.totalMoney);
        PlayerPrefs.Save();
    }

    public void UpdateState(GameStateManager gameStateManager)
    {

    }

    public void ExitState(GameStateManager gameStateManager)
    {
        
    }
}
