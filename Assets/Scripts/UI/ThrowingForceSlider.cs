using UnityEngine;
using UnityEngine.UI;

public class ThrowingForceSlider : MonoBehaviour
{
    public PickUpDrop pickUpDrop;
    public InputManager inputManager;
    public Slider slider;

    private void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = pickUpDrop.GetMaxThrowingForce();
        slider.gameObject.SetActive(false);
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
            slider.gameObject.SetActive(true);
        }
    }

    private void OnThrowEnd()
    {
        slider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (slider.gameObject.activeSelf) 
        {
            slider.value = pickUpDrop.GetCurrentThrowingForce();
        }
    }
}
