using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName  ="Item")]
public class ItemClass : ScriptableObject
{
    public Sprite icon;
    [TextArea(2,4)]
    [SerializeField] private string description;
    [SerializeField] private string nom;
    [SerializeField] private int itemID;
    [SerializeField] private int maxStack;
    [SerializeField] private ItemTier itemType;
    public enum ItemTier { Debug,Vide,Consum, Tier1, Tier2, Tier3, Tier4 };
    public string Description { get => description;}
    public string Nom { get => nom;}
    public int ItemID { get => itemID;}
    public int MaxStack { get => maxStack;}
    public ItemTier ItemType { get => itemType;}
}
