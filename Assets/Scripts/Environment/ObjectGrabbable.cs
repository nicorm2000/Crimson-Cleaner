using System.Collections;
using UnityEngine;
using static UnityEngine.UI.Button;

public class ObjectGrabbable : MonoBehaviour, IPickable
{
    [Header("Config")]
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float throwingForce = 10f;

    [Header("UI Config")]
    [SerializeField] private Sprite pickUpMessage;
    [SerializeField] private Sprite dropMessage;
    [SerializeField] private Sprite throwMessage;
    [SerializeField] private Sprite rotateMessage;

    //[Header("Audio Config")]
    //[SerializeField] private AudioManager audioManager = null;
    //[SerializeField] private string hitGroundEvent = null;

    private Rigidbody objectRigidBody;
    private Transform objectGrabPointTransform;
    private Vector3 initialLocalUp;
    private Vector3 initialLocalRight;
    private Vector3 newPosition;
    private Vector3 lastPosition;

    public bool isObjectPickedUp { get; private set; }
    public Sprite PickUpMessage => pickUpMessage;
    public Sprite DropMessage => dropMessage;
    public Sprite ThrowMessage => throwMessage;
    public Sprite RotateMessage => rotateMessage;

    public bool hasSoundAndParticles = false;
    
    //private int playerLayer;
    //private bool canPlaySound = true;
    //private float soundCooldown = 0.25f;
    //
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (hasSoundAndParticles)
    //    {
    //        if (collision.gameObject.layer != playerLayer && canPlaySound)
    //        {
    //            StartCoroutine(SoundCooldownCoroutine());
    //        }
    //    }
    //}
    //
    //private IEnumerator SoundCooldownCoroutine()
    //{
    //    canPlaySound = false;
    //    audioManager.PlaySound(hitGroundEvent);
    //    yield return new WaitForSeconds(soundCooldown);
    //    canPlaySound = true;
    //}

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        //playerLayer = LayerMask.NameToLayer("player");
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    audioManager.PlaySound(hitGroundEvent);
    //}

    public void Grab(Transform ObjectGrabPointTransform)
    {
        this.objectGrabPointTransform = ObjectGrabPointTransform;
        objectRigidBody.useGravity = false;
        objectRigidBody.freezeRotation = true;

        initialLocalUp = transform.up;
        initialLocalRight = transform.right;

        isObjectPickedUp = true;
    }

    public void Drop()
    {
        objectRigidBody.freezeRotation = false;
        this.objectGrabPointTransform = null;
        objectRigidBody.useGravity = true;
        objectRigidBody.velocity = (newPosition - lastPosition) * throwingForce;

        isObjectPickedUp = false;
    }

    public void Throw(float throwingForce, Vector3 throwDirection)
    {
        objectRigidBody.freezeRotation = false;
        this.objectGrabPointTransform = null;
        objectRigidBody.useGravity = true;

        objectRigidBody.AddForce(throwDirection * throwingForce, ForceMode.Impulse);

        isObjectPickedUp = false;
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            lastPosition = newPosition;
            newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidBody.MovePosition(newPosition);
        }
    }

    public void RotateObject(float mouseX, float mouseY)
    {
        transform.Rotate(initialLocalUp, mouseX * rotationSpeed * Time.deltaTime, Space.World);
        transform.Rotate(initialLocalRight, mouseY * rotationSpeed * Time.deltaTime, Space.World);
    }
}
