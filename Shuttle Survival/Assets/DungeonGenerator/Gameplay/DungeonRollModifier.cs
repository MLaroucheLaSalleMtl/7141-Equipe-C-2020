using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DungeonRollModifier
{
    [SerializeField] public DungeonStatsToCheck statsToCheck;
    [SerializeField] public float percentIncreasePerRelatedStatsPoint;
    public bool decreaser = false; // do we decrease the chance of success per stats or increase it
    public bool partyWide = false; //if true, count the stats on all active members, else, only on the selected character
}
