﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] private GameObject fog;
    [SerializeField] private GameObject hiddenModule;
    ShipManager ship;
    public string doorTitle = "Broken Door";
    [TextArea(2, 4)]
    public string doorInfo;
    public int turnToUnlock = 1;
    public int turnsRemaining;
    public bool underRepair = false;
    public ResourcesCost costToRepair;
    [TextArea(2, 4)]
    public string onDoorUnlockedPopupString;
    [TextArea(2, 4)]
    public string onNewRoomAvailablePopupString;
    [SerializeField] private int capaciteOxygeneSalle;
    [SerializeField] Vector3 unlockedRoomPosition;
    // Start is called before the first frame update
    void Start()
    {
        ship = ShipManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        DoorManager.doorManager.hoveredDoor = this;
        GetComponent<SpriteRenderer>().color = ModuleManager.moduleManager.hoverColor;
        MouseCursorManager.mouseCursorManager.SetCursor(MouseCursor.hoverCursor);

    }
    private void OnMouseExit()
    {
        DoorManager.doorManager.hoveredDoor = null;
        GetComponent<SpriteRenderer>().color = Color.white;
        MouseCursorManager.mouseCursorManager.SetCursor(MouseCursor.defaultCursor);

    }

    public string GetRepairCost()
    {
        return "Repair cost : " + turnToUnlock + " <sprite=\"Time\" index=0> + other resources. (need images)";
    }



    internal void BeginRepair()
    {
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
        turnsRemaining = turnToUnlock;
        underRepair = true;
    }

    public void OnTimeChanged(object sender, EventArgs e)
    {
        turnsRemaining--;
        if(turnsRemaining <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = DoorManager.doorManager.openDoorSprite;
            GetComponent<BoxCollider2D>().enabled = false;
            TimeManager.timeManager.OnTimeChanged -= OnTimeChanged;
            ShipEventsManager.shipEventsManager.AddShipEventToQueue(ShipEvent.DoorEvent(gameObject, transform.position, unlockedRoomPosition,
                                                                    () => 
                                                                    {
                                                                        fog.GetComponent<DissolveEffect>().StartDissolve(() =>
                                                                        {
                                                                            fog.gameObject.SetActive(false);
                                                                            hiddenModule.SetActive(true);
                                                                        });
                                                                        foreach (Transform transform in fog.transform)
                                                                        {
                                                                            transform.GetComponent<DissolveEffect>().StartDissolve(() => transform.gameObject.SetActive(false));
                                                                        }
                                                                        
                                                                    }));
            //ship.AddOxygenCapacite(capaciteOxygeneSalle);
        }
    }

    public void CancelRepair()
    {
        TimeManager.timeManager.OnTimeChanged -= OnTimeChanged;
        underRepair = false;
    }
}
