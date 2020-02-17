using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipEvent
{
    public ShipEventType shipEventType;
    public GameObject createdModuleOrDoor;
    public Vector3 createdModuleOrUnlockedDoorPosition;
    public Vector3 unlockedRoomPosition;
    public Action onEventPosition;
    /// <summary>
    /// Constructor used for module completion event
    /// </summary>
    /// <param name="createdModule">The module that has been created</param>
    /// <param name="createdModulePosition">The position of the created module</param>
    private ShipEvent(GameObject createdModule, Vector3 createdModulePosition)
    {
        this.createdModuleOrDoor = createdModule;
        this.createdModuleOrUnlockedDoorPosition = createdModulePosition;
        shipEventType = ShipEventType.ModuleCompletion;
    }

    /// <summary>
    /// Constructor used for door unlock event
    /// </summary>
    /// <param name="unlockedDoor">GameObject of the unlocked door</param>
    /// <param name="doorPosition">The position of the unlocked door</param>
    /// <param name="unlockedRoomPosition">The position of the unlocked room</param>
    private ShipEvent(GameObject unlockedDoor, Vector3 doorPosition, Vector3 unlockedRoomPosition, Action onEventPosition)
    {
        this.createdModuleOrDoor = unlockedDoor;
        this.createdModuleOrUnlockedDoorPosition = doorPosition;
        this.unlockedRoomPosition = unlockedRoomPosition;
        this.onEventPosition = onEventPosition;
        shipEventType = ShipEventType.UnlockedDoor;
    }

    /// <summary>
    /// Constructor used for asteroids events
    /// </summary>
    private ShipEvent(ShipEventType shipEventType)
    {
        this.shipEventType = shipEventType;
    }

    public static ShipEvent DoorEvent(GameObject unlockedDoor, Vector3 doorPosition, Vector3 unlockedRoomPosition, Action onEventPosition)
    {
        return new ShipEvent(unlockedDoor, doorPosition, unlockedRoomPosition, onEventPosition);
    }

    public static ShipEvent ModuleCreationEvent(GameObject createdModule, Vector3 createdModulePosition)
    {
        return new ShipEvent(createdModule, createdModulePosition);
    }

    public static ShipEvent AsteroidEvent()
    {
        return new ShipEvent(ShipEventType.Asteroids);
    }
}
