using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager panelManager;

    private void Awake()
    {
        if (PanelManager.panelManager == null)
        {
            PanelManager.panelManager = this;
        }
        else
        {
            Destroy(this);
        }
    }
   
    public EventHandler OnPanelOpened;

    public virtual void OnPanelOpened_Caller()
    {
        if(OnPanelOpened != null)
        {
            OnPanelOpened(this, EventArgs.Empty);
        }
    }

    private void Start()
    {
        OnPanelOpened += ModuleManager.moduleManager.OnPanelOpened;
        OnPanelOpened += DoorManager.doorManager.OnPanelOpened;
    }
}
