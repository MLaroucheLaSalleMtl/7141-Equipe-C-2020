using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public enum TooltipType { UImodule, SimpleText, Asteroids, ItemUI, UnderconstructionModule, DungeonDoor };

public class TooltipHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    GameObject regularTooltipObject;
    GameObject itemTooltipObject;
    [TextArea(2,4)]
    [SerializeField] public string simpleTextString;
    [SerializeField] TooltipType tooltipsType;

    TextMeshProUGUI tooltipText;

    // Start is called before the first frame update
    void Start()
    {
        if(tooltipsType == TooltipType.ItemUI)
        {
            if (gameObject.scene.name == "SceneEspace")
                itemTooltipObject = FindObjectOfType<ShipSceneItemTooltip>().gameObject;
            else if (gameObject.scene.name == "DungeonGeneratorScene")
                itemTooltipObject = FindObjectOfType<DungeonSceneItemTooltip>().gameObject;

        }
        else
        {
            if(gameObject.scene.name == "SceneEspace")
            {
                regularTooltipObject = FindObjectOfType<Tooltip>().gameObject;
                tooltipText = regularTooltipObject.GetComponentInChildren<TextMeshProUGUI>();
            }
            else if(gameObject.scene.name == "DungeonGeneratorScene")
            {
                regularTooltipObject = FindObjectOfType<DungeonSceneTooltip>().gameObject;
                tooltipText = regularTooltipObject.GetComponentInChildren<TextMeshProUGUI>();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        bool regularTooltip = true;
        switch (tooltipsType)
        {
            case TooltipType.UImodule:
                tooltipText.text = GetComponent<UIModule>().moduleDescription;
                break;
            case TooltipType.SimpleText:
                tooltipText.text = simpleTextString;
                break;
            case TooltipType.Asteroids:
                tooltipText.text = GetComponent<Asteroid>().GetTooltipDescription();
                break;
            case TooltipType.ItemUI:
                regularTooltip = false;
                if (GetComponent<ItemUI>())
                {
                    ItemUI itemUI = GetComponent<ItemUI>();           
                    ItemClass itemClass = itemUI.GetItemHolded();
                    itemTooltipObject.GetComponent<ItemTooltip>().SetUpItemTooltip(itemClass.icon, itemClass.Nom, itemClass.ItemTier, itemClass.Description, itemUI.GetCurrentQuantity(), itemClass.MaxStack);
                }
                else
                {
                    DungeonLootItem itemUI = GetComponent<DungeonLootItem>();
                    ItemClass itemClass = itemUI.GetItemHolded();
                    itemTooltipObject.GetComponent<ItemTooltip>().SetUpItemTooltip(itemClass.icon, itemClass.Nom, itemClass.ItemTier, itemClass.Description, itemUI.GetCurrentQuantity(), itemClass.MaxStack);
                }
                break;           
        }
        if (regularTooltip)
        {
            regularTooltipObject.transform.position = transform.position;
        }
        else
        {
            itemTooltipObject.transform.position = transform.position;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipsType == TooltipType.ItemUI)
            itemTooltipObject.transform.localPosition = new Vector3(2200, 0, 0);
        else
            regularTooltipObject.transform.localPosition = new Vector3(2200, 0, 0);
    }

    private void OnMouseEnter()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (tooltipsType == TooltipType.Asteroids)
            {
                regularTooltipObject.transform.position = Camera.main.WorldToScreenPoint(GetComponent<Asteroid>().spawn.GetComponentInChildren<TooltipSpawn>().transform.position);
            }
            else
            {
                regularTooltipObject.transform.position = Camera.main.WorldToScreenPoint(transform.GetComponentInChildren<TooltipSpawn>().transform.position);
            }
            switch (tooltipsType)
            {
                case TooltipType.UImodule:
                    tooltipText.text = GetComponent<UIModule>().moduleDescription;
                    break;
                case TooltipType.SimpleText:
                    tooltipText.text = simpleTextString;
                    break;
                case TooltipType.Asteroids:
                    tooltipText.text = GetComponent<Asteroid>().GetTooltipDescription();
                    break;
                case TooltipType.UnderconstructionModule:
                    tooltipText.text = GetComponent<UnderConstructionModule>().moduleToBuild.GetComponent<Module>().moduleName + "\n" + "<align=\"center\">" + GetComponent<UnderConstructionModule>().turnsRemainingToBuild + " <sprite=0></align>";
                    break;
                case TooltipType.DungeonDoor:
                    tooltipText.text = GetComponent<DungeonDoor>().IsUnlocked() ? "Enter room." : "Unlocked for " + GetComponent<DungeonDoor>().numberOfTurnsToUnlock + "<sprite=0>";
                    break;
            }
        }
    }

    private void OnMouseExit()
    {
        regularTooltipObject.transform.localPosition = new Vector3(2200, 0, 0);
    }
}
