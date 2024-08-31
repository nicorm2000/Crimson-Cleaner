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
            gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
        else
        {
            image.sprite = exitSprite;
            gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}
