using UnityEngine;

public class Humans : MonoBehaviour
{
    [SerializeField] private GameObject CharacterRigGeoGO;
    [SerializeField] private GameObject CharacterRigJoints;
    public GameObject tablet;

    [SerializeField] private InputManager inputManager;

    public void ToggleCharacterRig(bool active)
    {
        CharacterRigGeoGO.SetActive(active);
        CharacterRigJoints.SetActive(active);
    }

    public void ToggleTablet(bool active)
    {
        tablet.SetActive(active);
    }

    public void ToggleGamelayInputMap(bool active)
    {
        inputManager.ToggleGameplayMap(active);
    }
}
