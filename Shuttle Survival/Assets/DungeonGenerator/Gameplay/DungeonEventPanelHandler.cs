using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonEventPanelHandler : MonoBehaviour
{
    public static DungeonEventPanelHandler dungeonEventPanelHandler;
    [SerializeField] GameObject dungeonEventPanel;
    [SerializeField] TextMeshProUGUI dungeonEventText;
    [SerializeField] DungeonEvent currentDungeonEvent;
    CharacterSystem choosenCharacterForOption;
    bool busyWithDungeonEvent = false;
    private void Awake()
    {
        if(dungeonEventPanelHandler == null)
        {
            dungeonEventPanelHandler = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void FORTESTING_MANUALLY_LOAD_DUNGEON_EVENT()
    {
        SetupDungeonEventPanel(currentDungeonEvent);
    }

    public void SetupDungeonEventPanel(DungeonEvent dungeonEventToSetup)
    {        
        busyWithDungeonEvent = true;
        currentDungeonEvent = dungeonEventToSetup;
        dungeonEventText.text = currentDungeonEvent.eventMessage;
        
        DungeonEffectsHandler.dungeonEffectsHandler.HandleDungeonEffects(currentDungeonEvent.eventEffects);
        DungeonCharacterManager.dungeonCharacterManager.RefreshEveryCharacterUI();
        //IF NOT GAME OVER, on spawn les options, sinon on spawneras les options de give up etc.
        //Le handle options va check si on respect requirements, fak si un perso meurt et quon a besoin de lui pour une option, a saffichera pas       
        DungeonOptionsHandler.dungeonOptionsHandler.SetupDungeonOptions(currentDungeonEvent.dungeonOptions);

        dungeonEventPanel.SetActive(true);
    }

    public bool IsBusyWithDungeonEvent()
    {
        return busyWithDungeonEvent;
    }

    public void EndDungeonEvent()
    {
        busyWithDungeonEvent = false;
        dungeonEventPanel.SetActive(false);
    }

    public void SetCharacterForOption(CharacterSystem choosenCharacter)
    {
        choosenCharacterForOption = choosenCharacter;
    }

    public CharacterSystem GetChoosenCharacter()
    {
        return choosenCharacterForOption;
    }
}
