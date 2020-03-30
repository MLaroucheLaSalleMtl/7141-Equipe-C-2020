using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonKeywordsTranslater : MonoBehaviour
{
    public static DungeonKeywordsTranslater dungeonKeywordsTranslater;

    private void Awake()
    {
        if(dungeonKeywordsTranslater == null)
        {
            dungeonKeywordsTranslater = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string TranslateDungeonKeywordsIntoDescription(DungeonKeywords[] dungeonKeywordsToTranslate)
    {
        string description = "";
        for (int i = 0; i < dungeonKeywordsToTranslate.Length ; i++)
        {
            switch (dungeonKeywordsToTranslate[i])
            {
                case DungeonKeywords.Small:
                    description += "<color=#c6bdae>- Small ship</color>\n";
                    break;
                case DungeonKeywords.Medium:
                    description += "<color=#ae8f73>- Medium ship</color>\n";
                    break;
                case DungeonKeywords.Large:
                    description += "<color=#ffcc00>- Large ship</color>\n";
                    break;
                case DungeonKeywords.ManyTreasures:
                    description += "<color=\"yellow\">- Many treasures</color>\n";
                    break;
                case DungeonKeywords.Dangerous:
                    description += "<color=\"red\">- Lot of hazards</color>\n";
                    break;
                case DungeonKeywords.FormsOfLife:
                    description += "<color=\"green\">- Forms of life detected</color>\n";
                    break;
                case DungeonKeywords.HeavyDamagedShip:
                    description += "<color=#FF8282>- Heavily damaged ship</color>\n";
                    break;
                case DungeonKeywords.AdvancedTechnology:
                    description += "<color=#FF00FF>- Advanced Technology on board</color>\n";
                    break;
                case DungeonKeywords.Salvageable:
                    description += "<color=#CCFF00>- Salvageable parts</color>\n";
                    break;
            }
        }
        return description;
    }

}
