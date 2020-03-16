using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScanner : Module
{
    [SerializeField] int turnsBeforeScanningForNewShips = 3;

    int turnCounter = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void UseModule()
    {
        base.UseModule();
        ShipScanManager.shipScanManager.SetUpShipScannerDungeonPanel();
    }

    

    public override void OnCreation()
    {
        base.OnCreation();
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
    }

    public void OnTimeChanged(object source, EventArgs e)
    {
        turnCounter++;
        if(turnCounter % 3 == 0)
        {
            ScanForNewShips();
        }
    }

    private void ScanForNewShips()
    {
        //roll new ships
    }
}
