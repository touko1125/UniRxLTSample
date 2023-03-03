using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public class TapDisplayUniRx : MonoBehaviour
{
    [SerializeField] private TapCounterUniRx _tapCounter;

    [SerializeField] private TextMeshProUGUI _tapText;
    // Start is called before the first frame update
    void Start()
    {
        //タップ回数の値が変化した"イベント"を検知して、"変更後の値"を一時的にxという名前の変数に格納し実行する処理の引数に指定して実行
        _tapCounter.TapNum.Subscribe(x => TapCountDisplay(x));
    }

    private void TapCountDisplay(int num)
    {
        _tapText.text = num.ToString("00");
    }
}
