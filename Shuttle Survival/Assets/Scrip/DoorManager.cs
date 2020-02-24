using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DoorManager : MonoBehaviour
{
    [SerializeField] Inventaire inventaire;
    public static DoorManager doorManager;
    [SerializeField] GameObject doorPanel;
    [SerializeField] TextMeshProUGUI doorTitle;
    [SerializeField] TextMeshProUGUI doorInfoText;
    [SerializeField] TextMeshProUGUI repairCostText;
    [SerializeField] public Sprite openDoorSprite;
    [SerializeField] GameObject repairButton;
    [SerializeField] GameObject doorProgressBar;
    [SerializeField] Image doorProgressFill;
    [SerializeField] GameObject turnsRemainingText;
    [HideInInspector]
    public LockedDoor hoveredDoor;
    LockedDoor currentDoor;
    shipNPCmanager NPC;
    private void Awake()
    {
        if (DoorManager.doorManager == null)
        {
            DoorManager.doorManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void OnPanelOpened(object source, EventArgs args)
    {
        doorPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        PanelManager.panelManager.OnPanelOpened += OnPanelOpened;
        NPC = shipNPCmanager.NPCmanagInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if (hoveredDoor != null && PanelManager.panelManager.IsInteractablesEnabled() &&Input.GetMouseButtonDown(0))
        {
            GenerateDoorPanel(hoveredDoor);
        }
    }

    private void GenerateDoorPanel(LockedDoor hoveredDoor)
    {
        currentDoor = hoveredDoor;
        PanelManager.panelManager.OnPanelOpened_Caller();
        doorTitle.text = currentDoor.doorTitle;
        doorInfoText.text = currentDoor.doorInfo;
        if (!currentDoor.underRepair)
        {
            doorProgressBar.SetActive(false);
            turnsRemainingText.SetActive(false);
            repairCostText.gameObject.SetActive(true);
            repairCostText.text = currentDoor.GetRepairCost();
            repairButton.GetComponentInChildren<TextMeshProUGUI>().text = "Repair";
            repairButton.GetComponent<Button>().onClick.RemoveAllListeners();
            repairButton.GetComponent<Button>().onClick.AddListener(() => UnlockDoor());
            repairButton.GetComponent<TooltipHandler>().simpleTextString = "Repair door to gain \n access to other rooms";
        }
        else
        {
            repairCostText.gameObject.SetActive(false);
            turnsRemainingText.SetActive(true);
            repairButton.GetComponentInChildren<TextMeshProUGUI>().text = "Cancel";
            repairButton.GetComponent<Button>().onClick.RemoveAllListeners();
            repairButton.GetComponent<Button>().onClick.AddListener(() => CancelDoorRepair());
            doorProgressBar.SetActive(true);
            doorProgressFill.fillAmount = Mathf.Abs(1 - ((float)currentDoor.turnsRemaining) / currentDoor.turnToUnlock);
            turnsRemainingText.GetComponent<TextMeshProUGUI>().text = "<b>" + currentDoor.turnsRemaining + " <sprite=0>remaining";
            repairButton.GetComponent<TooltipHandler>().simpleTextString = "Cancel construction and\n get resources back.";

        }
        doorPanel.SetActive(true);
    }

    public void UnlockDoor()
    {
        if (inventaire.PayRessource(currentDoor.costToRepair))
        {
            if (NPC.IsNPCavailable() == true)
            {
                Debug.Log("Sending bob");

                repairButton.GetComponent<TooltipHandler>().OnPointerExit(null);
                currentDoor.BeginRepair();
                doorPanel.SetActive(false);

                //trouver le bon endroit pour envoyer bob      
                NPC.NeedAHandOverHere(currentDoor.transform);
            }
            else
            {
                MessagePopup.MessagePopupManager.SetStringAndShowPopup("Select someone to go upgrade");
                //plus tard on pourrait peut-être mettre ici le drop down list avec toutes les persos
            }
            
        }
        else
        {
            MessagePopup.MessagePopupManager.SetStringAndShowPopup("Not enough ressources");
        }
    }

    public void CancelDoorRepair()
    {
        inventaire.AddManyResources(currentDoor.costToRepair);
        currentDoor.CancelRepair();
        doorPanel.SetActive(false);
        repairButton.GetComponent<TooltipHandler>().OnPointerExit(null);
    }
}
