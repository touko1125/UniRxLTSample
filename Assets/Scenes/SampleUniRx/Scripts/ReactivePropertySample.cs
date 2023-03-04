using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class ReactivePropertySample : MonoBehaviour
{
    //ReactivePropertyの宣言
    private ReactiveProperty<int> _hogeReactiveProperty = new ReactiveProperty<int>();

    private int[] _hogeNums = new int[10] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 };
    
    void Start()
    {
        SetReactiveEvent();
        SetRepeatEvent();
    }

    //配列の要素数分、ReactivePropertyの値を更新する繰り返し処理をUniRxで書いたもの。
    //こう言った処理はUniRxである必要はないけど、こうもかけるよーということ
    private void SetRepeatEvent()
    {
        Observable
            .Range(0, _hogeNums.Length)
            .Subscribe(x => _hogeReactiveProperty.Value = _hogeNums[x])
            .AddTo(this);
    }

    private void SetReactiveEvent()
    {
        //単純に値変更を検知
        _hogeReactiveProperty.Subscribe(x => Hoge());
        
        //変更後の値を取得
        _hogeReactiveProperty.Subscribe(x => HogeWithArgument(x));
        
        //変更後の値を指定の条件(2で割り切れるか)でフィルタリング
        _hogeReactiveProperty
            .Where(x => x % 2 == 0)
            .Subscribe(_ => HogeEven());

        //変更後の値が2で割り切れる場合は2で割って値を取得
        _hogeReactiveProperty
            .Where(x => x % 2 == 0)
            .Select(x => x / 2)
            .Subscribe(x => HogeDevidedEven(x));
    }

    private void Hoge()
    {
        Debug.Log("Value Changed!");
    }

    private void HogeWithArgument(int hoge)
    {
        Debug.Log("New Value : " + hoge);
    }

    private void HogeEven()
    {
        Debug.Log("Value is Even");
    }

    private void HogeDevidedEven(int devided)
    {
        Debug.Log("Devided Value : " + devided);
    }
}
