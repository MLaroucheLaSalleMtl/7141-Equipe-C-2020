using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LockedDoor : MonoBehaviour
{
    ShipManager ship = ShipManager.instance;
    public string doorTitle = "Broken Door";
    [TextArea(2, 4)]
    public string doorInfo;
    public int turnToUnlock = 1;
    public int turnsRemaining;
    public bool underRepair = false;
    public ResourcesCost costToRepair;
    [SerializeField] private int capaciteOxygeneSalle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        DoorManager.doorManager.hoveredDoor = this;
        GetComponent<SpriteRenderer>().color = ModuleManager.moduleManager.hoverColor;
    }
    private void OnMouseExit()
    {
        DoorManager.doorManager.hoveredDoor = null;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public string GetRepairCost()
    {
        return "Repair cost : " + turnToUnlock + " <sprite=\"Time\" index=0> + other resources. (need images)";
    }

    public void UnlockingDoor(bool resourceCheck)
    {
        ship.AddOxygenCapacite(capaciteOxygeneSalle);
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
        }
    }
}
