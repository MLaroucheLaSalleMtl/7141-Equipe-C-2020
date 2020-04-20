using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ShipEventsManager : MonoBehaviour
{
    public static ShipEventsManager shipEventsManager;
    Queue<ShipEvent> shipEventsQueue = new Queue<ShipEvent>();
    [SerializeField] GameObject shipEventPopup;
    [SerializeField] TextMeshProUGUI eventTitleText;
    [SerializeField] TextMeshProUGUI eventDescriptionText;
    [SerializeField] Button closeButton;
    [SerializeField] Inventaire mainInventory;
    [SerializeField] Button altButton;
    ShipEvent currentShipEvent;
    bool firstTimeScrapsCleanedUp = true;
    bool firstTimeRoomUnlocked = true;

    private void Awake()
    {
        if(ShipEventsManager.shipEventsManager == null)
        {
            ShipEventsManager.shipEventsManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddShipEventToQueue(ShipEvent shipEventToAdd)
    {
        shipEventsQueue.Enqueue(shipEventToAdd);       
    }

    public void FreeShipEventsQueue()
    {
        RewardsDisplayer.rewardsDisplayer.CloseRewardsUI();
        shipEventPopup.SetActive(false);
        DisableUIDuringEvents();
        if (shipEventsQueue.Count > 0)
        {
            currentShipEvent = shipEventsQueue.Dequeue();
            closeButton.onClick.RemoveAllListeners();
            switch (currentShipEvent.shipEventType)
            {
                case ShipEventType.ModuleCompletion:
                    CameraController.cameraController.GetToThisPosition(currentShipEvent.positionOfTheEvent, () =>
                    {
                        eventTitleText.text = currentShipEvent.createdModuleOrDoor.GetComponent<Module>().moduleName + " completed !";
                        eventDescriptionText.text = "Your character " + currentShipEvent.createdModuleOrDoor.GetComponent<Module>().onCreationPopupString;
                        closeButton.onClick.AddListener(FreeShipEventsQueue);
                        shipEventPopup.SetActive(true);
                    });
                    break;
                case ShipEventType.UnlockedDoor:
                    CameraController.cameraController.GetToThisPosition(currentShipEvent.positionOfTheEvent, () =>
                    {
                        currentShipEvent.onEventPosition?.Invoke();
                        eventTitleText.text = "Door repair completed !";
                        eventDescriptionText.text = "Your character " + currentShipEvent.createdModuleOrDoor.GetComponent<LockedDoor>().onDoorUnlockedPopupString;
                        closeButton.onClick.AddListener(() =>
                        {
                            NewRoomUnlockedEvent(currentShipEvent);
                        });
                        shipEventPopup.SetActive(true);
                    });
                    break;
                case ShipEventType.Asteroids:
                    CameraController.cameraController.GetToThisPosition(AsteroidsManager.asteroidsManager.asteroidsViewPosition.position, () =>
                    {
                        eventTitleText.text = "New asteroids have been found ! ";
                        eventDescriptionText.text = "It is a great opportunity to gain some <b>easy resources</b>. Let's <i>hurry</i> before we lose them";
                        closeButton.onClick.AddListener(FreeShipEventsQueue);
                        shipEventPopup.SetActive(true);
                    });
                    break;
                case ShipEventType.ScrapsCleanedUp:
                    CameraController.cameraController.GetToThisPosition(currentShipEvent.positionOfTheEvent, () =>
                    {
                        eventTitleText.text = "Scraps have been cleaned up ! ";
                        eventDescriptionText.text = "Clean up of the scraps is done. You found precious resources amongst the junks that could be useful.";
                        ResourcesPack scrapsRewards = RandomisedLootDecrypter.GetInstance().DecryptRandomisedLoot(currentShipEvent.scraps.scrapsLoot);
                        mainInventory.AddManyResources(scrapsRewards);
                        RewardsDisplayer.rewardsDisplayer.ReceiveRewardsToDisplay(scrapsRewards.resources, false);
                        Destroy(currentShipEvent.scraps.gameObject);
                        closeButton.onClick.AddListener(() => { FreeShipEventsQueue(); if(firstTimeScrapsCleanedUp)FirstTimeScrapsCleanedUpEvent(); });
                        shipEventPopup.SetActive(true);
                    });
                    break;
                case ShipEventType.CharacterAlert:
                    if (currentShipEvent.characterSystem.characterAlertTypes.Count > 0)
                    {
                        CameraController.cameraController.GetToThisPosition(currentShipEvent.positionOfTheEvent, () =>
                        {
                            CharacterSystem currentCharacterSystem = currentShipEvent.characterSystem;
                            switch (currentCharacterSystem.characterAlertTypes[0])
                            {
                                case CharacterAlertType.Sleepy:
                                    shipEventPopup.SetActive(true);
                                    eventTitleText.text = currentCharacterSystem.name+ " is sleepy";
                                    eventDescriptionText.text = "";
                                    
                                    altButton.onClick.RemoveAllListeners();                                    

                                    if (!currentCharacterSystem.Dispo)
                                    {
                                        altButton.onClick.AddListener(() => QuestionDispo(currentCharacterSystem,() => currentCharacterSystem.GoToBed(true)));
                                        
                                    }
                                    else
                                    {
                                        altButton.onClick.AddListener(()=>currentCharacterSystem.GoToBed(false));
                                    }                                                                                                          
                                    
                                    break;
                                case CharacterAlertType.Hungry:
                                    shipEventPopup.SetActive(true);
                                    eventTitleText.text = currentCharacterSystem.name + " is hungry";
                                    eventDescriptionText.text = "";
                                    //EMILE
                                    altButton.onClick.RemoveAllListeners();

                                    if (!currentCharacterSystem.Dispo)
                                    {
                                        altButton.onClick.AddListener(() => QuestionDispo(currentCharacterSystem, () => currentCharacterSystem.GoEat(true)));

                                    }
                                    else
                                    {
                                        altButton.onClick.AddListener(() => currentCharacterSystem.GoEat(false));
                                    }
                                    break;
                                case CharacterAlertType.Hurty:
                                    shipEventPopup.SetActive(true);
                                    eventTitleText.text = currentCharacterSystem.name + " is low health";
                                    eventDescriptionText.text = "";
                                    
                                    altButton.onClick.RemoveAllListeners();

                                    if (!currentCharacterSystem.Dispo)
                                    {
                                        altButton.onClick.AddListener(() => QuestionDispo(currentCharacterSystem, () => currentCharacterSystem.StartHealing(true)));

                                    }
                                    else
                                    {
                                        altButton.onClick.AddListener(() => currentCharacterSystem.StartHealing(false));
                                    }
                                    break;
                                
                            }
                        });
                    }
                    break;
            }
            AudioManager.audioManager.PlaySoundEffect(SoundEffectsType.ShipEventWindowPopup);
        }
        else
        {
            ManageEndOfEventQueue();
        }
    }

    private void ManageEndOfEventQueue()
    {
        if (currentShipEvent != null && currentShipEvent.shipEventType == ShipEventType.Asteroids)
        {
            CameraController.cameraController.GetToThisPosition(AsteroidsManager.asteroidsManager.asteroidsReturnViewPosition.position, () =>
            {
                PanelManager.panelManager.EnableInteractables();
                TimeManager.timeManager.EnableSkipTimeButton();
                EnableUIAfterEvents();
            });
            currentShipEvent = null;
        }
        else
        {
            EnableUIAfterEvents();
        }
    }

    private static void DisableUIDuringEvents()
    {
        PanelManager.panelManager.DisableInteractables();
        TimeManager.timeManager.DisableSkipTimeButton();
    }

    private static void EnableUIAfterEvents()
    {
        CameraController.cameraController.playerControlEnabled = true;
        PanelManager.panelManager.EnableInteractables();
        TimeManager.timeManager.EnableSkipTimeButton();
    }

    private void NewRoomUnlockedEvent(ShipEvent currentShipEvent)
    {
        CameraController.cameraController.GetToThisPosition(currentShipEvent.unlockedRoomPosition, () => 
        {
            eventTitleText.text = "New area has been unlocked !";
            eventDescriptionText.text = currentShipEvent.createdModuleOrDoor.GetComponent<LockedDoor>().onNewRoomAvailablePopupString;
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(() => 
            {
                FreeShipEventsQueue();
                if (firstTimeRoomUnlocked)
                {
                    firstTimeRoomUnlocked = false;
                    DialogueTriggers.dialogueTriggers.TriggerDialogue(6);
                }
            });
            shipEventPopup.SetActive(true);
        });
        shipEventPopup.SetActive(false);
    }

    private void QuestionDispo(CharacterSystem current, Action action)
    {
        eventTitleText.text = "Personnage Occupé!";
        eventDescriptionText.text = current.name + " is busy, to proceed they need to stop what they were doing";
        altButton.GetComponentInChildren<Text>().text = "Proceed";
        altButton.onClick.RemoveAllListeners();
        altButton.onClick.AddListener(()=>  action() );
        altButton.onClick.AddListener(FreeShipEventsQueue); //close panel
    }

    public void FirstTimeScrapsCleanedUpEvent()
    {
        firstTimeScrapsCleanedUp = false;
        DialogueTriggers.dialogueTriggers.TriggerDialogue(3);
    }
}
