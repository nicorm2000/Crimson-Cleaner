using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BloodFootstepGenerator : MonoBehaviour
{
    [SerializeField] private BloodPoolDetection bloodPoolDetection;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float bloodPrintSpawningDuration;
    [SerializeField] private float bloodPrintSpawnDelay;
    [SerializeField] private float bloodPrintLifeTime;

    [SerializeField] private List<GameObject> leftFootstepPrefabs;
    [SerializeField] private List<GameObject> rightFootstepPrefabs;

    [SerializeField] private Transform leftFootTransform;
    [SerializeField] private Transform rightFootTransform;

    private bool isLeftFootNext = true;
    private bool canSpawnBloodPrint = true;
    private bool isSpawningActive = false;

    private void OnEnable()
    {
        bloodPoolDetection.poolDetected += OnPoolDetected;
    }

    private void OnDisable()
    {
        bloodPoolDetection.poolDetected -= OnPoolDetected;
    }

    private void OnPoolDetected()
    {
        if (!isSpawningActive)
            StartCoroutine(StartBloodSpawning(bloodPrintSpawningDuration));
    }

    private IEnumerator StartBloodSpawning(float spawningDuration)
    {
        isSpawningActive = true;

        float elapsedTime = 0f;

        while (elapsedTime < spawningDuration)
        {
            if (canSpawnBloodPrint && inputManager.Move != Vector2.zero)
            {
                StartCoroutine(InstanceBloodPrint(bloodPrintSpawnDelay));
            }
            elapsedTime += bloodPrintSpawnDelay;  
            yield return new WaitForSeconds(bloodPrintSpawnDelay);
        }

        isSpawningActive = false;
    }

    private IEnumerator InstanceBloodPrint(float coroutineDuration)
    {
        if (isLeftFootNext)
        {
            SpawnBloodPrint(leftFootTransform, leftFootstepPrefabs);  
        }
        else
        {
            SpawnBloodPrint(rightFootTransform, rightFootstepPrefabs);
        }

        isLeftFootNext = !isLeftFootNext;
        canSpawnBloodPrint = false;
        yield return new WaitForSeconds(coroutineDuration);
        canSpawnBloodPrint = true;
    }

    private void SpawnBloodPrint(Transform footTransform, List<GameObject> footPrintPrefabs)
    {
        int randomIndex = Random.Range(0, footPrintPrefabs.Count);
        GameObject selectedFootPrint = footPrintPrefabs[randomIndex];
        GameObject newBloodPrint = Instantiate(selectedFootPrint, footTransform.position, Quaternion.LookRotation(Vector3.down));
        StartCoroutine(FadeOutPrint(newBloodPrint, bloodPrintLifeTime));
    }

    private IEnumerator FadeOutPrint(GameObject print, float printLifetime)
    {
        float elapsedTime = 0f;
        Material stainMaterial = print.GetComponent<Renderer>().material;
        Color originalColor = stainMaterial.color;

        while (elapsedTime < printLifetime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / printLifetime);
            stainMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        Destroy(print);
    }

}
