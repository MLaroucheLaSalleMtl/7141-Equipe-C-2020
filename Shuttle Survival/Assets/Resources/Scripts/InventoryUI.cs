using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InventoryFilter { AllItems, Collapsed, Consumables, Tier1, Tier2, Tier3, Tier4}
public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject inventoryUIHolder;
    [SerializeField] Inventaire mainInventory;
    [SerializeField] ItemUI itemPrefab;
    [SerializeField] GameObject inventoryGridPanel;
    List<ItemUI> activeUIItems = new List<ItemUI>();
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Button[] filterTabsButtons;
    [SerializeField] GameObject selectedTabArrow;
    int currentFilter;
    bool uiActivable = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && uiActivable)
        {
            currentFilter = 0;
            ToggleMainInventoryUIPanel();
        }
    }

    public void ToggleMainInventoryUIPanel()
    {
        if (!inventoryUIHolder.activeInHierarchy)
        {
            OpenInventoryPanel();
        }
        else
        {
            DesactivateInventoryPanel();
        }
    }

    private void OpenInventoryPanel()
    {
        GetItemsFromMainInventory(currentFilter);
        inventoryUIHolder.SetActive(true);
    }

    private void DesactivateInventoryPanel()
    {
        inventoryUIHolder.SetActive(false);
        itemTooltip.transform.position = new Vector3(2200, 0, 0);
    }

    public void GetItemsFromMainInventory(int itemsFilterMode)
    {
        ClearPreviousListOfItemsUI();
        selectedTabArrow.transform.position = filterTabsButtons[itemsFilterMode].transform.position;
        ItemStack[] itemStacks = mainInventory.inventoryContent;       
        InventoryFilter inventoryFilter = (InventoryFilter)itemsFilterMode;
        if(inventoryFilter == InventoryFilter.Collapsed)
        {
            CollapsedItemFiltering(itemStacks);
        }
        else
        {
            StandardItemFiltering(itemStacks, inventoryFilter);
        }
    }

    private void CollapsedItemFiltering(ItemStack[] itemStacks)
    {
        List<ItemStack> collapsedItemStacks = new List<ItemStack>();
        foreach (ItemStack itemStack in itemStacks)
        {
            ItemStack clonedItemStack = itemStack.Clone(); //to lose reference
            if (SlotIsEmpty(itemStack))
            {
                continue;
            }
            bool stackMatching = false;
            for (int i = 0; i < collapsedItemStacks.Count; i++) //ce item ID est il deja dans la liste ?
            {
                if (itemStack.Item.ItemID == collapsedItemStacks[i].Item.ItemID) //we got a match
                {
                    collapsedItemStacks[i].Quantite += itemStack.Quantite;
                    stackMatching = true;
                    break;
                }
            }
            if (!stackMatching)
                collapsedItemStacks.Add(clonedItemStack);
        }
        foreach (ItemStack collapsedItem in collapsedItemStacks)
        {
            ItemUI newItem = Instantiate(itemPrefab, inventoryGridPanel.transform);
            newItem.SetupItemUI(collapsedItem.Item.icon, collapsedItem.Quantite, collapsedItem.Item);
            activeUIItems.Add(newItem);
        }
    }

    private void StandardItemFiltering(ItemStack[] itemStacks, InventoryFilter inventoryFilter)
    {
        bool specialFilter = false;
        ItemTier researchedTier = ItemTier.Debug;
        if (inventoryFilter != InventoryFilter.AllItems)
        {
            specialFilter = true;
            researchedTier = SetResearchedTier(inventoryFilter);
        }
        foreach (ItemStack itemStack in itemStacks)
        {
            if (SlotIsEmpty(itemStack) || (specialFilter && itemStack.Item.ItemTier != researchedTier))
            {
                continue;
            }
            ItemUI newItem = Instantiate(itemPrefab, inventoryGridPanel.transform);
            newItem.SetupItemUI(itemStack.Item.icon, itemStack.Quantite, itemStack.Item);
            activeUIItems.Add(newItem);
        }
    }

    private ItemTier SetResearchedTier(InventoryFilter inventoryFilter)
    {
        switch (inventoryFilter)
        {
            case InventoryFilter.Consumables:
                return ItemTier.Consum;
            case InventoryFilter.Tier1:
                return ItemTier.Tier1;
            case InventoryFilter.Tier2:
                return ItemTier.Tier2;
            case InventoryFilter.Tier3:
                return ItemTier.Tier3;
            case InventoryFilter.Tier4:
                return ItemTier.Tier4;
        }
        return ItemTier.Debug;

    }

    private static bool SlotIsEmpty(ItemStack itemStack)
    {
        return itemStack.Item.ItemID == -1;
    }

    private void ClearPreviousListOfItemsUI()
    {
        foreach (ItemUI itemUI in activeUIItems)
        {
            Destroy(itemUI.gameObject);
        }
        activeUIItems.Clear();
    }

    public void EnableInventoryUI()
    {        
        uiActivable = true;
    }
}
