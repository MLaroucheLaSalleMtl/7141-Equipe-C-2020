using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler inputHandler;
    public float h;
    public float v;
    private void Awake()
    {
        if (inputHandler == null)
        {
            inputHandler = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnHorizontal(InputAction.CallbackContext context)
    {
        h = context.ReadValue<float>();
        
    }
    
    public void OnVertical(InputAction.CallbackContext context)
    {
        v = context.ReadValue<float>();
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        bool pressed = context.phase == InputActionPhase.Started;
        if (pressed)
        {
            InventoryUI.inventoryUi.ToggleMainInventoryUIPanel();
        }
    }
    
    public void OnOpenShipStatus(InputAction.CallbackContext context)
    {
        bool pressed = context.phase == InputActionPhase.Started;
        if (pressed)
        {
            InfoPanelScript.infoPanelScript.PanelUpdate();
        }
    }
}
