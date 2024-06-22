using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private string totalMoneyString;
    [SerializeField] private TextMeshProUGUI totalMoneyTMP;

    void Start()
    {
        GetPlayersMoney();
    }

    private void GetPlayersMoney()
    {
        playerStats.totalMoney = PlayerPrefs.GetFloat(totalMoneyString, 0);
        totalMoneyTMP.text = "$" + playerStats.totalMoney.ToString(); ;
    }

}
