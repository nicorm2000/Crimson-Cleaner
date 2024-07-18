using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolsWheel : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private int ID;
    [SerializeField] private string itemName;
    [SerializeField] private TextMeshProUGUI itemText;
    
    private Animator _animator;
    private bool _selected = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_selected)
        {
            itemText.text = itemName;
        }
    }

    public void Selected()
    {
        _selected = true;
        ToolsWheelController.toolID = ID;
    }

    public void Deselected()
    {
        _selected = false;
        ToolsWheelController.toolID = 0;
    }

    public void HoverEnter()
    {
        _animator.SetBool("Hover", true);
        itemText.text = itemName;
    }

    public void HoverExit()
    {
        _animator.SetBool("Hover", false);
        itemText.text = itemName;
    }
}