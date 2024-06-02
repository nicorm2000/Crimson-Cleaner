using System;
using UnityEngine;

public class GarbageDisposal : MonoBehaviour
{
    [SerializeField] private Cart openable;
    [SerializeField] private BoxCollider coll;
    [SerializeField] private ParticleSystem burnParticles;

    private void Start()
    {
        coll.enabled = true;
    }

    public void DestroyBoxContents()
    {
        RaycastHit[] trash = Physics.BoxCastAll(coll.transform.position,
                                                coll.size,
                                                Vector3.forward);

        if (trash.Length > 0)
        {
            foreach (RaycastHit raycastHits in trash) 
            {
                DisposableObject disposableObject = raycastHits.transform.GetComponent<DisposableObject>();
                if (disposableObject)
                {
                    disposableObject.TriggerDisposal();
                }
            }
        }
    }

    public void ActivateBurning() => burnParticles.Play();
    public void ActivateBarrier() => coll.enabled = true;
    public void DeactivateBarrier() => coll.enabled = false;
}