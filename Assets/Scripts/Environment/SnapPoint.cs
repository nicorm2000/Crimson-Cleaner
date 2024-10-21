using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    public Transform snapTransform;

    private void Awake()
    {
        snapTransform = gameObject.transform;
    }
}
