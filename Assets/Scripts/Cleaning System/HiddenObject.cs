using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    private Renderer _renderer;
    private Collider _collider;
    private UVLight _uvLight;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
        _renderer.enabled = false;
        _collider.enabled = false;

        _uvLight = FindObjectOfType<UVLight>();
    }

    void Update()
    {
        if (_uvLight != null)
        {
            bool isInLightRadius = _uvLight.IsObjectInLightRadius(gameObject);

            _collider.enabled = isInLightRadius;
            _renderer.enabled = isInLightRadius;
        }
    }
}