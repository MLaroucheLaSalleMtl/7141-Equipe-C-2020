using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PourTousLesSelectable))]
public class PointOfInterestObject : MonoBehaviour
{
    [SerializeField] bool clickable = true;
    [SerializeField] DungeonEvent onClickDungeonEvent;
    [SerializeField] bool doneAfterClick = true;
    bool doneWithPOI = false;
    bool hovered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!doneWithPOI && hovered && !DungeonEventPanelHandler.dungeonEventPanelHandler.IsBusyWithDungeonEvent() && Input.GetMouseButtonDown(0))
        {
            if (doneAfterClick) doneWithPOI = true;            
            DungeonEventPanelHandler.dungeonEventPanelHandler.SetupDungeonEventPanel(onClickDungeonEvent);
        }
    }

    private void OnMouseEnter()
    {
        
        hovered = true;
    }

    private void OnMouseExit()
    {
        
        hovered = false;
    }
}
