using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scraps : MonoBehaviour, ISelectable
{
    public RandomisedLoot scrapsLoot;
    bool hovered = false;
    public bool beingCleaned = false;
    SpriteRenderer spriteRenderer;
    shipNPCmanager NPC;
    public CharacterSystem bob ;

    // Start is called before the first frame update
    void Start()
    {
        NPC = shipNPCmanager.NPCmanagInstance;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hovered && Input.GetMouseButtonDown(0))
        {         
            ScrapsManager.scrapsManager.OpenScrapsPanel(this);
        }
    }

    private void OnMouseEnter()
    {
        
        CharacterSystem.surPerso = true;
        Debug.Log(CharacterSystem.surPerso);
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            spriteRenderer.color = ModuleManager.moduleManager.hoverColor;
            hovered = true;
            MouseCursorManager.mouseCursorManager.SetCursor(MouseCursor.hoverCursor);     
        }

    }

    private void OnMouseExit()
    {
        CharacterSystem.surPerso = false;
        spriteRenderer.color = Color.white;

        hovered = false;

        MouseCursorManager.mouseCursorManager.SetCursor(MouseCursor.defaultCursor);
    }

    private void OnMouseDown()
    {
        SelectionManager.selectionManager.ChangeObjectSelection(this);
    }

    public void BeginCleanUp()
    {
        if(!ScrapsManager.scrapsManager.firstScrapsCleanedUp)
        {
            ScrapsManager.scrapsManager.firstScrapsCleanedUp = true;
            //DialogueTriggers.dialogueTriggers.TriggerDialogue(2);
        }
        beingCleaned = true;
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
    }

    public void CancelCleanUp()
    {
        //ramène bob dispo
        bob.CancelNowDispo();
        
        beingCleaned = false;
        TimeManager.timeManager.OnTimeChanged -= OnTimeChanged;
    }

    public void OnTimeChanged(object source, EventArgs e)
    {
        TimeManager.timeManager.OnTimeChanged -= OnTimeChanged;
        ShipEventsManager.shipEventsManager.AddShipEventToQueue(ShipEvent.CleanedUpScrapsEvent(this, transform.position));

        Debug.Log("End of cleanup");
        bob.CancelNowDispo();
    }

    public void OnSelection()
    {
        spriteRenderer.material.SetFloat("_OutlineThickness", 3f);
    }

    public void OnDeselection()
    {
        spriteRenderer.material.SetFloat("_OutlineThickness", 0f);
    }

}
