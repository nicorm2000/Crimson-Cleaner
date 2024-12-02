using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolsWheel : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private ToolsWheelController toolsWheelController;
    [SerializeField] private string hoverHash;

    private Image image;
    [SerializeField] private Sprite enterSprite;
    [SerializeField] private Sprite exitSprite;

    [NonSerialized] public int ID;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void HoverEnter()
    {
        toolsWheelController.currentToolID = ID;
    }

    public void SetHighlight(bool active)
    {
        if (active)
        {
            image.sprite = enterSprite;
        }
        else
        {
            image.sprite = exitSprite;
        }
    }
}
