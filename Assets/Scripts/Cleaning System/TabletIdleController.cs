using UnityEngine;

public class TabletIdleController : MonoBehaviour
{
    [SerializeField] private MediumTierOutcome humansOutcome;
    [SerializeField] private GameObject tablet;

    public void ToggleTabletOn()
    {
        tablet.SetActive(true);
    }

    public void ToggleTabletOff()
    {
        tablet.SetActive(false);
    }
}
