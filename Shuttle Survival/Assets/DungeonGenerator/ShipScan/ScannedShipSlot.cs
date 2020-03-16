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
        shipName.text = "Romano Fafard";
        chooseShipButton.onClick.AddListener(() => ShipScanManager.shipScanManager.LaunchDungeon(dungeonIndex));
    }
}
