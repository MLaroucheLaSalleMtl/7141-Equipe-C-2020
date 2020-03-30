using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipScanManager : MonoBehaviour
{
    public static ShipScanManager shipScanManager;
    [SerializeField] int turnsBetweenScans = 5;
    [SerializeField] TextMeshProUGUI turnBeforeScanText;
    [SerializeField] DungeonSheet[] possibleDungeons;
    [SerializeField] GameObject shipScannerDungeonsPanel;
    [SerializeField] GameObject boardingCrewSelectionPanel;
    [SerializeField] ScannedShipSlot[] scannedShipSlots;
    [SerializeField] ScannedShipSlot choosedScannedShipSlot;

    [Range(0, 3)]
    [SerializeField] int scannerCapacity;
    [SerializeField] GameObject SHIP_SCENE_HOLDER;
    int turnCounter = 0;
    [SerializeField] CharacterSystem[] TESTCHARACTERS;
    int selectedShipSlotIndex;

    private void Awake()
    {
        if(shipScanManager == null)
        {
            shipScanManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       turnCounter = turnsBetweenScans;
       SceneManager.LoadSceneAsync("DungeonGeneratorScene", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUpShipScannerDungeonPanel()
    {

        PanelManager.panelManager.OnPanelOpened(this, EventArgs.Empty);
        foreach (var scannedShipSlot in scannedShipSlots)
        {
            scannedShipSlot.gameObject.SetActive(false);
        }

        for (int i = 0; i < scannerCapacity; i++)
        {
            scannedShipSlots[i].gameObject.SetActive(true);
        }
        shipScannerDungeonsPanel.SetActive(true);
    }

    private DungeonSheet RollRandomDungeonSheet()
    {
        int rand = UnityEngine.Random.Range(0, possibleDungeons.Length);
        return possibleDungeons[rand];
    }

    public void PrepareBoardingCrewSelection(int dungeonIndex)
    {
        shipScannerDungeonsPanel.SetActive(false);
        boardingCrewSelectionPanel.SetActive(true);
        choosedScannedShipSlot.ReceiveDungeonSheetAndSetupUI(scannedShipSlots[selectedShipSlotIndex].GetDungeonSheet());
        BoardingCrewSelectionManager.boardingCrewSelectionManager.ManageBoardingSelection(GameManager.GM.Personnages);       
    }

    public void LaunchDungeon()
    {
        //SCREEN TRANSITION, WHEN FADED TO BLACK, ERASE CURRENT SCENE AND LOAD DUNGEON 
        SHIP_SCENE_HOLDER.SetActive(false);        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("DungeonGeneratorScene")); //ON switch le active scene pour que nos instantie se fasse dans l'autre, on la wipera plus tard apres le dungeon.
        RoomTemplates.roomTemplates.StartDungeonSetup(scannedShipSlots[selectedShipSlotIndex].GetDungeonSheet());
        DungeonCharacterManager.dungeonCharacterManager.SetupDungeonCharactersUI(BoardingCrewSelectionManager.boardingCrewSelectionManager.GetBoardingCrew());
    }

    public void OnTimeChanged(object source, EventArgs e)
    {
        turnCounter--;
        if (turnCounter == 0)
        {
            ScanForShips();
            turnCounter = turnsBetweenScans;
        }
        turnBeforeScanText.text = "Turns until new scan : " + turnCounter;
        
    }

    public void ScanForShips()
    {
        for (int i = 0; i < scannedShipSlots.Length; i++)
        {
            scannedShipSlots[i].ReceiveDungeonSheetAndSetupUI(RollRandomDungeonSheet());
        }
    }

    public void ActivateScannerCountdown()
    {
        ScanForShips();
        turnBeforeScanText.text = "Turns until new scan : " + turnCounter;
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
    }

    public void TESTING_MANUALLY_SETUP_CHARACTER_UI()
    {
        DungeonCharacterManager.dungeonCharacterManager.SetupDungeonCharactersUI(TESTCHARACTERS);
    }
}
