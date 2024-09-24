using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysManager : MonoBehaviour
{
    [SerializeField] private List<Key> keys = new List<Key>();


    private void Start()
    {
        UncheckKeys();
    }

    private void UncheckKeys()
    {
        foreach (var key in keys)
        {
            key.isKeyPickedUp = false;
        }
    }

    public List<Key> GetKeysList()
    {
        return keys;
    }

    public Key GetKey(int ID)
    {
        foreach (var key in keys)
        {
            if (key.ID == ID)
                return key;
        }

        Debug.Log("No key with " + ID + "ID");
        return null;
    }

}
