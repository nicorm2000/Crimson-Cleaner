using UnityEngine;
using System.Collections;

public class BloodSplashManager : MonoBehaviour
{
    public static BloodSplashManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateBloodSplash(Vector3 position, Quaternion rotation, ParticleSystem bloodSplashParticles, GameObject bloodStainPrefab, float stainLifetime, float stainOffset)
    {
        ParticleSystem splash = Instantiate(bloodSplashParticles, position, rotation);
        splash.Play();
        Destroy(splash.gameObject, splash.main.duration);

        Vector3 stainPosition = position + (rotation * Vector3.forward) * stainOffset;
        GameObject stain = Instantiate(bloodStainPrefab, stainPosition, rotation);
        stain.transform.Rotate(180, 0, 0);
        StartCoroutine(FadeOutStain(stain, stainLifetime));
    }

    private IEnumerator FadeOutStain(GameObject stain, float stainLifetime)
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