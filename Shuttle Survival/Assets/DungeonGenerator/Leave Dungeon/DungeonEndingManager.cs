using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DungeonEndingManager
{
    public static void EndDungeon()
    {
        PushLootTheMainInventory();
        DungeonTimeCounter.dungeonTimeCounter.SendElapsedTurnsToTimeManager();
        ScreenTransitionManager.screenTransitionManager.ScreenTransition(() => ShipScanManager.shipScanManager.ReenableShipSceneHolderAndReloadEmptyDungeonScene(), () => TimeManager.timeManager.AddTimeAfterDungeon());       
    }

    private static void PushLootTheMainInventory()
    {
        List<ItemStack> loots = DungeonLootPanelManager.dungeonLootPanelManager.GetLootsFromAllCharacters();
        foreach (ItemStack item in loots)
        {
            Inventaire.inventaire.AddItem(item);
        }
    }
}
