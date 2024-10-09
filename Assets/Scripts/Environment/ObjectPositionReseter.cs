using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositionReseter : MonoBehaviour
{
    Dictionary<int, (Vector3,Quaternion)> objectsDictionary = new Dictionary<int, (Vector3, Quaternion)>();

    private PositionReseatable[] objects;
    private int id = 0;
    private void Awake()
    {
        objects = FindObjectsOfType<PositionReseatable>();

        foreach (var obj in objects) 
        {
            objectsDictionary.Add(obj.ID = id, (obj.transform.position, obj.transform.rotation));
            id++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PositionReseatable obj = other.GetComponent<PositionReseatable>();
        if (obj != null)
        {
            var transformData = GetTrasformDataByID(obj.ID);
            other.gameObject.transform.position = transformData.Value.Item1;
            other.gameObject.transform.rotation = transformData.Value.Item2;
            
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    private (Vector3, Quaternion)? GetTrasformDataByID(int id)
    {
        if (objectsDictionary.TryGetValue(id, out (Vector3, Quaternion) transformData))
            return transformData;
        else
            return null;
    }
}
