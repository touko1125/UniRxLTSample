using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TimerCounterUniRx : MonoBehaviour
{
    private ReactiveProperty<float> _timer = new ReactiveProperty<float>();
    public IReadOnlyReactiveProperty<float> Timer => _timer;
    void Start()
    {
        _timer.Value = 30.0f;

        //通常のUpdate関数が実行されるタイミングを"イベント"として、使用する値として_timer.Value(タイマーの値)を選択
        //さらに選択した値に対して0.0fより大きいという条件を満たす間、実行する処理(カウントダウン)を指定
        this.UpdateAsObservable().Select(x => _timer.Value)
            .Where(time => time > 0.0f)
            .Subscribe(_ => CountDownTime());
    }

    private void CountDownTime()
    {
        //カウントダウン
        _timer.Value -= Time.deltaTime;
    }
}
