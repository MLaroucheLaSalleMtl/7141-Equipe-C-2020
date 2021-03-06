﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventaire : MonoBehaviour 
{
    public static Inventaire inventaire;

    [SerializeField] ItemClass[] ressources;//note ID negatif = item de debug, ID = 0 : item Vide
    /*[HideInInspector]*/ public ItemStack[] inventoryContent;
    [SerializeField] private ResourcesPack cost;

    //ItemStack objet;

    private void Awake()
    {
        if(inventaire == null)
        {
            inventaire = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {   
        RemoveNulls();
        //PayRessource(cost);
    }

    public ItemStack AddItem(ItemStack stackWork)//cette fonction a besoin de retouner un ItemStack si on veut pouvoir transférer les objet d'un inventaire à un autre
    {
        List<int> posVide = new List<int>();
        List<int> posID = new List<int>();
        for (int i = 0; i < inventoryContent.Length; i++)
        {
            if(inventoryContent[i].Item.ItemID == stackWork.Item.ItemID)
            {
                posID.Add(i);
            }
            if(inventoryContent[i].Item.ItemID == -1)
            {
                posVide.Add(i);
            }
        }
        //cherche à remplir les stacks existants
        for(int pos = 0; pos<posID.Count; pos++) 
        {
            int x = posID[pos];
            if (stackWork.Quantite+inventoryContent[x].Quantite > stackWork.Item.MaxStack)
            {
                stackWork.Quantite -= stackWork.Item.MaxStack - inventoryContent[x].Quantite;
                inventoryContent[x].Quantite = inventoryContent[x].Item.MaxStack;
            }
            else
            {
                inventoryContent[x].Quantite += stackWork.Quantite;
                
                return new ItemStack(0,ressources[0]);
            }
        }
        //rempli les stacks vides
        for(int pos = 0; pos<posVide.Count; pos++)
        {
            int x = posVide[pos];
            if (stackWork.Quantite > stackWork.Item.MaxStack)
            {
                inventoryContent[x] = new ItemStack(stackWork.Item.MaxStack ,stackWork.Item);
                stackWork.Quantite -= stackWork.Item.MaxStack;
            }
            else
            {
                inventoryContent[x] = new ItemStack(stackWork.Quantite, stackWork.Item);
                return new ItemStack(0, ressources[0]);
            }
        }
        return stackWork;//retourne le stack qui reste s'il n'a pas pu etre rangé dans l'inventaire
    }


     public ItemStack IdentifyStackItem(int ID, int qte)
    {
        ItemStack stackWork = new ItemStack(0, ressources[0]);
        for (int pos = 0; pos < ressources.Length; pos++)
        {

            if (ID == ressources[pos].ItemID)
            {
                stackWork = new ItemStack(qte, ressources[pos]);
            }
        }//va chercher un ItemClass à partir d'un ID et le combine avec la quantité pour créer un ItemStack necessaire pour traiter les données
        return stackWork;
    }

    #region

    public void RemoveNulls()
    {
        for (int i = 0; i < inventoryContent.Length; i++) {
            if(inventoryContent[i].Quantite == 0) {
                inventoryContent[i] = IdentifyStackItem(0, 0);
            }  
        } 
    }

    public void AddStackOnCase(int pos, ItemStack stack)
    {
        if (inventoryContent[pos] == null)
        {
            inventoryContent[pos] = stack;
        }
    }
 
    public void RemoveAllEmptyStacks()
    {
        for(int i= 0; i < inventoryContent.Length; i++)
        {
            if(inventoryContent[i].Quantite <= 0)
            {
                inventoryContent[i] = IdentifyStackItem(0,0);
            }
        }
    }

    public void RemoveEmptyStack(int id)
    {
        for (int i = 0; i < inventoryContent.Length; i++)
        {
            if (inventoryContent[i].Quantite <= 0)
            {
                inventoryContent[i] = null;
            }
        }
    }
    #endregion

    public int GetAmount(int ID)
    {
        int qte = 0;
        for(int i = 0; i< inventoryContent.Length; i++)
        {
            if(ID == inventoryContent[i].Item.ItemID)
            {
                qte += inventoryContent[i].Quantite;
            }
        }
        return qte;
    }

    public bool PayRessource(ResourcesPack resourcesCost)
    {

        ItemClass[] ID = new ItemClass[resourcesCost.resources.Length];

        int[] qte = new int[resourcesCost.resources.Length];
        for (int i = 0; i < resourcesCost.resources.Length; i++)
        {
            ID[i] = resourcesCost.resources[i].Item;
  
            qte[i] = resourcesCost.resources[i].Quantite;
        }

        List<int> agglutine = new List<int>();

        for (int i = 0; i < ID.Length; i++)
        {
            agglutine.Add(0);
            for (int a = 0; a < inventoryContent.Length; a++)
            {
                //Debug.Log(inventory[a].Item.ItemID);
                //Debug.Log(ID[i]);
                    if (inventoryContent[a].Item.ItemID == ID[i].ItemID)
                    {
                        agglutine[i] += inventoryContent[a].Quantite;//donne la quantite total d un item dans l inventaire 
                        inventoryContent[a].Quantite = 0;
                    }
                
            }
            if (agglutine[i] < qte[i]) 
            {
                for(int b = 0; b <= i; b++) { 
                    AddItem(new ItemStack(agglutine[b],ID[b]));
                    //remet les stacks dans l'inventaire
                    //dès qu'on manque d'une ressource on sort de la fonction
                }
                return false;/*MESSAGE "N'A PAS ASSEZ DE LA RESSOURCE IdentifyStackItem(ID[i]).Item.Name"*/
            }                      
        }

        for(int pos = 0; pos < agglutine.Count; pos++) 
        {
            agglutine[pos] -= qte[pos];
            if(agglutine[pos] > 0)
            {
                AddItem(new ItemStack(agglutine[pos], ID[pos]));
                RemoveAllEmptyStacks();
            }
            
        }
        return true;
    }

    //formule simplifié pour payer 1 seule ressource
    public bool PayFromID(int id,int qte)
    {
        int agglutine = 0;
        for(int i = 0; i< inventoryContent.Length; i++)
        {
            ItemStack x = inventoryContent[i];
            if(id == x.Item.ItemID)
            {
                if (qte < x.Quantite) { 
                    x.Quantite -= qte; 
                    return true; 
                } else 
                {
                    agglutine += x.Quantite;
                    x.Quantite = 0;
                }
            }
        }
        if(agglutine > qte)
        {
            agglutine -= qte;
            AddItem(IdentifyStackItem(id, agglutine));
            RemoveAllEmptyStacks();
            return true;
        }
        AddItem(IdentifyStackItem(id, agglutine));
        return false;
    }

    public void AddManyResources(ResourcesPack resourcesToAdd)
    {
        for (int i = 0; i < resourcesToAdd.resources.Length; i++)
        {
            ItemStack itemStack = new ItemStack(resourcesToAdd.resources[i].Quantite, resourcesToAdd.resources[i].Item);
            AddItem(itemStack);
        }
    }
}
