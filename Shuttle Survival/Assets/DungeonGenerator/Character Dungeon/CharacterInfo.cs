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
    [SerializeField] public CharacterPerk perk1, perk2;
    [SerializeField] public CharacterTool equipedTool;
    [SerializeField] public int strength;
    [SerializeField] public int tinkering;
    [SerializeField] public int charisma;
    [SerializeField] public int defence;

    public CharacterInfo(string characterName, Sprite characterSprite, int maxHp, int currentHp, int backPackCapacity, CharacterPerk perk1, CharacterPerk perk2, CharacterTool equipedTool, int strength,int defence, int tinkering, int charisma)
    {
        this.defence = defence;
        this.characterName = characterName;
        this.characterSprite = characterSprite;
        this.maxHp = maxHp;
        this.currentHp = currentHp;
        this.backPackCapacity = backPackCapacity;
        this.perk1 = perk1;
        this.perk2 = perk2;
        this.equipedTool = equipedTool;
        this.strength = strength;
        this.tinkering = tinkering;
        this.charisma = charisma;
    }
}
