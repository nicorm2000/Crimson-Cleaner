using TMPro;
using UnityEngine;

public class VersionNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionText;

    private void Start()
    {
        versionText.text = "V. " + Application.version;
    }
}