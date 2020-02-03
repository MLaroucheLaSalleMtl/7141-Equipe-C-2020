using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack 
{
    private ItemClass item;
    private int quantite;

    public int Quantite { get => quantite; set => quantite = value; }
    public ItemClass Item { get => item; set => item = value; }

    public ItemStack(int quantite, ItemClass item)
    {
        this.item = item;
        this.quantite = quantite;
    }
}
