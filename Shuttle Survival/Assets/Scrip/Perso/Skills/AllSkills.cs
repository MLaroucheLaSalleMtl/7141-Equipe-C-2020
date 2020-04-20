using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSkills
{
    //le singleton plz

   public void SkillSwitch(CharacterPerkType type, CharacterSkillsManager perso)
    {
        CharacterSystem persoSys = perso.GetComponent<CharacterSystem>();
        switch (type)
        {
            
            #region tous
            case CharacterPerkType.Base:
                persoSys.IncreaseHP(1);
                break;

            case CharacterPerkType.Backpacker:
                persoSys.BackpackCapacity += 1;
                break;
            #endregion

            #region combat
            case CharacterPerkType.Combat:
                persoSys.IncreaseHP(2);
                break;

            case CharacterPerkType.Attack:
                persoSys.Strenght += 2;
                break;

            case CharacterPerkType.Defence:
                persoSys.Defence += 2;
                break;

            #endregion
            #region ingenieur
            case CharacterPerkType.Ingenieur:
                persoSys.Tinkering = true;
                break;

            case CharacterPerkType.Hacker:
                Debug.Log("Hacker");
                //utiliser le hasSkill
                break;

            case CharacterPerkType.Looter:
                //help g besoin de yordan
                break;

            #endregion
            case CharacterPerkType.Leader:
                perso.XpMult(2);
                break;

            case CharacterPerkType.Soignant:
               persoSys.ChangeHealValue(4);
                break;

            case CharacterPerkType.SmoothTalker:
                persoSys.Charisma = true;
                break;
               
            
            default:
                Debug.Log("OwO whats dis");
                break;
        }

    }
}
