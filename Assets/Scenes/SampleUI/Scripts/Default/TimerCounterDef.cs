using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCounterDef : MonoBehaviour
{
    private float _timer = 0.0f;
    public float Timer => _timer;   //timerの読み取り専用プロパティ

    [SerializeField] private TimerDisplayDef _timerDisplayDef;
    [SerializeField] private TapCounterDef _tapCounterDef;
    void Start()
    {
        _timer = 30.0f;
    }

    void Update()
    {
        if (_timer < 0) return;
        
        CountDownTime();
    }

    private void CountDownTime()
    {
        //カウントダウン
        _timer -= Time.deltaTime;

        //タップの入力の受け取りを停止
        if (_timer < 0)
        {
            _tapCounterDef.StopTap = true;
        }

        //表示
        _timerDisplayDef.DisplayTimer(_timer);
    }
}
