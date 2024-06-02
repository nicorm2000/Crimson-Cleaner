using System;
using UnityEngine;

public class DisposableObject : MonoBehaviour, IDisposable
{
    public bool IsObjectDisposed => isObjectDisposed;
    private bool isObjectDisposed = false;
    public event Action Disposed;
    public event Action<GameObject> DisposedGO;

    public void TriggerDisposal()
    {
        Disposed?.Invoke();
        DisposedGO?.Invoke(gameObject);
        isObjectDisposed = true;
        Destroy(gameObject);
    }
}
