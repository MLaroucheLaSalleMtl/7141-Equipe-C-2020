using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderConstructionModule : Module
{
    public int totalTurnsToBuild;
    public int turnsRemainingToBuild;
    public GameObject moduleToBuild;
    //price paid

    public CharacterSystem bob;

    // Start is called before the first frame update
    protected override void Start()
    {
        Debug.Log("construction initiated");
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
        bob = GameManager.selection.GetComponent<CharacterSystem>();
        bob.Unavailable();
    }

    public void OnTimeChanged(object sender, EventArgs e)
    {
        turnsRemainingToBuild--;
        if(turnsRemainingToBuild <= 0)
        {
            GameObject module = Instantiate(moduleToBuild, transform.position, Quaternion.identity);
            module.transform.SetParent(ModuleManager.moduleManager.sceneHolder.transform);
            TimeManager.timeManager.OnTimeChanged -= OnTimeChanged;
            if(!(moduleToBuild.GetComponent<Module>() is CoreModule))
            {
                ShipEventsManager.shipEventsManager.AddShipEventToQueue(ShipEvent.ModuleCreationEvent(moduleToBuild, transform.position));
            }
            Debug.Log("End of construction");
            bob.CancelNowDispo();
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    internal void SetTurnsToBuild(int turns)
    {
        totalTurnsToBuild = turns;
        turnsRemainingToBuild = turns;
    }

    public void CancelCreation()
    {
        bob.CancelNowDispo();
        TimeManager.timeManager.OnTimeChanged -= OnTimeChanged;    
    }
}

