using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillsManager : MonoBehaviour
{
    private int experience = 0;
    public int level = 0;
    private int expToNext = 2;
    public int AbilityPoints = 0;
    public readonly int maxLvl = 6;
    public PrerequisObj[] skills;
    private int xpMultiplier = 1;
    [SerializeField] private GameObject tree;
    public int Experience { get => experience; set => experience = value; }
    public int ExpToNext { get => expToNext;}

    //sur les perso

    private void Start()
    { 
        
        PerkButton[] temp2 = tree.transform.GetComponentsInChildren<PerkButton>();
        skills = new PrerequisObj[temp2.Length];      
        for (int i = 0; i<temp2.Length; i++)
        {
            skills[i] = new PrerequisObj(temp2[i].thisPerk.perkType);
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
        
        experience += xp*xpMultiplier;
        if(experience >= expToNext) 
        { 
            LevelUp(); 
            //Visuel de lvl up
        }
    }

    public float XpMarginal()
    {
        float b = expToNext - 2 * level;
        return b;
    }

    public float XpLast()
    {
        if(level> 0) {
            float b = 2;
            b += (level-1) * 2;
        return b;
        }
        else
        {
            return 0;
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

    public bool AddSkill(CharacterPerkType type)
    {
        if (AbilityPoints > 0) 
        { 
            AllSkills skillSwitch = new AllSkills();
            skillSwitch.SkillSwitch(type, this);           
            foreach (PrerequisObj perk in skills)
            {
                if (type == perk.type)
                {
                    AbilityPoints--;
                    perk.actif = true;
                    if (type == CharacterPerkType.Looter)
                        RandomisedLootDecrypter.GetInstance().ActivateModifier();
                    return true;
                }
            }
            
        }

        return false;
    }

    public void AddSkill(int pos)
    {
        skills[pos].actif = true;
        
    }

    public void XpMult(int x)
    {
        xpMultiplier = x;
    }
   
}
