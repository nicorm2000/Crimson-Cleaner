using UnityEngine;

public class GarbageDisposal : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private BoxCollider coll;
    [SerializeField] private ParticleSystem burnParticles;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string fireEvent = null;

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

    public void ActivateBurning()
    {
        burnParticles.Play();
        audioManager.PlaySound(fireEvent);
    }

    public void ActivateBarrier() => coll.enabled = true;
    public void DeactivateBarrier() => coll.enabled = false;
}