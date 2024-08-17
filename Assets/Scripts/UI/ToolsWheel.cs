using System;
using TMPro;
using UnityEngine;

public class ToolsWheel : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private ToolsWheelController toolsWheelController;
    [SerializeField] private string hoverHash;

    [NonSerialized] public string itemName;
    [NonSerialized] public int ID;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void HoverEnter()
    {
        _animator.SetBool(hoverHash, true);
        toolsWheelController.toolID = ID;
        toolsWheelController.previousToolID = ID;
        itemText.text = itemName;
    }

    public void HoverExit()
    {
        _animator.SetBool(hoverHash, false);
        toolsWheelController.toolID = toolsWheelController.previousToolID;
        itemText.text = "";
    }
}
