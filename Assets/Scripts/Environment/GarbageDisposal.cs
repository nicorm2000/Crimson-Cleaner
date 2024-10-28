using System.Collections;
using UnityEngine;

public class GarbageDisposal : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private BoxCollider coll;
    [SerializeField] private ParticleSystem burnParticles;
    [SerializeField] private Light burnLight;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string fireEvent = null;

    private void Start()
    {
        coll.enabled = false;
    }

    public void DestroyBoxContents()
    {
        coll.enabled = true;

        Collider[] hitColliders = Physics.OverlapBox(coll.transform.position, coll.size);

        foreach (Collider hitCollider in hitColliders)
        {
            DisposableObject disposableObject = hitCollider.GetComponent<DisposableObject>();
            if (disposableObject)
            {
                Debug.Log("Hit Object: " + disposableObject.gameObject.name);
                disposableObject.TriggerDisposal();
            }
        }
        coll.enabled = false;
    }

    public void ActivateBurning()
    {
        burnParticles.Play();
        audioManager.PlaySound(fireEvent);
    }

    public void ActivateBarrier() => coll.enabled = true;
    public void DeactivateBarrier() => coll.enabled = false;

    //private void OnDrawGizmos()
    //{
    //    if (coll == null) return;
    //    Gizmos.DrawWireCube(coll.center, coll.size);
    //}
}
