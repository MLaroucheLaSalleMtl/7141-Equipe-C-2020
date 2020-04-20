using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DungeonEndingManager
{
    public static bool FirstTimeExitingDungeon = true;
    public static void EndDungeon()
    {
        PushLootTheMainInventory();
        DungeonTimeCounter.dungeonTimeCounter.SendElapsedTurnsToTimeManager();
        ScreenTransitionManager.screenTransitionManager.ScreenTransition(() => ShipScanManager.shipScanManager.ReenableShipSceneHolderAndReloadEmptyDungeonScene(),
            () =>
            {
                TimeManager.timeManager.AddTimeAfterDungeon();
                CheckIfFirstTimeExit();
            }
        );       
    }

    private static void PushLootTheMainInventory()
    {
        List<ItemStack> loots = DungeonLootPanelManager.dungeonLootPanelManager.GetLootsFromAllCharacters();
        foreach (ItemStack item in loots)
        {
            Inventaire.inventaire.AddItem(item);
        }
    }

    private static void CheckIfFirstTimeExit()
    {
        if (FirstTimeExitingDungeon)
        {
            DialogueTriggers.dialogueTriggers.TriggerDialogue(11);
            FirstTimeExitingDungeon = false;
        }
    }
}