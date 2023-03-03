using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class TapCounterUniRx : MonoBehaviour
{
    private ReactiveProperty<int> _tapNum = new ReactiveProperty<int>();
    public IReadOnlyReactiveProperty<int> TapNum => _tapNum;

    [SerializeField] private Button _tapButton;
    // Start is called before the first frame update
    void Start()
    {
        //ボタンの押し込みをイベントとして変換し、このイベントが発生時の処理をSubscribe内で指定
        _tapButton.OnClickAsObservable().Subscribe(_ => PlusTapNum());
    }

    private void PlusTapNum()
    {
        _tapNum.Value++;
    }
}
