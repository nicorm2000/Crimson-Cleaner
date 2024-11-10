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

    [SerializeField] private List<Zone> zones;
    public List<Zone> Zones => zones;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string defaultLayerName;
    [SerializeField] private string outlineLayerName;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private TabletUIManager tabletUIManager;

    public InputManager inputManager;
    public PlayerController playerController;
    public PickUpDrop pickUpDrop;
    public PlayerStats playerStats;
    public UIManager uiManager;

    private IGameState currentState;
    private IGameState previousState;
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
        cleaningManager.GetToolSelector().OnToolSwitched += OnToolSwitchedState;
    }

    private void OnDisable()
    {
        inputManager.PauseEvent -= TogglePause;
        cleaningManager.GetToolSelector().OnToolSwitched -= OnToolSwitchedState;
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

    public void OnObjectCleaned(GameObject go)
    {
        UpdateTabletUI(go);
        cleanedCount++;
    }
    
    public void OnObjectDisposed(GameObject go)
    {
        UpdateTabletUI(go);
        disposedCount++;
    } 
    
    public void OnDocumentRetrieved(GameObject go)
    {
        UpdateTabletUI(go);
        retrievedDocumetsCount++;
    }
    
    public void OnClothesRetrieved(GameObject go)
    {
        UpdateTabletUI(go);
        retrievedClothesCount++;
    }
    
    public void OnMiscellaneousRetrieved(GameObject go)
    {
        UpdateTabletUI(go);
        retrievedMiscellaneousCount++;
    }
    
    public void OnWeaponsRetrieved(GameObject go)
    {
        UpdateTabletUI(go);
        retrievedWeaponsCount++;
    }

    public void OnSnapObject(GameObject go)
    {
        UpdateTabletUI(go);
        pickUpDrop.GetObjectGrabbable().ToggleHologram(false);
        pickUpDrop.DropObject();
    }

    public void UpdateTabletUI(GameObject go)
    {
        foreach (var zone in zones)
        {
            foreach (var item in zone.blood)
            {
                if (!item) break;

                if (go == item.gameObject)
                {
                    if (zone.bloodCurrentAmmount < zone.blood.Count)
                    {
                        zone.bloodCurrentAmmount++;
                        tabletUIManager.UpdateBloodText(zone);
                    }
                    return;
                }
            }
            
            foreach (var item in zone.corpses)
            {
                if (!item) break;

                if (go == item.gameObject)
                {
                    if (zone.corpsesCurrentAmmount < zone.corpses.Count)
                    {
                        zone.corpsesCurrentAmmount++;
                        tabletUIManager.UpdateCorpsesText(zone);
                    }
                    return;
                }
            }

            foreach (var item in zone.uvCleanables)
            {
                if (!item) break;
                if (go == item.gameObject)
                {
                    if (zone.uvCleanablesCurrentAmmount < zone.uvCleanables.Count)
                    {
                        zone.uvCleanablesCurrentAmmount++;
                        tabletUIManager.UpdateUVCleanablesText(zone);
                    }
                    return;
                }
            }

            foreach (var item in zone.bloodyObjects)
            {
                if (!item) break;
                if (go == item.gameObject)
                {
                    if (zone.bloodyObjectsCurrentAmmount < zone.bloodyObjects.Count)
                    {
                        zone.bloodyObjectsCurrentAmmount++;
                        tabletUIManager.UpdateBloodyObjectsText(zone);
                    }
                    return;
                }
            }

            foreach (var item in zone.arrabgables)
            {
                if (!item) break;
                if (go == item.gameObject)
                {
                    if (zone.arrabgablesCurrentAmmount < zone.arrabgables.Count)
                    {
                        zone.arrabgablesCurrentAmmount++;
                        tabletUIManager.UpdateArrangablesText(zone);
                    }
                    return;
                }
            }

            foreach (var item in zone.weapons)
            {
                if (!item) break;
                if (go == item.gameObject)
                {
                    if (zone.weaponsCurrentAmmount < zone.weapons.Count)
                    {
                        zone.weaponsCurrentAmmount++;
                        tabletUIManager.UpdateWeaponsText(zone);
                    }
                    return;
                }
            }

            foreach (var item in zone.documents)
            {
                if (!item) break;
                if (go == item.gameObject)
                {
                    if (zone.documentsCurrentAmmount < zone.documents.Count)
                    {
                        zone.documentsCurrentAmmount++;
                        tabletUIManager.UpdateDocumentsText(zone);
                    }
                    return;
                }
            }

            foreach (var item in zone.clothes)
            {
                if (!item) break;
                if (go == item.gameObject)
                {
                    if (zone.clothesCurrentAmmount < zone.clothes.Count)
                    {
                        zone.clothesCurrentAmmount++;
                        tabletUIManager.UpdateClothesText(zone);
                    }
                    return;
                }
            }

            foreach (var item in zone.miscellaneous)
            {
                if (!item) break;
                if (go == item.gameObject)
                {
                    if (zone.miscellaneousCurrentAmmount < zone.miscellaneous.Count)
                    {
                        zone.miscellaneousCurrentAmmount++;
                        tabletUIManager.UpdateMiscellaneousText(zone);
                    }
                    return;
                }
            }

        }
    }

    public void OnToolSwitchedState(int toolIndex)
    {
        if (toolIndex != cleaningManager.GetTablet())
        {
            if (currentState is not GamePlayState)
            {
                TransitionToState("GamePlay");
            }
        }
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

        if (isPauseState)
        {
            if (previousState is TabletState)
            {
                TransitionToState("Tablet");
            }
            else
            {
                TransitionToState("GamePlay");
            }

            return;
        }
        else
        {
            previousState = currentState;
            TransitionToState("Pause");
        }
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

    public IGameState GetCurrentState()
    {
        return currentState;
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
        foreach(var zone in gameStateManager.Zones)
        {
            foreach (var item in zone.blood)
            {
                if (item != null) item.CleanedGO += gameStateManager.OnObjectCleaned;
            }

            foreach (var item in zone.corpses)
            {
                if (item != null) item.DisposedGO += gameStateManager.OnObjectDisposed;
                if (item != null) item.Broken += gameStateManager.OnObjectBroken;
            }

            foreach (var item in zone.uvCleanables)
            {
                if (item != null) item.CleanedGO += gameStateManager.OnObjectCleaned;
            }

            foreach (var item in zone.bloodyObjects)
            {
                if (item != null) item.CleanedGO += gameStateManager.OnObjectCleaned;
            }

            foreach (var item in zone.arrabgables)
            {
                if (item != null) item.SnappedGO += gameStateManager.OnSnapObject;
            }

            foreach (var item in zone.weapons)
            {
                if (item != null) item.ObjectRetrievedEventGO += gameStateManager.OnWeaponsRetrieved;
            }

            foreach (var item in zone.documents)
            {
                if (item != null) item.ObjectRetrievedEventGO += gameStateManager.OnDocumentRetrieved;
            }

            foreach (var item in zone.clothes)
            {
                if (item != null) item.ObjectRetrievedEventGO += gameStateManager.OnClothesRetrieved;
            }

            foreach (var item in zone.miscellaneous)
            {
                if (item != null) item.ObjectRetrievedEventGO += gameStateManager.OnMiscellaneousRetrieved;
            }
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
        gameStateManager.inputManager.ToggleGameplayMap(true);
        gameStateManager.playerController.isCameraMovable = true;
        gameStateManager.playerController.isMovable = true;
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
        gameStateManager.inputManager.ToggleGameplayMap(false);
        gameStateManager.inputManager.ShowCursor();
        Debug.Log("Game Paused");
    }

    public void UpdateState(GameStateManager gameStateManager)
    {
        
    }

    public void ExitState(GameStateManager gameStateManager)
    {
        gameStateManager.inputManager.HideCursor();
        gameStateManager.inputManager.ToggleGameplayMap(true);
        Debug.Log("Game Resumed");
    }
}

public class DeInitializationState : IGameState
{
    public void EnterState(GameStateManager gameStateManager)
    {
        foreach (var zone in gameStateManager.Zones)
        {
            foreach (var item in zone.blood)
            {
                if (item != null) item.CleanedGO -= gameStateManager.OnObjectCleaned;
            }

            foreach (var item in zone.corpses)
            {
                if (item != null) item.DisposedGO -= gameStateManager.OnObjectDisposed;
                if (item != null) item.Broken -= gameStateManager.OnObjectBroken;
            }

            foreach (var item in zone.uvCleanables)
            {
                if (item != null) item.CleanedGO -= gameStateManager.OnObjectCleaned;
            }

            foreach (var item in zone.bloodyObjects)
            {
                if (item != null) item.CleanedGO -= gameStateManager.OnObjectCleaned;
            }

            foreach (var item in zone.arrabgables)
            {
                if (item != null) item.SnappedGO -= gameStateManager.OnSnapObject;
            }

            foreach (var item in zone.weapons)
            {
                if (item != null) item.ObjectRetrievedEventGO -= gameStateManager.OnWeaponsRetrieved;
            }

            foreach (var item in zone.documents)
            {
                if (item != null) item.ObjectRetrievedEventGO -= gameStateManager.OnDocumentRetrieved;
            }

            foreach (var item in zone.clothes)
            {
                if (item != null) item.ObjectRetrievedEventGO -= gameStateManager.OnClothesRetrieved;
            }

            foreach (var item in zone.miscellaneous)
            {
                if (item != null) item.ObjectRetrievedEventGO -= gameStateManager.OnMiscellaneousRetrieved;
            }
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
