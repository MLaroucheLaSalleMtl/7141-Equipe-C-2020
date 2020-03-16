using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DungeonOptionRequirement
{
    [SerializeField] public DungeonStatsToCheck statsToCheck;
    [SerializeField] public float quantityRequired;
}
