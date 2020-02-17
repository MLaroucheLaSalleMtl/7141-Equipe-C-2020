using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmModule : Module
{
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
        AsteroidsManager.asteroidsManager.GoInAsteroidsViewForArm();
        PanelManager.panelManager.CloseAllPanels();
    }

    public override void OnCreation()
    {
        base.OnCreation();       
    }
}
