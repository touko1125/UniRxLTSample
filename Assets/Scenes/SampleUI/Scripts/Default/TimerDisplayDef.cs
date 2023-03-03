using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerDisplayDef : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    [SerializeField] private TimerCounterDef _timerCounter;

    // Update is called once per frame
    void Update()
    {
        DisplayTimer();
    }

    private void DisplayTimer()
    {
        _timerText.text = "TIMER : " + _timerCounter.Timer.ToString("00.00");
    }
}
