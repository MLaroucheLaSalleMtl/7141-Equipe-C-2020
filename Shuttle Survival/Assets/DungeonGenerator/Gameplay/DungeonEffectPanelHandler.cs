using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonEffectPanelHandler : MonoBehaviour
{
    public static DungeonEffectPanelHandler dungeonEffectPanelHandler;
    [SerializeField] TextMeshProUGUI dungeonEffectsText;
    [SerializeField] GameObject lootButton;
    [SerializeField] GameObject dungeonEffectsPanel;
    bool thereIsLoot = false;

    private void Awake()
    {
        if(dungeonEffectPanelHandler == null)
        {
            dungeonEffectPanelHandler = this;
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

    public void SetupDungeonEventEffectPanel(List<DungeonEffect> dungeonEffects)
    {
        thereIsLoot = false;

        if(dungeonEffects.Count == 0)
        {
            CloseDungeonEffectsPanel();
            return;
        }

        ResetDungeonEventEffectPanel();

        foreach (DungeonEffect dungeonEffect in dungeonEffects)
        {
            bool addNewLineStatement = true;
            switch (dungeonEffect.dungeonEffectType)
            {
                case DungeonEffectType.AffectHealthSingle:
                    dungeonEffectsText.text += DungeonEventPanelHandler.dungeonEventPanelHandler.GetChoosenCharacter().CharacterName + " receives "
                                            + dungeonEffect.dungeonEffectIntensity + " damage.";
                    break;
                case DungeonEffectType.AffectHealthParty:
                    dungeonEffectsText.text += "Everyone receives "
                                            + dungeonEffect.dungeonEffectIntensity + " damage.";
                    break;
                case DungeonEffectType.UnlockDoor:
                    dungeonEffectsText.text += "Selected Door is unlocked.";
                    break;
                case DungeonEffectType.SpecificItemsLoot:
                    addNewLineStatement = false;
                    thereIsLoot = true;
                    break;
            }
            if(addNewLineStatement)
                dungeonEffectsText.text += "\n";            
        }
        if (thereIsLoot)
        {
            dungeonEffectsText.text += "You found some loot !";
            lootButton.SetActive(true);
            SetupLootPanel();
        }        
        dungeonEffectsPanel.SetActive(true);
    }

    private void ResetDungeonEventEffectPanel()
    {
        dungeonEffectsText.text = "";
        lootButton.SetActive(false);
    }

    private void SetupLootPanel()
    {
        
    }

    public void CloseDungeonEffectsPanel()
    {
        dungeonEffectsPanel.SetActive(false);
    }
}
