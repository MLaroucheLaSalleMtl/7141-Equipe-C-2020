using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Related/Character Perk")]
public class CharacterPerk : ScriptableObject
{
    public CharacterPerk preRequis;
    [SerializeField] public Sprite perkSprite;
    [SerializeField] public CharacterPerkType perkType;
    [TextArea(2,4)]
    [SerializeField] public string perkDescription;
}
