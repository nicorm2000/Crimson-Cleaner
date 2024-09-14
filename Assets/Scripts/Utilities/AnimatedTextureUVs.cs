using UnityEngine;

public class AnimatedTextureUVs : MonoBehaviour
{
    public int _uvTieX = 1;
    public int _uvTieY = 1;
    public int _fps = 10;

    private Vector2 _size;
    private Renderer _myRenderer;
    private int _lastIndex = -1;
    private string textureProperty = "_MainTex";

    void Start()
    {
        _size = new Vector2(1.0f / _uvTieX, 1.0f / _uvTieY);
        _myRenderer = GetComponent<Renderer>();
        if (_myRenderer == null)
        {
            enabled = false;
            return;
        }

        Shader shader = _myRenderer.material.shader;
        string shaderName = shader.name;

        if (shaderName.Contains("HDRP/Lit"))
        {
            textureProperty = "_BaseColorMap";
        }
        else if (shaderName.Contains("Legacy Shaders/Particles/Additive"))
        {
            textureProperty = "_MainTex";
        }
        else
        {
            Debug.LogWarning("Shader not recognized, using default _MainTex property.");
        }
    }

    void Update()
    {
        int index = (int)(Time.timeSinceLevelLoad * _fps) % (_uvTieX * _uvTieY);
        if (index != _lastIndex)
        {
            int uIndex = index % _uvTieX;
            int vIndex = index / _uvTieY;

            Vector2 offset = new Vector2(uIndex * _size.x, 1.0f - _size.y - vIndex * _size.y);

            _myRenderer.material.SetTextureOffset(textureProperty, offset);
            _myRenderer.material.SetTextureScale(textureProperty, _size);

            _lastIndex = index;
        }
    }
}
