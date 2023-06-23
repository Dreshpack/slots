using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Symbol : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private SymbolData[] allSymbols = new SymbolData[6];
    [SerializeField] private SymbolType type;

    private void OnEnable()
    {
        Array.Sort(allSymbols,
            delegate(SymbolData x, SymbolData y) { return x.GetDropChance().CompareTo(y.GetDropChance()); });
        WheelManager.wheel += Spin;
    }

    public SymbolType GetType()
    {
        return type;
    }

    public void Visibility(bool value)
    {
       this.transform.GetChild(0).gameObject.SetActive(value);
    }
    
    public void SetInfo(SymbolData data)
    {
        type = data.GetSymbolType();
        icon.sprite = data.GetSprite();
    }

    public void Spin()
    {
        int chance = Random.Range(0, 10000);
        foreach (var symbolData in allSymbols)
        {
            if (CompareChance(chance, symbolData.GetDropChance()))
            {
                SetInfo(symbolData);
                return;
            }
        }
    }

    private bool CompareChance(int randChance, int dropChance)
    {
        return randChance <= dropChance;
    }
}
