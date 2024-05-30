using System;
using TMPro;
using UnityEngine;

public class UIMainMenuManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI gameTime = null;

    private void Update()
    {
        LogCurrentTime();
    }

    private void LogCurrentTime()
    {
        DateTime now = DateTime.Now;
        string formattedTime = now.ToString("hh:mm tt");
        gameTime.text = formattedTime;
    }
}