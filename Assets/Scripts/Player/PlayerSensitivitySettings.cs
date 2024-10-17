using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSensitivitySettings", menuName = "Settings/Player Sensitivity Settings")]
public class PlayerSensitivitySettings : ScriptableObject
{
    [Header("Mouse Sensitivity")]
    public float sensitivity = 6.5f;
}
