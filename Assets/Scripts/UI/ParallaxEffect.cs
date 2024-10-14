using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] private Transform[] layers;
    [SerializeField] private float[] parallaxSpeeds;

    [Header("Config")]
    [SerializeField] private float parallaxIntensity = 1f;
    [SerializeField] private Camera mainCamera = null;

    private Vector2 lastMousePosition;

    private void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    private void Update()
    {
        Vector2 currentMousePosition = Input.mousePosition;

        Vector2 mouseDelta = (currentMousePosition - lastMousePosition) * parallaxIntensity;

        for (int i = 0; i < layers.Length; i++)
        {
            float parallaxSpeed = parallaxSpeeds[i];
            Vector3 targetPosition = layers[i].position + new Vector3(mouseDelta.x * parallaxSpeed, mouseDelta.y * parallaxSpeed, 0);

            layers[i].position = Vector3.Lerp(layers[i].position, targetPosition, Time.deltaTime * 10f);
        }

        lastMousePosition = currentMousePosition;
    }
}