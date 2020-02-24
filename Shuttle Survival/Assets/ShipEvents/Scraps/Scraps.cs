using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scraps : MonoBehaviour
{
    public RandomisedLoot scrapsLoot;
    bool hovered = false;
    public bool beingCleaned = false;

    shipNPCmanager NPC;

    // Start is called before the first frame update
    void Start()
    {
        NPC = shipNPCmanager.NPCmanagInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if(hovered && Input.GetMouseButtonDown(0))
        {
            if (NPC.IsNPCavailable() == true)
            {
                Debug.Log("Sending bob");

                ScrapsManager.scrapsManager.OpenScrapsPanel(this);

                //trouver le bon endroit pour envoyer bob      
                NPC.NeedAHandOverHere(gameObject.transform);
            }
            else
            {
                MessagePopup.MessagePopupManager.SetStringAndShowPopup("Select someone to go upgrade");
                //plus tard on pourrait peut-être mettre ici le drop down list avec toutes les persos
            }
        }
    }

    private void OnMouseEnter()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetComponent<SpriteRenderer>().color = ModuleManager.moduleManager.hoverColor;
            hovered = true;
            MouseCursorManager.mouseCursorManager.SetCursor(MouseCursor.hoverCursor);
        }

    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        hovered = false;
        MouseCursorManager.mouseCursorManager.SetCursor(MouseCursor.defaultCursor);
    }

    public void BeginCleanUp()
    {
        beingCleaned = true;
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
    }

    public void CancelCleanUp()
    {
        beingCleaned = false;
        TimeManager.timeManager.OnTimeChanged -= OnTimeChanged;
    }
    public void OnTimeChanged(object source, EventArgs e)
    {
        TimeManager.timeManager.OnTimeChanged -= OnTimeChanged;
        ShipEventsManager.shipEventsManager.AddShipEventToQueue(ShipEvent.CleanedUpScrapsEvent(this, transform.position));

    }
}
