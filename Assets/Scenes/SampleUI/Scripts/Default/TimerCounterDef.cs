using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCounterDef : MonoBehaviour
{
    private float _timer = 0.0f;
    public float Timer => _timer;   //timerの読み取り専用プロパティ

    void Start()
    {
        _timer = 30.0f;
    }

    void Update()
    {
        CountDownTime();
    }

    private void CountDownTime()
    {
        //カウントダウン
        _timer -= Time.deltaTime;
    }
}
