using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipNPCmanager : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AvailableNPC(PathFinding NPC)
    {
        ListNPCship.Add(NPC);
        Debug.Log("NPC ADDED TO THE WILLING LIST");
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
        //choisit un npc envoie le à la bonne place
        ListNPCship[0].BobDoSomething(targetTHATneedHELP);
        RemoveNPC(ListNPCship[0]);
    }
}
