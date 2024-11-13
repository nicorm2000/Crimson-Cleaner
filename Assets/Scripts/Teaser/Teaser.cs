using System.Collections.Generic;
using UnityEngine;

public class Teaser : MonoBehaviour
{
    [Header("Body Config")]
    [SerializeField] private List<Rigidbody> rbs = new();
    [SerializeField] private float bodyThrowForce = new();
    
    [Header("Mop Config")]
    [SerializeField] private List<Rigidbody> mopRb = new();
    [SerializeField] private List<Collider> mopColl = new();

    [Header("Mop 2 Config")]
    [SerializeField] private GameObject mop2 = null;
    private Animator mop2Animator = null;

    private void Awake()
    {
        for (int i = 0; i < rbs.Count; i++)
        {
            rbs[i].useGravity = false;
            rbs[i].freezeRotation = true;
        }

        for (int i = 0; i < mopRb.Count; i++)
        {
            mopRb[i].useGravity = false;
            mopRb[i].freezeRotation = true;
        }

        for (int i = 0; i < mopColl.Count; i++)
        {
            mopColl[i].enabled = false;
        }

        mop2Animator = mop2.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            BodyEvent();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            MopEvent();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Mop2Event();
        }
    }

    public void MopEvent()
    {
        for (int i = 0; i < mopRb.Count; i++)
        {
            mopRb[i].useGravity = true;
            mopRb[i].freezeRotation = false;
        }

        for (int i = 0; i < mopColl.Count; i++)
        {
            mopColl[i].enabled = true;
        }
    }

    public void Mop2Event()
    {
        mop2Animator.SetBool("Activate", true);
    }

    public void BodyEvent()
    {
        for (int i = 0; i < rbs.Count; i++)
        {
            rbs[i].useGravity = true;
            rbs[i].freezeRotation = false;
            rbs[i].AddForce(new Vector3(1f, 0f, 1f) * bodyThrowForce, ForceMode.Impulse);
        }
    }
}