using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStack 
{
    [SerializeField] private ItemClass item;
    [SerializeField] private int quantite;

    public int Quantite { get => quantite; set => quantite = value; }
    public ItemClass Item { get => item; set => item = value; }

    public ItemStack(int quantite, ItemClass item)
    {
        this.item = item;
        this.quantite = quantite;
    }
}

[System.Serializable]
public class ResourcesCost
{
    [SerializeField] public ItemStack[] resources;
    
}
