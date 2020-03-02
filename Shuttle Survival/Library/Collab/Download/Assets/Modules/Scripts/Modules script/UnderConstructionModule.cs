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


    // Start is called before the first frame update
    protected override void Start()
    {
        Debug.Log("construction initiated");
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
    }

    public void OnTimeChanged(object sender, EventArgs e)
    {
        turnsRemainingToBuild--;
        if(turnsRemainingToBuild <= 0)
        {
            Instantiate(moduleToBuild, transform.position, Quaternion.identity);
            TimeManager.timeManager.OnTimeChanged -= OnTimeChanged;
            if(!(moduleToBuild.GetComponent<Module>() is CoreModule))
            {
                ShipEventsManager.shipEventsManager.AddShipEventToQueue(ShipEvent.ModuleCreationEvent(moduleToBuild, transform.position));
            }
            Debug.Log("End of construction");
            GameManager.selection.Dispo = true;
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
        TimeManager.timeManager.OnTimeChanged -= OnTimeChanged;
    }
}
