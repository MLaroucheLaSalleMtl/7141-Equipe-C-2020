using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DungeonEffect
{
    [SerializeField] public DungeonEffectType dungeonEffectType;
    [SerializeField] public float dungeonEffectIntensity;
    [SerializeField] public ItemStack[] specificItemsToReceive;
    [SerializeField] public RandomisedLoot randomisedLoot;
}
