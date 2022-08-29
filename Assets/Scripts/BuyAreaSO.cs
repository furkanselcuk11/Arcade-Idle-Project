using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BuyArea Type", menuName = "BuyAreaSO")]
public class BuyAreaSO : ScriptableObject
{
    [SerializeField] private int _cost;    // Sat�n al�nacak ��enin maliyeti
    [SerializeField] private int _currentMoney; // �denen para miktar�
    [SerializeField] private bool _locked = true;

    public int currentMoney
    {
        get { return _currentMoney; }
        set { _currentMoney = value; }
    }
    public int cost
    {
        get { return _cost; }
        set { _cost = value; }
    }
    public bool locked
    {
        get { return _locked; }
        set { _locked = value; }
    }
}
