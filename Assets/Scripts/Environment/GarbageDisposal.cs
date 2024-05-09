using UnityEngine;
using System.Collections.Generic;

public class GarbageDisposal : MonoBehaviour
{
    [SerializeField] private Cart openable;
    [SerializeField] private BoxCollider collider;
    private List<GameObject> disposalList = new List<GameObject>();


    //void OnTriggerStay(Collider other)
    //{
    //    if (!openable._isOpen && other.CompareTag("Trash") && !disposalList.Contains(other.gameObject))
    //    {
    //        disposalList.Add(other.gameObject);

    //        Destroy(other.gameObject, 0.5f);
    //    }
    //}


    public void DestroyBoxContents()
    {
        RaycastHit[] trash = Physics.BoxCastAll(collider.transform.position, collider.size, Vector3.forward);

        if (trash.Length > 0)
        {
            foreach (RaycastHit raycastHits in trash) 
            {
                if (raycastHits.transform.CompareTag("Trash"))
                    Destroy(raycastHits.collider.gameObject);
            }
        }
    }

}
