using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerDisplayDef : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    public void DisplayTimer(float time)
    {
        _timerText.text = "TIMER : " + time.ToString("00.00");
    }
}
