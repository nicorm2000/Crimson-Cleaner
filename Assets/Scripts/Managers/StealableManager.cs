using System.Collections;
using TMPro;
using UnityEngine;

public class StealableManager : MonoBehaviour
{
    [Header("UI Config")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private float moneyPopUpDuration;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string moneyEvent = null;
    
    public static Coroutine currentPopUpCoroutine = null;
    public static float totalMoney = 0;
    private bool coroutineRunning = false;

    public void AddMoney(float amount)
    {
        totalMoney += amount;

        if (currentPopUpCoroutine != null)
        {
            StopCoroutine(currentPopUpCoroutine);
        }

        currentPopUpCoroutine = StartCoroutine(ShowMoneyPopUp());
    }

    private IEnumerator ShowMoneyPopUp()
    {
        coroutineRunning = true;
        float elapsedTime = 0f;

        while (elapsedTime < moneyPopUpDuration)
        {
            moneyText.text = "$" + totalMoney.ToString();
            moneyText.gameObject.SetActive(true);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        moneyText.gameObject.SetActive(false);
        totalMoney = 0;
        coroutineRunning = false;
    }

    public void PlayeMoneySFX() => audioManager.PlaySound(moneyEvent);
}