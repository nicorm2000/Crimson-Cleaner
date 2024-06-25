using UnityEngine;
using System.Collections;

public class BloodSplash : MonoBehaviour
{
    public ParticleSystem bloodSplashParticles;
    public GameObject bloodStainPrefab;
    public float stainLifetime = 5f;
    public float stainOffset = 0.01f; // Small offset to prevent z-fighting

    void OnCollisionEnter(Collision collision)
    {
        // Trigger blood splash particle effect
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.LookRotation(contact.normal);
        Vector3 position = contact.point + contact.normal * stainOffset; // Apply offset

        ParticleSystem splash = Instantiate(bloodSplashParticles, position, rotation);
        splash.Play();
        Destroy(splash.gameObject, splash.main.duration);

        // Apply blood stain decal
        GameObject stain = Instantiate(bloodStainPrefab, position, rotation);
        stain.transform.Rotate(180, 0, 0); // Ensure the quad faces the camera
        StartCoroutine(FadeOutStain(stain));
    }

    private IEnumerator FadeOutStain(GameObject stain)
    {
        float elapsedTime = 0f;
        Material stainMaterial = stain.GetComponent<Renderer>().material;
        Color originalColor = stainMaterial.color;

        while (elapsedTime < stainLifetime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / stainLifetime);
            stainMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(stain);
    }
}
