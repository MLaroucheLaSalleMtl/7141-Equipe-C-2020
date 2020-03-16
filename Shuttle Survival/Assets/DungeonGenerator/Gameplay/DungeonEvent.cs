using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon/Dungeon Event")]
public class DungeonEvent : ScriptableObject
{
    [SerializeField] public string eventName;
    [TextArea(2,5)]
    [SerializeField] public string eventMessage;
    [SerializeField] public List<DungeonOption> dungeonOptions = new List<DungeonOption>();
    [SerializeField] public List<DungeonEffect> eventEffects = new List<DungeonEffect>();
    
}
