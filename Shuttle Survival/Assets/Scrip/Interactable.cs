using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject panel;


    void Start()
    {
        //panel.SetActive(false);
       // PanelManager.panelManager.OnPanelOpened += ModuleManager.moduleManager.OnPanelOpened;
    }

    private void OnMouseDown()
    {
        Debug.Log("Fonctionne");
        //panel.SetActive(true);
    }

   /* public void OnPanelOpened(object source, System.EventArgs args)
    {
        
    }*/
}
