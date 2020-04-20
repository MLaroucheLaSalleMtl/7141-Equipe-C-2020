using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DungeonDoor : MonoBehaviour
{
    [SerializeField] DungeonEvent dungeonDoorEvent;
    public int numberOfTurnsToUnlock = 1;
    GameObject linkedRoom;
    DungeonDoor linkedDoor;
    bool hovered;
    bool unlocked;
    DoorOpening doorOpening;
    [SerializeField] Sprite unlockedSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!DungeonEventPanelHandler.dungeonEventPanelHandler.IsBusyWithDungeonEvent() && hovered && Input.GetMouseButtonDown(0))
        {
            if (unlocked)
            {
                EnterDoor();
            }
            else
            {
                DungeonDoorUnlocker.SetDungeonDoor(this);
                DungeonEventPanelHandler.dungeonEventPanelHandler.SetupDungeonEventPanel(dungeonDoorEvent);
            }
        }
    }

    public void SetLinkedRoomPositionAndOpening(GameObject linkedRoom, DoorOpening doorOpening)
    {
        this.doorOpening = doorOpening;
        this.linkedRoom = linkedRoom;
    }

    private void OnMouseEnter()
    {
        
        hovered = true;
        GetComponentInChildren<SpriteRenderer>().color = ModuleManager.moduleManager.hoverColor;
        MouseCursorManager.mouseCursorManager.SetCursor(MouseCursor.hoverCursor);
    }

    private void OnMouseExit()
    {
        
        hovered = false;
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
        MouseCursorManager.mouseCursorManager.SetCursor(MouseCursor.defaultCursor);
    }

    public void Unlock()
    {
        //change sprite
        unlocked = true;
        GetComponentInChildren<SpriteRenderer>().sprite = unlockedSprite;
        switch (doorOpening)
        {
            case DoorOpening.Top:
                linkedRoom.GetComponent<AddRoom>().bottomDoor.LinkedUnlock();
                break;
            case DoorOpening.Left:
                linkedRoom.GetComponent<AddRoom>().rightDoor.LinkedUnlock();
                break;
            case DoorOpening.Bottom:
                linkedRoom.GetComponent<AddRoom>().topDoor.LinkedUnlock();
                break;
            case DoorOpening.Right:
                linkedRoom.GetComponent<AddRoom>().leftDoor.LinkedUnlock();
                break;
        }
    }

    public void LinkedUnlock()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = unlockedSprite;
        unlocked = true;
    }

    public void EnterDoor()
    {
        DungeonCamera.dungeonCameraHolder.GetToThisPosition(linkedRoom.transform.position);
    }

    public bool IsUnlocked()
    {
        return unlocked;
    }


}
