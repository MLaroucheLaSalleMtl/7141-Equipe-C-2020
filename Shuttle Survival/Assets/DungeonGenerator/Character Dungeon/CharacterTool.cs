using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Related/Character Tool")]
public class CharacterTool : ScriptableObject
{
    [SerializeField] public Sprite toolSprite;
    [SerializeField] public CharacterToolType toolType;
    [TextArea(2, 4)]
    [SerializeField] public string toolDescription;
}
