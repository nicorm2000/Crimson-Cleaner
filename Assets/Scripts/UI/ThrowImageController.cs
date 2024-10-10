using UnityEngine;
using UnityEngine.UI;

public class ThrowImageController : MonoBehaviour
{
    public PickUpDrop pickUpDrop;
    public InputManager inputManager;
    public Image throwImage;

    private void Start()
    {
        throwImage.fillAmount = 0f;
        throwImage.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        inputManager.ThrowStartEvent += OnThrowStart;
        inputManager.ThrowEndEvent += OnThrowEnd;
    }

    private void OnDisable()
    {
        inputManager.ThrowStartEvent -= OnThrowStart;
        inputManager.ThrowEndEvent -= OnThrowEnd;
    }

    private void OnThrowStart()
    {
        if (pickUpDrop.GetObjectGrabbable() != null)
        {
            throwImage.gameObject.SetActive(true);
        }
    }

    private void OnThrowEnd()
    {
        throwImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (throwImage.gameObject.activeSelf)
        {
            float currentThrowingProgress = pickUpDrop.GetCurrentThrowingForce();
            float normalizedThrowingValue = currentThrowingProgress / pickUpDrop.GetMaxThrowingForce();

            throwImage.fillAmount = Mathf.Clamp01(normalizedThrowingValue);

            throwImage.color = throwImage.fillAmount >= 1 ? Color.yellow : Color.white;
        }
    }
}
