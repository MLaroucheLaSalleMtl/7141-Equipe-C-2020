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
        stackText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupItemUI(Sprite itemImage, int quantity, ItemClass itemHolded)
    {
        GetComponent<Image>().sprite = itemImage;
        this.quantity = quantity;
        stackText.text = quantity.ToString();
        this.itemHolded = itemHolded;
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
