using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Module
{
    ShipManager shipM;
    shipNPCmanager path;
    int cooldown = 0;
    int useTime = 2;
    int useCD = 0;
    bool inUse = false;
    int delay = 4;
    int yield = 1;
    [SerializeField] private int cost = 5;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
        shipM = ShipManager.shipM;
        path = shipNPCmanager.NPCmanagInstance;
    }

    private void OnTimeChanged(object sender, EventArgs e)
    {
        shipM.AddAir(1);
        Debug.Log(shipM.O2Qte);
        if (inUse)
        {
            if (useTime%useCD==0)
            {
                Debug.Log("fini");
                
                useCD = 0;
                inUse = false;
                shipM.ShipInv().AddItem(shipM.ShipInv().IdentifyStackItem(1, yield));
            }
            useCD++;
        }
        
        if (cooldown%delay== 0)
        {
           cooldown = 0;
            shipM.ShipInv().AddItem(shipM.ShipInv().IdentifyStackItem(1,yield));            
        }
        cooldown++;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void UseModule()
    {
        if (GameManager.selection && GameManager.selection.Dispo) 
        {
            if (shipM.ShipInv().PayFromID(12,cost)) 
            { 
                base.UseModule();
                path.NeedAHandOverHere(this.transform);
                inUse = true;
                Debug.Log("Farm in use");
            }
        }
    }

    public override void OnCreation()
    {
        base.OnCreation();
        
    }
}
