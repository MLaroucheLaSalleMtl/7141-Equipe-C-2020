using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DungeonLootItem : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler
{    
    [SerializeField] ItemClass itemHolded;
    [SerializeField] int quantity;
    [SerializeField] TextMeshProUGUI stackText;
    RectTransform parentGrid;
    Vector3 savedPos;
    RectTransform savedParent;
    Vector2 savedPivot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupDungeonLootItemUI(ItemStack lootItem, RectTransform parentGrid)
    {
        GetComponent<Image>().sprite = lootItem.Item.icon;        
        this.quantity = lootItem.Quantite;
        stackText.text = quantity.ToString();
        this.itemHolded = lootItem.Item;
        this.parentGrid = parentGrid;
    }

    public int GetCurrentQuantity()
    {
        return quantity;
    }

    public ItemClass GetItemHolded()
    {
        return itemHolded;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        savedPivot = GetComponent<RectTransform>().pivot;
        SetPivot(new Vector2(0.5f, 0.5f));
        GetComponent<TooltipHandler>().OnPointerExit(null);
        GetComponent<TooltipHandler>().enabled = false;
        savedPos = transform.position;
        savedParent = (RectTransform)transform.parent;
        transform.SetParent(DungeonLootPanelManager.dungeonLootPanelManager.GetTransformToPutChildRenderInFront());
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;        
    }

    public void OnDrop(PointerEventData eventData)
    {
        DungeonLootPanelManager.dungeonLootPanelManager.ManageDungeonLootDrop(this);   
        GetComponent<TooltipHandler>().enabled = true;        
    }    

    public void SetParentGridTransform(RectTransform parentGrid)
    {
        this.parentGrid = parentGrid;
        transform.SetParent(parentGrid);
    }

    public RectTransform GetParentGridTransform()
    {
        return parentGrid;
    }

    public void GetParentBack()
    {
        transform.SetParent(savedParent);
        SetPivot(savedPivot);
    }

    public RectTransform GetSavedParent()
    {
        return savedParent;
    }

    public void SetPivot(Vector2 pivot)
    {
        GetComponent<RectTransform>().pivot = pivot;
    }

    public ItemStack GetItemStack()
    {
        return new ItemStack(quantity, itemHolded);
    }
}
