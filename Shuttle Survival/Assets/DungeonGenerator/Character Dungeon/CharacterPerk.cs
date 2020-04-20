using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Perk")]
public class CharacterPerk : ScriptableObject
{
    
    public CharacterPerk preRequis;
    [SerializeField] public Sprite perkSprite;
    [SerializeField] public CharacterPerkType perkType;
    [TextArea(2,4)]
    [SerializeField] public string perkDescription;
}

public class CharPerkClass : MonoBehaviour
{
    public CharacterPerk perk;
}


// 1per - le perk. 1autre perso - une autre instance du perk//n instance du perk <- dégoutant
//le perk -les persos vont chercher //1 instance du perk