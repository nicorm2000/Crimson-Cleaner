using UnityEngine;

public class BloodSplash : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private ParticleSystem bloodSplashParticles;
    [SerializeField] private GameObject bloodStainPrefab;
    [SerializeField] private float stainLifetime = 5f;
    [SerializeField] private float stainOffset = 0.01f;

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.LookRotation(contact.normal);
        Vector3 position = contact.point + contact.normal * stainOffset;

        BloodSplashManager.Instance.CreateBloodSplash(position, rotation, bloodSplashParticles, bloodStainPrefab, stainLifetime, stainOffset);
    }
}