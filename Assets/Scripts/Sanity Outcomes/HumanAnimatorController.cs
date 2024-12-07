using UnityEngine;

public class HumanAnimatorController : MonoBehaviour
{
    [SerializeField] private Humans humansController;
    
    public void ToggleCharacterRigOn()
    {
        humansController.ToggleCharacterRig(true);
        if (/*SanityManager.Instance.isTabletActive && */
            CleaningManager.Instance.GetToolSelector().CurrentToolIndex == CleaningManager.Instance.GetTablet()) 
            humansController.ToggleTablet(true);
    }
    
    public void ToggleCharacterRigOff()
    {
        humansController.ToggleCharacterRig(false);
        humansController.ToggleTablet(false);
    } 
    
    public void ToggleGamelayInputMapOn()
    {
        humansController.ToggleGamelayInputMap(true);
    }
    
    public void ToggleGamelayInputMapOff()
    {
        humansController.ToggleGamelayInputMap(false);
    }
}
