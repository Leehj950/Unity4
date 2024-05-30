using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable
}

public enum ConsumableType
{
    health,
    Stamina,
    Speed,
}

[Serializable]
public class ItmeDateConsumbale
{
    public ConsumableType tyep;
    public float value;
}


[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemDate : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;

    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItmeDateConsumbale[] consumbales;
}
