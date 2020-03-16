﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonEventPanelHandler : MonoBehaviour
{
    public static DungeonEventPanelHandler dungeonEventPanelHandler;
    [SerializeField] GameObject dungeonEventPanel;
    [SerializeField] TextMeshProUGUI dungeonEventText;
    [SerializeField] DungeonEvent currentDungeonEvent;
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
        currentDungeonEvent = dungeonEventToSetup;
        dungeonEventText.text = currentDungeonEvent.eventMessage;

        DungeonEffectsHandler.dungeonEffectsHandler.HandleDungeonEffects(currentDungeonEvent.eventEffects);
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
        dungeonEventPanel.SetActive(false);
    }
}
