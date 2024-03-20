using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Food,
    Weapon,
    Tools,
    Armor
}
public enum Attributes
{
    CritChance,
    ATK,
    DEF,
    HP
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Items : ScriptableObject
{
    public int Id;
    public string Name;
    public ObjectType Type;
    public Sprite UIDisplay;
    [TextArea(15, 20)]
    public string description;

    public EquipBuffs buffs;
    public string LinkOnPrefab;
}

[System.Serializable]
public class EquipBuffs
{
    public Attributes buff;
    public float value;
}
