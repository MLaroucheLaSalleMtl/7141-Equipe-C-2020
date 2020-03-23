using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemTierText;
    [SerializeField] TextMeshProUGUI itemDescriptionText;
    [SerializeField] TextMeshProUGUI itemStackText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpItemTooltip(Sprite itemSprite, string itemName, ItemTier itemTier, string itemDescription, int currentQuantity, int maxQuantity)
    {
        itemImage.sprite = itemSprite;
        itemDescriptionText.text = itemDescription;
        itemStackText.text = "Stack: " + currentQuantity + "/" + maxQuantity;
        switch (itemTier)
        {
            case ItemTier.Consum:
                itemTierText.text = "<color=#c94747>Consumables";
                itemNameText.text = "<color=#c94747>" + itemName;
                break;
            case ItemTier.Tier1:
                itemNameText.text = "<color=#b5a7b6>" + itemName;
                itemTierText.text = "<color=#b5a7b6>Tier 1";
                break;
            case ItemTier.Tier2:
                itemNameText.text = "<color=#93c8e7>" + itemName;
                itemTierText.text = "<color=#93c8e7>Tier 2";
                break;
            case ItemTier.Tier3:
                itemNameText.text = "<color=#bd23e3>" + itemName;
                itemTierText.text = "<color=#bd23e3>Tier 3";
                break;
            case ItemTier.Tier4:
                itemNameText.text = "<color=#e1b000>" + itemName;
                itemTierText.text = "<color=#e1b000>Tier 4";
                break;
            case ItemTier.Vide:
                itemNameText.text = "<color=#b5a7b6>" + itemName;
                itemTierText.text = "";
                itemStackText.text = "";
                break;
        }
    }
}
