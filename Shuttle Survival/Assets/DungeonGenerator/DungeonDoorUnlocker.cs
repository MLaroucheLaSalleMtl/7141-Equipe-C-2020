using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DungeonDoorUnlocker
{    
    [SerializeField] static DungeonDoor currentUnlockableDungeonDoor;

    public static void UnlockCurrentDungeonDoor()
    {
        if(currentUnlockableDungeonDoor != null)
            currentUnlockableDungeonDoor.Unlock();
    }

    public static void SetDungeonDoor(DungeonDoor dungeonDoor)
    {
        currentUnlockableDungeonDoor = dungeonDoor;
    }
}

