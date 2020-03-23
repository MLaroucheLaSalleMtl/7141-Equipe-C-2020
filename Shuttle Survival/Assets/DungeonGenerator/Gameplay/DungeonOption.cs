using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon/Dungeon Option")]
public class DungeonOption : ScriptableObject
{    
    public string optionName;
    [TextArea(2, 2)]
    [SerializeField] public string optionText;
    [SerializeField] public bool chooseCharacterForOption = false;
    [SerializeField] public List<DungeonOptionRequirement> requirementsToBeActive = new List<DungeonOptionRequirement>();
    [SerializeField] public DungeonRoll relatedDungeonRoll = new DungeonRoll();    
}
