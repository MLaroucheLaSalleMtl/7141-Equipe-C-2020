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

    List<GameObject> characterItemSlots = new List<GameObject>();
    CharacterInfo ci;
    CharacterSystem linkedCharacter;
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
        for (int i = 0; i < ci.backPackCapacity; i++)
        {
            GameObject newItemSlot = Instantiate(inGameItemPrefab, characterInventoryGridTransform);
            characterItemSlots.Add(newItemSlot);
        }
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
}
    
