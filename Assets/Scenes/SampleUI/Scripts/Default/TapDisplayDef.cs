using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TapDisplayDef : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tapText;

    public void TapCountDisplay(int num)
    {
        _tapText.text = num.ToString("00");
    }
}
