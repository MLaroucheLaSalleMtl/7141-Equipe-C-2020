using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorModule : Module
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

    public override void OnCreation()
    {
        base.OnCreation();
        AsteroidsManager.asteroidsManager.DisableOtherUI();
        DialogueTriggers.dialogueTriggers.TriggerDialogue(14);
    }
}
