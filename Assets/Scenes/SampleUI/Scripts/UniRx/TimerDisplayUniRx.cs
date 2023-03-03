using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public class TimerDisplayUniRx : MonoBehaviour
{
    [SerializeField] private TimerCounterUniRx _timerCounter;
    [SerializeField] private TextMeshProUGUI _timerText;

    void Start()
    {
        //タイマーの値が変化するイベントを検知し、変化後の値を引数としDisplayTimerの関数を実行する
        _timerCounter.Timer.Subscribe(time => DisplayTimer(time));
    }

    private void DisplayTimer(float time)
    {
        _timerText.text = "TIMER : " + time.ToString("00.00");
    }
}
