using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire : MonoBehaviour
{
    [SerializeField] private static int taille = 1;
    private ItemStack[] inventory = new ItemStack[taille];
    private ItemStack[] workInventory = new ItemStack[taille];
    //ItemStack objet;

    public ItemStack CreateNewStack(ItemClass item, int qte)
    {
        ItemStack stackWork = new ItemStack(qte, item);
        return stackWork ;
    }

    public void AddStack(ItemStack stackWork)
    {
        int vide = -1;
        for (int i = inventory.Length; i >= 0; i--)
        {
            //va donner la première case vide du tableau
            if(inventory[i] == null)
            {
                vide = i;
            }
            stackWork = MergeStacks(stackWork, i);
        }
        if(vide != -1 && stackWork.Quantite > 0)
        {
            inventory[vide] = stackWork;
        }
    }

    public ItemStack MergeStacks(ItemStack stackWork, int pos)
    {
        if (inventory[pos].Item.itemID == stackWork.Item.itemID)
        {
            int x;
            x =  stackWork.Quantite + inventory[pos].Quantite;

            if (x > stackWork.Item.maxStack) 
            { 
                stackWork.Quantite = x - stackWork.Item.maxStack;
                inventory[pos].Quantite = inventory[pos].Item.maxStack;
            }
            else
            {
                inventory[pos].Quantite = x;
                stackWork.Quantite = 0;
            }
        }        
        return stackWork;
    }
 
}
