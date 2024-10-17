using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int ID;
    public bool isKeyPickedUp;
    public string keyName;

    private RetrievableObject retrievableObject;

    private void Awake()
    {
        retrievableObject = GetComponent<RetrievableObject>();
    }

    private void OnEnable()
    {
        if (retrievableObject)
            retrievableObject.ObjectRetrievedEvent += PickupKey;
    }

    private void OnDisable()
    {
        if (retrievableObject)
            retrievableObject.ObjectRetrievedEvent -= PickupKey;
    }

    private void PickupKey()
    {
        isKeyPickedUp = true;
    }
}
