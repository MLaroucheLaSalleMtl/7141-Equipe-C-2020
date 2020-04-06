using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkButton : MonoBehaviour
{
    [SerializeField] GameObject[] bouttonsSkill;
    //[SerializeField] Button trav;
    [SerializeField] Color disCol;
    private void Awake()
    {
       // disable les bouton des skills
        Button travail = this.GetComponent<Button>();
        CharacterPerk thisPerk = this.GetComponent<CharacterPerk>();
        CharacterSkillsManager thisChar = GameManager.selection.GetComponent<CharacterSkillsManager>();

        /*Debug.Log(this.GetComponent<Button>());            
        Debug.Log(travail);   */
        if (!thisChar.HasSkill(thisPerk.preRequis.perkType))            
        {                
            travail.interactable = false;
        }
        else
        {

        }
        /*else  //si le prérequis est actif
        {               
            if (obj.GetComponent<CharacterPerk>().actif) { 
                travail.interactable = false;
                ColorBlock temp = travail.colors;
                temp.disabledColor = disCol;
                travail.colors = temp;
            }
            else {                 
                travail.interactable = true;
                Selected(obj.GetComponent<Button>());
            }
        } */


    }

    private void Selected(Button work)
    {
        
    }
}
//il faut que je déactive les boutons lorsque le prérequis n'est pas activé
//désactivé le bouton une fois qu'il a été activé
//le check du prérequis doit passer par le personnage
//level dans la scene espace, pour pouvoir utiliser le Selection

//selection.getcomponent<CharacterSkillManager>().HasSkill(boutonskills[i].CharacterPerk.Prerequis.PerkType)
//