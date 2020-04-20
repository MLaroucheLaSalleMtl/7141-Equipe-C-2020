using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInfo
{
    [SerializeField] public string characterName;
    [SerializeField] public Sprite characterSprite;
    [SerializeField] public int maxHp;
    [SerializeField] public int currentHp;
    [SerializeField] public int backPackCapacity;
    [SerializeField] private CharacterPerk[] perksChar;
    [SerializeField] private CharacterPerk[] perksTool;
   // [SerializeField] public CharacterTool equipedTool;
    [SerializeField] public int strength;
    [SerializeField] public bool tinkering;
    [SerializeField] public bool charisma;
    [SerializeField] public int defence;

    public CharacterPerk[] PerksChar { get => perksChar; set => perksChar = value; }
    //public CharacterPerk[] PerksTool { get => perksTool; set => perksTool = value; }
    //pas eu le temps parceque j'étais trop patate pendant la quarantaine :/ s: E.D
    public CharacterInfo(string characterName, Sprite characterSprite, int maxHp, int currentHp, int backPackCapacity, CharacterPerk[] perks, int strength,int defence, bool tinkering, bool charisma)
    {
        this.defence = defence;
        this.characterName = characterName;
        this.characterSprite = characterSprite;
        this.maxHp = maxHp;
        this.currentHp = currentHp;
        this.backPackCapacity = backPackCapacity;
        this.PerksChar = perks;
        
        this.strength = strength;
        this.tinkering = tinkering;
        this.charisma = charisma;
    }
}
