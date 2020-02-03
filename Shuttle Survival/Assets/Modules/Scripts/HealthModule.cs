using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModule : Module
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void UseModule()
    {
        base.UseModule();
        print("Healing");
    }

    public override void OnCreation()
    {
        base.OnCreation();
        print("Healing module has been created !");
    }
}
