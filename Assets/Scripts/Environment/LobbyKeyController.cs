using UnityEngine;

public class LobbyKeyController : MonoBehaviour
{
    [SerializeField] private PCCanvasController pCCanvasController;

    public void TriggerKeyDespawn()
    {
        pCCanvasController.DespawnKey();
    }
}
