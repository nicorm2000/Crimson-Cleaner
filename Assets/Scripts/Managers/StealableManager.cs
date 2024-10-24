using System.Collections;
using TMPro;
using UnityEngine;

public class StealableManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [Header("UI Config")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI pauseMoneyText;
    [SerializeField] private float moneyPopUpDuration;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string moneyEvent = null;
    
    public static Coroutine currentPopUpCoroutine = null;

    public void AddMoney(float amount)
    {
        playerStats.currentMoney += amount;

        if (currentPopUpCoroutine != null)
        {
            StopCoroutine(currentPopUpCoroutine);
            moneyText.gameObject.SetActive(false);
        }

        currentPopUpCoroutine = StartCoroutine(ShowMoneyPopUp());
    }

    private IEnumerator ShowMoneyPopUp()
    {
        moneyText.gameObject.SetActive(true);
        float elapsedTime = 0f;

        while (elapsedTime < moneyPopUpDuration)
        {
            moneyText.text = "$" + playerStats.currentMoney.ToString();
            pauseMoneyText.text = "$" + playerStats.currentMoney.ToString();

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        moneyText.gameObject.SetActive(false);
        currentPopUpCoroutine = null;
    }

    public void PlayMoneySFX() => audioManager.PlaySound(moneyEvent);
}