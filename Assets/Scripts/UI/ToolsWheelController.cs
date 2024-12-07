using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolsWheelController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private CleaningTool cleaningTool;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private Image reticle;
    [SerializeField] private float minRadius;
    [SerializeField] private float maxRadius;
    [SerializeField] private ToolsWheel[] tools;

    public int previousToolID;
    public int currentToolID;
    private Animator _animator;

    private List<bool> hoverSoundPlayed;
    private bool isToolWheelActive = false;

    private void OnEnable()
    {
        inputManager.ToggleToolWheelStartEvent += OnToolWheelStart;
        inputManager.ToggleToolWheelEndEvent += OnToolWheelEnd;
    }

    private void OnDisable()
    {
        inputManager.ToggleToolWheelStartEvent -= OnToolWheelStart;
        inputManager.ToggleToolWheelEndEvent -= OnToolWheelEnd;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        AssignToolIDs();

        hoverSoundPlayed = new List<bool>();

        for (int i = 0; i < tools.Length; i++)
        {
            bool newHoverSoundPlayed = false;
            hoverSoundPlayed.Add(newHoverSoundPlayed);
        }
    }

    private void Update()
    {
        if (!isToolWheelActive || SanityManager.Instance.isHumansOutcomeActive) return;

        Vector2 moveInput = new Vector2();

        moveInput.x = Input.mousePosition.x - (Screen.width / 2f);
        moveInput.y = Input.mousePosition.y - (Screen.height / 2f);

        float distance = moveInput.magnitude;

        if (distance < minRadius)
        {
            if (!hoverSoundPlayed[tools.Length - 1] && cleaningManager.GetHoverEvent() != null)
            {
                cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetHoverEvent());
                hoverSoundPlayed[tools.Length - 1] = true;
            }
            tools[tools.Length - 1].SetHighlight(true);
            tools[tools.Length - 1].HoverEnter();

            for (int i = 0; i < tools.Length - 1; i++)
            {
                tools[i].SetHighlight(false);
                hoverSoundPlayed[i] = false;
            }
        }
        else if (distance < maxRadius)
        {
            tools[tools.Length - 1].SetHighlight(false);
            hoverSoundPlayed[tools.Length - 1] = false;

            moveInput.Normalize();

            if (moveInput != Vector2.zero)
            {
                float angle = Mathf.Atan2(moveInput.y, -moveInput.x) * Mathf.Rad2Deg + 90f;
                if (angle < 0) angle += 360f;

                for (int i = 0; i < tools.Length - 1; i++)
                {
                    float startAngle = (i * (360f / 4) + 45f);
                    float endAngle = ((i + 1) * (360f / 4) + 45f);

                    if ((angle > startAngle && angle <= endAngle) ||
                        (endAngle > 360f && (angle > startAngle || angle <= endAngle - 360f)))
                    {
                        if (!hoverSoundPlayed[i] && cleaningManager.GetHoverEvent() != null)
                        {
                            cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetHoverEvent());
                            hoverSoundPlayed[i] = true;
                        }
                        tools[i].HoverEnter();
                        tools[i].SetHighlight(true);
                    }
                    else
                    {
                        tools[i].SetHighlight(false);
                        hoverSoundPlayed[i] = false;
                    }
                }
            }
        }
    }

    private void OnToolWheelStart()
    {
        isToolWheelActive = true;

        previousToolID = currentToolID;
        currentToolID = -1;

        reticle.gameObject.SetActive(false);

        if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetTablet())
            gameStateManager.TransitionToState("Tablet");
        else
            gameStateManager.TransitionToState("ToolWheel");

        if (cleaningManager.GetOpenTWEvent() != null)
            cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetOpenTWEvent());
        _animator.SetBool("OpenToolWheel", true);
    }

    private void OnToolWheelEnd()
    {
        if (currentToolID == -1)
            currentToolID = previousToolID;

        if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetTablet())
            gameStateManager.TransitionToState("Tablet");
        else
        {
            gameStateManager.TransitionToState("GamePlay");
            reticle.gameObject.SetActive(true);
        }

        if (cleaningManager.GetCloseTWEvent() != null)
            cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetCloseTWEvent());
        _animator.SetBool("OpenToolWheel", false);
        SelectTool();

        isToolWheelActive = false;
    }

    private void AssignToolIDs()
    {
        ToolsWheel[] toolWheels = GetComponentsInChildren<ToolsWheel>();
        for (int i = 0; i < toolWheels.Length; i++)
        {
            if (i < cleaningTool.ToolsLength)
            {
                toolWheels[i].ID = i;
            }
        }
    }

    private void SelectTool()
    {
        if (currentToolID >= 0 && currentToolID < cleaningTool.ToolsLength)
        {
            if (currentToolID != cleaningTool.CurrentToolIndex)
                cleaningTool.SwitchTool(currentToolID);
        }
        else
        {
            Debug.LogWarning($"ToolID {currentToolID} is out of range.");
        }
    }
}