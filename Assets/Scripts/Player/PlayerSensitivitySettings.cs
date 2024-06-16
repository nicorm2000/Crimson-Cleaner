using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSensitivitySettings", menuName = "Settings/Player Sensitivity Settings")]
public class PlayerSensitivitySettings : ScriptableObject
{
    [Header("Mouse Sensitivity")]
    public float sensitivityX;
    public float sensitivityY;
    public float maxSensitivityX;
    public float maxSensitivityY;
}
