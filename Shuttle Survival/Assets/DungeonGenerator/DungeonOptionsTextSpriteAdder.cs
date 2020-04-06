using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DungeonOptionsTextSpriteAdder
{
    public static string AddTextAndSpritesAccordingToOptionsRequirements(List<DungeonOptionRequirement> requirements)
    {
        string stringToAppend = "";
        foreach (var requirement in requirements)
        {
            switch (requirement.statsToCheck)
            {
                case DungeonStatsToCheck.StrengthSingle:
                    stringToAppend += "[Str]";
                    break;
                case DungeonStatsToCheck.HealthAmountSingle:
                    stringToAppend += "[Hp]";
                    break;
                case DungeonStatsToCheck.HealthPercentageSingle:
                    stringToAppend += "[Hp]";
                    break;
                case DungeonStatsToCheck.Gold:
                    stringToAppend += "[Gold]";
                    break;
                case DungeonStatsToCheck.HasHammer:
                    stringToAppend += "[Hammer]";
                    break;
                case DungeonStatsToCheck.HasEngineerToolbox:
                    stringToAppend += "[Engineer's Toolbox]";
                    break;
                case DungeonStatsToCheck.HasDigitalKey:
                    stringToAppend += "[Digital Key]";
                    break;
                case DungeonStatsToCheck.StrengthParty:
                    stringToAppend += "[Str]";
                    break;
                case DungeonStatsToCheck.TinkeringSingle:
                    stringToAppend += "[Tink]";
                    break;
                case DungeonStatsToCheck.TinkeringParty:
                    stringToAppend += "[Tink]";
                    break;
                case DungeonStatsToCheck.CharismaSingle:
                    stringToAppend += "[Chr]";
                    break;
                case DungeonStatsToCheck.CharismaParty:
                    stringToAppend += "[Chr]";
                    break;
            }
        }
        return stringToAppend;
    }
}
