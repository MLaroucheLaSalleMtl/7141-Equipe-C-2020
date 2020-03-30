using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonCharacterUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] Image characterImageSlot;
    [SerializeField] Image healthBarFillImage;
    [SerializeField] TextMeshProUGUI healthBarText;
    [SerializeField] Image toolImage;
    [SerializeField] Image perk1Image, perk2Image;
    [SerializeField] Transform characterInventoryGridTransform;
    [SerializeField] GameObject inGameItemPrefab;
    [SerializeField] GameObject chooseCharacterButtonForOption;
    [SerializeField] TextMeshProUGUI backPackCapacityText;
    [SerializeField] TextMeshProUGUI statsPointsText;

    List<GameObject> characterItemSlots = new List<GameObject>();
    CharacterInfo ci;
    CharacterSystem linkedCharacter;

    int backPackCapacity;
    int carriedItemCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupUI(CharacterSystem characterSystem)
    {
        linkedCharacter = characterSystem;
        ci = linkedCharacter.GetInfoForCharacterDungeonUI();
        foreach (Transform transform in characterInventoryGridTransform)
        {
            Destroy(transform.gameObject);
        }
        backPackCapacity = ci.backPackCapacity;        
        //take list of item from characterinfo and load it in the inventory.
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (linkedCharacter)
        {
            ci = linkedCharacter.GetInfoForCharacterDungeonUI();
            characterName.text = ci.characterName;
            characterImageSlot.sprite = ci.characterSprite;

            healthBarFillImage.fillAmount = ci.currentHp / (float)ci.maxHp;
            healthBarText.text = ci.currentHp + " / " + ci.maxHp;

            toolImage.sprite = ci.equipedTool ? ci.equipedTool.toolSprite : null;
            perk1Image.sprite = ci.perk1 ? ci.perk1.perkSprite : null;
            perk2Image.sprite = ci.perk2 ? ci.perk2.perkSprite : null;
            toolImage.GetComponent<TooltipHandler>().simpleTextString = ci.equipedTool.toolDescription;
            perk1Image.GetComponent<TooltipHandler>().simpleTextString = ci.perk1.perkDescription;
            perk2Image.GetComponent<TooltipHandler>().simpleTextString = ci.perk2.perkDescription;
            backPackCapacityText.text = "Backpack : " + carriedItemCount + " / " + backPackCapacity;
            statsPointsText.text = "<color=\"red\">" + ci.strength + " str \n" +
                                   "<color=\"yellow\">" + ci.tinkering + " tink \n" +
                                   "<color=\"purple\">" + ci.charisma + " char ";               
        }
    }

    public GameObject GetChooseCharacterButton()
    {
        return chooseCharacterButtonForOption;
    }

    public CharacterSystem GetLinkedCharacter()
    {
        return linkedCharacter;
    }

    public bool CheckIfThereIsPlaceInBackpack()
    {
        return carriedItemCount < backPackCapacity;
    }

    public void AddItemToBackpack()
    {
        carriedItemCount++;
    }

    public void RemoveItemFromBackpack()
    {
        carriedItemCount--;
    }
}
    
