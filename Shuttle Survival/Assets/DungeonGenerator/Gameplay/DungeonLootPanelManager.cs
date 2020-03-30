using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creer prefab lootitem
//script dungeon loot (draggable bla bla)
//creer la gestion des drag dans le manager script
//pensez a rendre les inventaire des character draggable quand on est dans le loot window. (activate their script)
public class DungeonLootPanelManager : MonoBehaviour
{
    public static DungeonLootPanelManager dungeonLootPanelManager;
    [SerializeField] GameObject dungeonLootPanel;
    [SerializeField] RectTransform lootGrid;
    [SerializeField] RectTransform[] charactersInventoryGrids;
    [SerializeField] GameObject lootItemPrefab;
    [SerializeField] Transform transformForChildInFront;
    [SerializeField] Vector2 characterInventoryItemPivot;
    [SerializeField] Vector2 lootPanelItemPivot;

    private void Awake()
    {
        if(dungeonLootPanelManager == null)
        {
            dungeonLootPanelManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReceiveLootAndFeedThePanel(ItemStack[] lootItems)
    {
        ClearPreviousLoot();
        foreach (ItemStack itemStack in lootItems)
        {
            GameObject newItem = Instantiate(lootItemPrefab, lootGrid);
            newItem.GetComponent<DungeonLootItem>().SetupDungeonLootItemUI(itemStack, lootGrid);           
        }
    }

    private void ClearPreviousLoot()
    {
        foreach (Transform child in lootGrid)
        {
            Destroy(child.gameObject);
        }
    }

    public void ManageDungeonLootDrop(DungeonLootItem dungeonLootItem)
    {
        if(CheckIfRectActiveAndMouseInsideAndIfItemChangesRect(lootGrid, dungeonLootItem))
        {
            RemoveFromBackpackCountIfApplicable(dungeonLootItem);
            dungeonLootItem.SetParentGridTransform(lootGrid);
            dungeonLootItem.SetPivot(lootPanelItemPivot);
            RefreshCharactersUI();
            return;
        }
        else
        {
            for (int i = 0; i < charactersInventoryGrids.Length; i++)
            {
                if (CheckIfRectActiveAndMouseInsideAndIfItemChangesRect(charactersInventoryGrids[i], dungeonLootItem))
                {
                    if (DungeonCharacterManager.dungeonCharacterManager.CheckIfThereIsPlaceInCharacterBackpack(i))
                    {
                        RemoveFromBackpackCountIfApplicable(dungeonLootItem);
                        DungeonCharacterManager.dungeonCharacterManager.AddItemToBackpack(i);
                        dungeonLootItem.SetParentGridTransform(charactersInventoryGrids[i]);
                        dungeonLootItem.SetPivot(characterInventoryItemPivot);
                        RefreshCharactersUI();
                        return;
                    }
                }
            }
        }
        dungeonLootItem.GetParentBack();
    }

    public bool CheckIfRectActiveAndMouseInsideAndIfItemChangesRect(RectTransform rectTransform, DungeonLootItem dungeonLootItem)
    {                    
        return rectTransform.gameObject.activeInHierarchy && 
            dungeonLootItem.GetParentGridTransform() != rectTransform && 
            RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition);        
    }

    private void RemoveFromBackpackCountIfApplicable(DungeonLootItem dungeonLootItem)
    {
        if (dungeonLootItem.GetSavedParent().GetComponentInParent<DungeonCharacterUI>())
        {
            dungeonLootItem.GetSavedParent().GetComponentInParent<DungeonCharacterUI>().RemoveItemFromBackpack();
        }
    }

    public Transform GetTransformToPutChildRenderInFront()
    {
        return transformForChildInFront;
    }

    private void RefreshCharactersUI()
    {
        DungeonCharacterManager.dungeonCharacterManager.RefreshEveryCharacterUI();
    }

    public void ActivateDungeonLootPanel()
    {
        dungeonLootPanel.SetActive(true);
    }
    
    public void DesactivateDungeonLootPanel()
    {
        dungeonLootPanel.SetActive(false);
    }
}
