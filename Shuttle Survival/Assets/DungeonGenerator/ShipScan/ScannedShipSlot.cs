using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScannedShipSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI shipName;
    [SerializeField] Image shipSprite;
    [SerializeField] TextMeshProUGUI shipDescription;
    [SerializeField] Button chooseShipButton;
    [SerializeField] TextMeshProUGUI chooseShipButtonText;
    [SerializeField] int dungeonIndex;
    DungeonSheet currentDungeonSheet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void ReceiveDungeonSheetAndSetupUI(DungeonSheet dungeonSheet)
    {
        //read data from assigned dungeon sheet
        currentDungeonSheet = dungeonSheet;
        shipName.text = "Romano Fafard";        
        shipSprite.sprite = dungeonSheet.dungeonSprite;
        shipDescription.text = DungeonKeywordsTranslater.dungeonKeywordsTranslater.TranslateDungeonKeywordsIntoDescription(dungeonSheet.dungeonKeywords);
        chooseShipButton.onClick.AddListener(() => ShipScanManager.shipScanManager.PrepareBoardingCrewSelection(dungeonIndex));
    }

    public DungeonSheet GetDungeonSheet()
    {
        return currentDungeonSheet;
    }
}
