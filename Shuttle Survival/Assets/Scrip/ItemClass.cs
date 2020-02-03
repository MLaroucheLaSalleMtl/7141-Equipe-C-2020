using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName  ="Item")]
public class ItemClass : ScriptableObject
{
    public Sprite icon;
    public string description;   
    public string nom;
    public int itemID;
    public int maxStack;
}
