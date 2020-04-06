﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DungeonOptionRequirementsHandler
{  
    public static bool IsRequirementMet(List<DungeonOptionRequirement> requirementsList)
    {
        bool passedAllTests = true;
        List<DungeonCharacterUI> partyCharacters = DungeonCharacterManager.dungeonCharacterManager.GetActivePartyCharacters();
        List<DungeonCharacterUI> currentListOfPassingCharacters = new List<DungeonCharacterUI>(partyCharacters);
        foreach (DungeonOptionRequirement requirementToMeet in requirementsList)
        {
            int numberOfPassingCharacter = 0;
            if (passedAllTests == false) return passedAllTests;
            
            switch (requirementToMeet.statsToCheck)
            {
                case DungeonStatsToCheck.StrengthSingle:
                    foreach (DungeonCharacterUI characterUI in partyCharacters)
                    {                       
                        if (characterUI.GetLinkedCharacter().Strenght >= requirementToMeet.quantityRequired)
                        {
                            numberOfPassingCharacter++;
                        }
                        else
                        {
                            currentListOfPassingCharacters.Remove(characterUI);
                        }
                    }
                    passedAllTests = numberOfPassingCharacter > 0;
                    break;
                case DungeonStatsToCheck.HealthAmountSingle:
                    foreach (DungeonCharacterUI characterUI in partyCharacters)
                    {
                        if (characterUI.GetLinkedCharacter().CurrHp >= requirementToMeet.quantityRequired)
                        {
                            numberOfPassingCharacter++;
                        }
                        else
                        {
                            currentListOfPassingCharacters.Remove(characterUI);
                        }
                    }
                    passedAllTests = numberOfPassingCharacter > 0;
                    break;
                case DungeonStatsToCheck.HealthPercentageSingle:
                    foreach (DungeonCharacterUI characterUI in partyCharacters)
                    {
                        if (((float)characterUI.GetLinkedCharacter().CurrHp / (float)characterUI.GetLinkedCharacter().HPMax >= requirementToMeet.quantityRequired))
                        {
                            numberOfPassingCharacter++;
                        }
                        else
                        {
                            currentListOfPassingCharacters.Remove(characterUI);
                        }
                    }
                    passedAllTests = numberOfPassingCharacter > 0;
                    break;
                case DungeonStatsToCheck.Gold:
                    break;
                case DungeonStatsToCheck.HasHammer:
                    break;
                case DungeonStatsToCheck.HasEngineerToolbox:
                    foreach (DungeonCharacterUI characterUI in partyCharacters)
                    {
                        passedAllTests = (characterUI.GetLinkedCharacter().EquipedTool.GetToolType() == CharacterToolType.EngineersToolbox);
                        if (passedAllTests == true) break;
                    }
                    break;
                case DungeonStatsToCheck.HasDigitalKey:
                    break;
                case DungeonStatsToCheck.StrengthParty:
                    int partyStrength = 0;
                    foreach (DungeonCharacterUI characterUI in partyCharacters)
                    {
                        partyStrength += characterUI.GetLinkedCharacter().Strenght;                        
                    }
                    passedAllTests = partyStrength >= requirementToMeet.quantityRequired;
                    break;
                case DungeonStatsToCheck.TinkeringSingle:
                    foreach (DungeonCharacterUI characterUI in partyCharacters)
                    {
                        if (characterUI.GetLinkedCharacter().Tinkering >= requirementToMeet.quantityRequired)
                        {
                            numberOfPassingCharacter++;
                        }
                        else
                        {
                            currentListOfPassingCharacters.Remove(characterUI);
                        }
                    }
                    break;
                case DungeonStatsToCheck.TinkeringParty:
                    int partyTinkering = 0;
                    foreach (DungeonCharacterUI characterUI in partyCharacters)
                    {
                        partyTinkering += characterUI.GetLinkedCharacter().Tinkering;
                    }
                    passedAllTests = partyTinkering >= requirementToMeet.quantityRequired;
                    break;
                case DungeonStatsToCheck.CharismaSingle:
                    foreach (DungeonCharacterUI characterUI in partyCharacters)
                    {
                        if (characterUI.GetLinkedCharacter().Charisma >= requirementToMeet.quantityRequired)
                        {
                            numberOfPassingCharacter++;
                        }
                        else
                        {
                            currentListOfPassingCharacters.Remove(characterUI);
                        }
                    }
                    break;
                case DungeonStatsToCheck.CharismaParty:
                    int partyCharisma = 0;
                    foreach (DungeonCharacterUI characterUI in partyCharacters)
                    {
                        partyCharisma += characterUI.GetLinkedCharacter().Charisma;
                    }
                    passedAllTests = partyCharisma >= requirementToMeet.quantityRequired;
                    break;
            }           
        }
        if (passedAllTests)
        {
            Debug.Log(currentListOfPassingCharacters.Count);
            DungeonOptionsHandler.dungeonOptionsHandler.SetCurrentListOfPassingCharacters(currentListOfPassingCharacters);
        }
        return passedAllTests;
    }
}