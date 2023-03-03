using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapCounterDef : MonoBehaviour
{
    private int _tapNum;
    public int TapNum => _tapNum;   //タップ回数の読み取り専用プロパティ

    private bool _stopTap = false;

    public bool StopTap
    {
        set { _stopTap = value; }
    }

    [SerializeField] private TapDisplayDef _tapDisplayDef;
    
    // Start is called before the first frame update
    void Start()
    {
        _tapNum = 0;
    }

    public void OnTapped()
    {
        if (_stopTap) return;
        
        _tapNum++;
        
        _tapDisplayDef.TapCountDisplay(_tapNum);
    }
}
