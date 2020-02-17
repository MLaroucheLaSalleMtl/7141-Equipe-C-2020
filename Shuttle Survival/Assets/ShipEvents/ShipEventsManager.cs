using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ShipEventsManager : MonoBehaviour
{
    public static ShipEventsManager shipEventsManager;
    Queue<ShipEvent> shipEventsQueue = new Queue<ShipEvent>();
    [SerializeField] GameObject shipEventPopup;
    [SerializeField] TextMeshProUGUI eventTitleText;
    [SerializeField] TextMeshProUGUI eventDescriptionText;
    [SerializeField] Button closeButton;

    ShipEvent currentShipEvent;
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
        shipEventPopup.SetActive(false);
        DisableUIDuringEvents();
        //print(shipEventsQueue.Count);
        if (shipEventsQueue.Count > 0)
        {
            currentShipEvent = shipEventsQueue.Dequeue();
            closeButton.onClick.RemoveAllListeners();
            switch (currentShipEvent.shipEventType)
            {
                case ShipEventType.ModuleCompletion:
                    CameraController.cameraController.GetToThisPosition(currentShipEvent.createdModuleOrUnlockedDoorPosition, () =>
                    {
                        eventTitleText.text = currentShipEvent.createdModuleOrDoor.GetComponent<Module>().moduleName + " completed !";
                        eventDescriptionText.text = "CharacterName " + currentShipEvent.createdModuleOrDoor.GetComponent<Module>().onCreationPopupString;
                        closeButton.onClick.AddListener(FreeShipEventsQueue);
                        shipEventPopup.SetActive(true);
                    });
                    break;
                case ShipEventType.UnlockedDoor:
                    CameraController.cameraController.GetToThisPosition(currentShipEvent.createdModuleOrUnlockedDoorPosition, () =>
                    {
                        currentShipEvent.onEventPosition?.Invoke();
                        eventTitleText.text = "Door repair completed !";
                        eventDescriptionText.text = "CharacterName " + currentShipEvent.createdModuleOrDoor.GetComponent<LockedDoor>().onDoorUnlockedPopupString;
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
            }
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
            closeButton.onClick.AddListener(FreeShipEventsQueue);
            shipEventPopup.SetActive(true);
        });
        shipEventPopup.SetActive(false);
    }
}
