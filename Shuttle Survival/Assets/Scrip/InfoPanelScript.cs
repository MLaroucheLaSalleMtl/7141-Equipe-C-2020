using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelScript : MonoBehaviour
{
    public static InfoPanelScript infoPanelScript;
    [SerializeField] private Text powerTot;
    [SerializeField] private Text powerTurn;
    [SerializeField] private Text oxyPct;
    [SerializeField] private Text oxyTurn;
    [SerializeField] private Text foodQte;
    [SerializeField] private Text foodTurn;
    [SerializeField] GameObject panelComp;
    bool firstTimeClosingPanel = true;
    bool isPanelOn = false;
    private string preText = "Time left: ";

    ShipManager ship;
    GameManager game;

    private void Awake()
    {
        if (infoPanelScript == null)
        {
            infoPanelScript = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        panelComp.SetActive(false);
        ship = ShipManager.shipM;
        game = GameManager.GM;
        
    }

    private void Update()
    {
        if (!firstTimeClosingPanel && Input.GetKeyDown(KeyCode.O))
        {
            PanelUpdate();
        }
    }
    public void ClosePanel()
    {
        isPanelOn = false;
        panelComp.SetActive(false);
        if (firstTimeClosingPanel)
        {
            firstTimeClosingPanel = false;
            DialogueTriggers.dialogueTriggers.TriggerDialogue(5);
        }

    }

    public void PanelUpdate()
    {
        isPanelOn = !isPanelOn;
        if (!isPanelOn)
        {
            panelComp.SetActive(false);
            return;
        }
        panelComp.SetActive(true);
        //Power
        powerTot.text = ship.PowerQte.ToString();
        powerTurn.text = ship.PowerQte.ToString();//temporaire, faudrait une fonction quelque part

        //Oxyg
        float oxyP = (ship.O2Qte*100 / ship.O2Capacity);
        oxyPct.text = oxyP.ToString() + "%";
        //oxyP = (ship.O2Qte / game.Personnages.Count)*30;
        float a = ship.O2Qte, b = game.Personnages.Count;
        oxyP = a / b;
        //oxyTurn.text = preText + Mathf.FloorToInt(oxyP).ToString("0:00");
        oxyTurn.text = preText + Mathf.FloorToInt(oxyP).ToString();

        //Food
            
        int foodNb = ship.ShipInv().GetAmount(1);
        foodQte.text = foodNb.ToString();

        foodNb = foodNb*CharacterSystem.hunger/game.Personnages.Count;
        foodTurn.text =preText + foodNb.ToString();
    }

}
