using UnityEngine;
using TMPro;

public class AnyKeyMenu : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private MySceneManager mySceneManager = null;
    [SerializeField] private string sceneName = null;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI text = null;
    [SerializeField] private float minAlpha = 0.3f;
    [SerializeField] private float maxAlpha = 1.0f;
    [SerializeField] private float speed = 2.0f;

    private Color _originalColor;
    private float _alpha;

    private void Start()
    {
        _originalColor = text.color;
    }

    private void Update()
    {
        _alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(Time.time * speed) + 1f) / 2f);

        text.color = new Color(_originalColor.r, _originalColor.g, _originalColor.b, _alpha);

        if (mySceneManager == null) 
            return;

        if (Input.anyKeyDown)
        {
            mySceneManager.LoadSceneByName(sceneName);
        }
    }
}