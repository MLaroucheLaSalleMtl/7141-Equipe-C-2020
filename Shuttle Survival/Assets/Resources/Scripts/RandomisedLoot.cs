using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomisedLoot
{
    [SerializeField] public ItemStack[] resourcesPossible;
    [Range(0f, 1f)]
    [SerializeField] public float[] chancesToGive;
    [Range(0f, 1f)]
    [SerializeField] public float[] resourcesVariation;
}
