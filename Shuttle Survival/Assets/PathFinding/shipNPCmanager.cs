using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipNPCmanager : MonoBehaviour
{
    //---------------------------------------------------------------------------
    //No touch zone cuz instance zone
    public static shipNPCmanager NPCmanagInstance;
    //GameManager gameM = GameManager.GM;

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

    public bool IsNPCavailable()
    {
        if (GameManager.selection) {
            return GameManager.selection.Dispo;
            //Question cancel action
        } else
        {
            return false;
        }             
    }
    public void Start()
    {

    }
    public void NeedAHandOverHere(Transform targetTHATneedHELP)
    {
        Debug.Log("INCOMING");
        //choisit un npc envoie le à la bonne place        
        //BobDoSomething(targetTHATneedHELP);   
        GameManager.selection.gameObject.GetComponent<PathFinding>().BobDoSomething(targetTHATneedHELP);
        GameManager.selection.Dispo= false;
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
