using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillsManager : MonoBehaviour
{
    private int experience = 0;
    public int level = 0;
    private int expToNext = 2;
    public int AbilityPoints = 0;
    public PrerequisObj[] skills;
    [SerializeField] GameObject tree;
    public int Experience { get => experience; set => experience = value; }
    //sur les perso

    private void Start()
    {
        CharacterPerk[] temp = tree.transform.GetComponentsInChildren<CharacterPerk>();
        skills = new PrerequisObj[temp.Length];
        for (int i = 0; i<temp.Length; i++)
        {
            skills[i] = new PrerequisObj(temp[i].perkType);
        }
        
    }
    public void LevelUp()
    {
        level++;
        AbilityPoints++;
        expToNext += (level * 2);//2/2(4)/4(8)/6(14)
    }

    public void ExpUP(int xp)
    {
        experience += xp;
        if(experience >= expToNext) 
        { 
            LevelUp(); 
            //Visuel de lvl up
        }
    }

    public bool HasSkill(CharacterPerkType type)
    {
        foreach (PrerequisObj perk in skills)
        {
            if(type == perk.type)
            {
                return perk.actif;
            }
        } 
        return false;
    }

    public void AddSkill(CharacterPerkType type)
    {
        foreach (PrerequisObj perk in skills)
        {
            if (type == perk.type)
            {
                perk.actif = true;
            }
        }
    }

    public void AddSkill(int pos)
    {
        skills[pos].actif = true;
    }


   
}
