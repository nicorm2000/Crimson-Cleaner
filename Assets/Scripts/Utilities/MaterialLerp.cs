using System.Collections;
using UnityEngine;

public class MaterialLerp : MonoBehaviour
{
    [SerializeField] private float duration;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        StartCoroutine(BlendTextures());
    }

    private IEnumerator BlendTextures()
    {
        Material mat = _renderer.material;
        float timeElapsed = 0.0f;
        while (timeElapsed < duration)
        {
            float blend = Mathf.Lerp(0.0f, 1.0f, timeElapsed / duration);
            mat.SetFloat("_Blend", blend);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}