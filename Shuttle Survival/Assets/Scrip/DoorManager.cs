using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    [SerializeField] GameObject repairCostHolder;
    [SerializeField] Transform repairCostGrid;
    [SerializeField] public Sprite openDoorSprite;
    [SerializeField] GameObject repairButton;
    [SerializeField] GameObject doorProgressBar;
    [SerializeField] Image doorProgressFill;
    [SerializeField] GameObject turnsRemainingText;
    [SerializeField] TextMeshProUGUI repairTimeTextField;
    private int unlockedDoorCount = 0;
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
        if (hoveredDoor != null && PanelManager.panelManager.IsInteractablesEnabled() && Input.GetMouseButtonDown(0))
        {
            AudioManager.audioManager.PlaySoundEffect(SoundEffectsType.ButtonClick);
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
            repairCostHolder.gameObject.SetActive(true);
            RewardsDisplayer.rewardsDisplayer.DisplayResourcesCost(currentDoor.costToRepair.resources, repairCostGrid);
            repairTimeTextField.text = "Repair time : " + currentDoor.turnToRepair + " <sprite=0>";
            repairButton.GetComponentInChildren<TextMeshProUGUI>().text = "Repair";
            repairButton.GetComponent<Button>().onClick.RemoveAllListeners();
            repairButton.GetComponent<Button>().onClick.AddListener(() => UnlockDoor());
            repairButton.GetComponent<TooltipHandler>().simpleTextString = "Repair door to gain \n access to other rooms";
        }
        else
        {
            repairCostHolder.gameObject.SetActive(false);
            turnsRemainingText.SetActive(true);
            repairButton.GetComponentInChildren<TextMeshProUGUI>().text = "Cancel";
            repairButton.GetComponent<Button>().onClick.RemoveAllListeners();
            repairButton.GetComponent<Button>().onClick.AddListener(() => CancelDoorRepair());
            doorProgressBar.SetActive(true);
            doorProgressFill.fillAmount = Mathf.Abs(1 - ((float)currentDoor.turnsRemaining) / currentDoor.turnToRepair);
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
                currentDoor.bob = GameManager.selection.GetComponent<CharacterSystem>();
                currentDoor.bob.Unavailable();
            }
            else
            {
                MessagePopup.MessagePopupManager.SetStringAndShowPopup("Select someone to go upgrade");
                inventaire.AddManyResources(currentDoor.costToRepair);
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

    public void IncrementUnlockedDoorCount()
    {
        unlockedDoorCount++;
        if (unlockedDoorCount == 3)
        {
            DialogueTriggers.dialogueTriggers.TriggerDialogue(8);
        }
        else if (unlockedDoorCount == 4)
        {
            DialogueTriggers.dialogueTriggers.TriggerDialogue(12);
        }
        else if (unlockedDoorCount == 5)
        {
            DialogueTriggers.dialogueTriggers.TriggerDialogue(13);
        }
    }
}
