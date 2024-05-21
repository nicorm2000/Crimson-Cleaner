using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [NonSerialized] public bool isObjectPickedUp = false;
    public string interactionMessage = "Press 'E' to interact";
    public string rotateMessage = "Press 'G' to rotate";

}