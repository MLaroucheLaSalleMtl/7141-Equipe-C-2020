using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DungeonOptionRequirementsHandler
{  
    public static bool IsRequirementMet(List<DungeonOptionRequirement> requirementsList)
    {
        bool passedAllTests = true;
        foreach (DungeonOptionRequirement requirementToMeet in requirementsList)
        {
            if (passedAllTests == false) return passedAllTests;
            List<DungeonCharacterUI> partyCharacters = DungeonCharacterManager.dungeonCharacterManager.GetActivePartyCharacters();
            switch (requirementToMeet.statsToCheck)
            {
                case DungeonStatsToCheck.StrengthSingle:
                    foreach (DungeonCharacterUI characterUI in partyCharacters)
                    {
                        passedAllTests = characterUI.GetLinkedCharacter().Strenght >= requirementToMeet.quantityRequired;      
                        if (passedAllTests == true) break;
                    }
                    break;
                case DungeonStatsToCheck.HealthAmountSingle:
                    foreach (DungeonCharacterUI characterUI in partyCharacters)
                    {
                        passedAllTests = characterUI.GetLinkedCharacter().CurrHp >= requirementToMeet.quantityRequired;
                        if (passedAllTests == true) break;
                    }
                    break;
                case DungeonStatsToCheck.HealthPercentageSingle:
                    foreach (DungeonCharacterUI characterUI in partyCharacters)
                    {
                        passedAllTests = ((float)characterUI.GetLinkedCharacter().CurrHp / (float)characterUI.GetLinkedCharacter().HPMax >= requirementToMeet.quantityRequired);
                        if (passedAllTests == true) break;
                    }
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
            }
            return false;            
        }
        return passedAllTests;
    }
}
