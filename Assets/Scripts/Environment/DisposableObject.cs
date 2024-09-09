using System;
using System.Collections.Generic;
using UnityEngine;

public class DisposableObject : MonoBehaviour, IDisposableCustom
{
    public List<GameObject> breakablePieces;

    public bool IsObjectDisposed => isObjectDisposed;
    private bool isObjectDisposed = false;
    public event Action Disposed;
    public event Action<GameObject> DisposedGO;
    public event Action<GameObject, List<GameObject>> Broken;

    private Rigidbody objectRigidBody;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }

    public void TriggerDisposal()
    {
        SanityManager.Instance.ModifySanityScalar(SanityManager.Instance.BurnObjectScaler);
        Disposed?.Invoke();
        DisposedGO?.Invoke(gameObject);
        isObjectDisposed = true;
        Destroy(gameObject);
    }

    public void TriggerBreaking()
    {
        List<GameObject> spawnedPieces = new List<GameObject>();

        foreach (var piece in breakablePieces)
        {
            GameObject brokenPiece = SpawnBrokenPiece(piece);
            if (brokenPiece != null)
            {
                spawnedPieces.Add(brokenPiece);
            }
        }

        Broken?.Invoke(gameObject, spawnedPieces);

        Destroy(gameObject);
    }

    private GameObject SpawnBrokenPiece(GameObject brokenPrefab)
    {
        if (brokenPrefab == null) return null;

        GameObject brokenPiece = Instantiate(brokenPrefab, transform.position, transform.rotation);

        Rigidbody brokenRigidBody = brokenPiece.GetComponent<Rigidbody>();
        if (brokenRigidBody != null)
        {
            brokenRigidBody.velocity = objectRigidBody.velocity;
            brokenRigidBody.angularVelocity = objectRigidBody.angularVelocity;

            brokenRigidBody.AddExplosionForce(100f, transform.position, 1f);
        }

        return brokenPiece;
    }
}
