using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [NonSerialized] public bool isObjectPickedUp = false;
    public string interactionMessage = "Press [E] to pick up";
    public string rotateMessage = "Hold [G] to rotate";
}