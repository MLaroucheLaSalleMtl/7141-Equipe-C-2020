using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipNPCmanager : MonoBehaviour
{
    //c'est quoi cette merde? regarde les scriptes qu'on a déjà
    //---------------------------------------------------------------------------
    //No touch zone cuz instance zone
    public static shipNPCmanager NPCmanagInstance;
    private void Awake()
    {     
        if (NPCmanagInstance == null)
        {
            NPCmanagInstance = this;
        }
        else if(NPCmanagInstance != this)
        {
            Destroy(this);  //EXPLOOOOOOOOOSIOOOOOOOON
        }
    }
    //---------------------------------------------------------------------------

    private List<PathFinding> ListNPCship = new List<PathFinding>();  

    public void AvailableNPC(PathFinding NPC)
    {
        ListNPCship.Add(NPC);
    }

    public void RemoveNPC(PathFinding NPC)
    {
        for (int z = 0; ListNPCship.Count >= z; z++ ) {
            if (NPC == ListNPCship[z])
            {
                ListNPCship.RemoveAt(z);

            }
        }        
    }

    public bool IsNPCavailable()
    {
        //vérifie si un npc est disponible et en mode listenning
        if (ListNPCship.Count > 0)
        {
            return true;
        }else
        {
            return false;
        }
        
    }

    public void NeedAHandOverHere(Transform targetTHATneedHELP)
    {
        Debug.Log("INCOMING");
        //choisit un npc envoie le à la bonne place        
        ListNPCship[0].BobDoSomething(targetTHATneedHELP);
        RemoveNPC(ListNPCship[0]);
        
    }

    /*if (NPC.IsNPCavailable() == true) {
                Debug.Log("Sending bob");
                CODE BLAHBLAH
                //trouver le bon endroit pour envoyer bob      
                NPC.NeedAHandOverHere(gameObject.transform);
            }else{
                MessagePopup.MessagePopupManager.SetStringAndShowPopup("Select someone to go upgrade");
                //plus tard on pourrait peut-être mettre ici le drop down list avec toutes les persos
            } */
}
