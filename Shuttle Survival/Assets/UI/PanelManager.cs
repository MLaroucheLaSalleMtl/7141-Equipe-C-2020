using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager panelManager;
    public bool interactablesEnabled;

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
        interactablesEnabled = true;
    }
   
    public EventHandler OnPanelOpened;

    public virtual void OnPanelOpened_Caller()
    {
        if(OnPanelOpened != null)
        {
            OnPanelOpened(this, EventArgs.Empty);
        }
    }



    public void CloseAllPanels()
    {
        OnPanelOpened_Caller();
    }

    public void DisableInteractables()
    {
        interactablesEnabled = false;
    }

    public void EnableInteractables()
    {
        interactablesEnabled = true;
    }

    public bool IsInteractablesEnabled()
    {
        return interactablesEnabled;
    }
}
