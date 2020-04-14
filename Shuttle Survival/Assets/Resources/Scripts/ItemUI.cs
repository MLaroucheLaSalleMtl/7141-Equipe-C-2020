using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemUI : MonoBehaviour
{
    [SerializeField] ItemClass itemHolded;
    [SerializeField] int quantity;
    [SerializeField] TextMeshProUGUI stackText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupItemUI(Sprite itemImage, int quantity, ItemClass itemHolded, bool cost = false)
    {
        stackText.text = "";
        this.quantity = quantity;
        this.itemHolded = itemHolded;

        GetComponent<Image>().sprite = itemImage;
        if (cost)
        {
            int amountInInventory = Inventaire.inventaire.GetAmount(itemHolded.ItemID);
            stackText.text = amountInInventory + " / " + quantity.ToString();
            stackText.color = (amountInInventory >= quantity) ? Color.green : Color.red;
        }
        else
        {
            stackText.color = Color.white;
            stackText.text = quantity.ToString();
        }
    }

    public int GetCurrentQuantity()
    {
        return quantity;
    }

    public ItemClass GetItemHolded()
    {
        return itemHolded;
    }
}
