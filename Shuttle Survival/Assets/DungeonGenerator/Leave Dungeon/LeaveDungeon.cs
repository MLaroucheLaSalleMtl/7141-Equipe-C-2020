using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveDungeon : MonoBehaviour
{
    [SerializeField] Button openLeaveDungeonPanelButton;
    [SerializeField] GameObject leaveDungeonPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        openLeaveDungeonPanelButton.interactable = !DungeonEventPanelHandler.dungeonEventPanelHandler.IsBusyWithDungeonEvent();
    }

    public void OpenLeaveDungeonPanel()
    {
        leaveDungeonPanel.SetActive(true);
    }

    public void CloseLeaveDungeonPanel()
    {
        leaveDungeonPanel.SetActive(false);
    }

    public void LeaveTheDungeon()
    {
        DungeonEndingManager.EndDungeon();
    } 
}
