using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkButton : MonoBehaviour
{
    private Button travail;
    //[SerializeField] Button trav;
    [SerializeField] Color disCol;
    [SerializeField] Image perkImg;
    public CharacterPerk thisPerk;

    

    private void Start()
    {
        GameManager.GM.OnSkillOpen += open;
    }

    private void open(object sender, EventArgs e)
    {
        perkImg.sprite = thisPerk.perkSprite;

        //check pour eviter les erreurs
        travail = this.GetComponent<Button>();
        travail.onClick.RemoveAllListeners();
        if (GameManager.selection)
        {
            CharacterSkillsManager thisChar = GameManager.selection.gameObject.GetComponent<CharacterSkillsManager>();

            if (thisChar.HasSkill(thisPerk.perkType))//disable si il a deja le skill
            {
                ButtonUnSelectable();
            }
            else
            {
                if (!thisChar.HasSkill(thisPerk.preRequis.perkType) && thisPerk.preRequis != thisPerk)//disable si il a pas le prerequis et que c'est pas le premier skill
                {
                    ButtonUnSelectable();
                }
                else
                {
                    Debug.Log("Bouton accessible");
                    travail.interactable = true;
                    travail.onClick.AddListener(() =>
                    {

                        if (thisChar.AddSkill(thisPerk.perkType))
                        {
                            ButtonUnSelectable();
                            GameManager.GM.OnSkillOpen_Caller();
                        }

                    });
                }
            }
        }
    }


    private void ButtonUnSelectable()
    {
        travail.interactable = false;
        ColorBlock temp = travail.colors;
        temp.disabledColor = disCol;
        travail.colors = temp;
    }
    
}
//il faut que je déactive les boutons lorsque le prérequis n'est pas activé
//désactivé le bouton une fois qu'il a été activé
//le check du prérequis doit passer par le personnage
//level dans la scene espace, pour pouvoir utiliser le Selection

//selection.getcomponent<CharacterSkillManager>().HasSkill(boutonskills[i].CharacterPerk.Prerequis.PerkType)
//