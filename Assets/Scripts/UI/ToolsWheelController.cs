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

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string openToolWheelEvent = null;
    [SerializeField] private string closeToolWheelEvent = null;
    [SerializeField] private string hoverToolEvent = null;

    public int previousToolID;
    public int currentToolID;

    private Animator _animator;

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
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2();

        moveInput.x = Input.mousePosition.x - (Screen.width / 2f);
        moveInput.y = Input.mousePosition.y - (Screen.height / 2f);

        float distance = moveInput.magnitude;

        if (distance < minRadius)
        {
            //if (!hoverSoundPlayed && hoverToolEvent != null)
            //    audioManager.PlaySound(hoverToolEvent);
            tools[tools.Length - 1].SetHighlight(true);
            tools[tools.Length - 1].HoverEnter();

            for (int i = 0; i < tools.Length - 1; i++)
            {
                tools[i].SetHighlight(false);
            }
        }
        else if (distance < maxRadius)
        {
            tools[tools.Length - 1].SetHighlight(false);
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
                        //if (hoverToolEvent != null)
                        //    audioManager.PlaySound(hoverToolEvent);
                        tools[i].HoverEnter();
                        tools[i].SetHighlight(true);
                    }
                    else
                    {
                        tools[i].SetHighlight(false);
                    }
                }
            }
        }
    }

    private void OnToolWheelStart()
    {
        previousToolID = currentToolID;
        currentToolID = -1;

        reticle.gameObject.SetActive(false);

        if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetTablet())
            gameStateManager.TransitionToState("Tablet");
        else
            gameStateManager.TransitionToState("ToolWheel");

        if (openToolWheelEvent != null)
            audioManager.PlaySound(openToolWheelEvent);
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

        if (closeToolWheelEvent != null)
            audioManager.PlaySound(closeToolWheelEvent);
        _animator.SetBool("OpenToolWheel", false);
        SelectTool();
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
            cleaningTool.SwitchTool(currentToolID);
        }
        else
        {
            Debug.LogWarning($"ToolID {currentToolID} is out of range.");
        }
    }
}