using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHoverDetector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI applicationName = null;
    [SerializeField] private List<string> targetNames;

    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    private PointerEventData pointerEventData;

    void Awake()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
        pointerEventData = new PointerEventData(eventSystem);
    }

    void Update()
    {
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new ();
        raycaster.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            foreach (var result in results)
            {
                if (targetNames.Contains(result.gameObject.name))
                {
                    applicationName.text = result.gameObject.name;
                }
            }
        }
        else
        {
            applicationName.text = "";
        }
    }
}