using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappableManagerLobby : MonoBehaviour
{
    [SerializeField] private PickUpDrop pickUpDrop;
    [SerializeField] private SnappableObject[] snappableObjects;

    private void OnEnable()
    {
        foreach (var obj in snappableObjects) 
        {
            obj.Snapped += OnSnapObject;
        }
    }

    private void OnDisable()
    {
        foreach (var obj in snappableObjects)
        {
            obj.Snapped -= OnSnapObject;
        }
    }

    private void OnSnapObject()
    {
        pickUpDrop.GetObjectGrabbable().ToggleHologram(false);
        pickUpDrop.DropObject();
    }
}
