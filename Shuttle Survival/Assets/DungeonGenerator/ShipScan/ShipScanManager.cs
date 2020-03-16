using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipScanManager : MonoBehaviour
{
    public static ShipScanManager shipScanManager;
    [SerializeField] DungeonSheet[] possibleDungeons;
    [SerializeField] GameObject shipScannerDungeonsPanel;
    [SerializeField] ScannedShipSlot[] scannedShipSlots;

    [SerializeField] GameObject SHIP_SCENE_HOLDER;

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
       SceneManager.LoadSceneAsync("DungeonGeneratorScene", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpShipScannerDungeonPanel()
    {
        foreach (var scannedShipSlot in scannedShipSlots)
        {
            scannedShipSlot.gameObject.SetActive(false);
        }

        scannedShipSlots[0].gameObject.SetActive(true);
        scannedShipSlots[0].ReceiveDungeonSheetAndSetupUI(possibleDungeons[0]);
        shipScannerDungeonsPanel.SetActive(true);

    }

    public void LaunchDungeon(int dungeonIndex)
    {
        //SCREEN TRANSITION, WHEN FADED TO BLACK, ERASE CURRENT SCENE AND LOAD DUNGEON 
        SHIP_SCENE_HOLDER.SetActive(false);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("DungeonGeneratorScene")); //ON switch le active scene pour que nos instantie se fasse dans l'autre, on la wipera plus tard apres le dungeon.
        RoomTemplates.roomTemplates.StartDungeonSetup(possibleDungeons[0]);
    }
}
