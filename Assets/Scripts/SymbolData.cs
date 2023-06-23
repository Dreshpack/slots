using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SymbolData", menuName = "Scriptable objects/Symbol")]
public class SymbolData : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private SymbolType type;
    [SerializeField] private ushort dropChance;

    public SymbolType GetSymbolType()
    {
        return type;
    }

    public Sprite GetSprite()
    {
        return icon;
    }

    public ushort GetDropChance()
    {
        return dropChance;
    }

}
