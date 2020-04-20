using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ScrapsManager : MonoBehaviour
{    
    public static ScrapsManager scrapsManager;
    [SerializeField] GameObject scrapsPanel;
    [SerializeField] GameObject cleanUpText;
    [SerializeField] GameObject alreadyBeingCleanedUpText;
    [SerializeField] Button cleanOrCancelButton;
    Scraps currentScraps;
    shipNPCmanager NPC;
    public bool firstScrapsCleanedUp = false;

    private void Awake()
    {
        if(ScrapsManager.scrapsManager == null)
        {
            ScrapsManager.scrapsManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PanelManager.panelManager.OnPanelOpened += OnPanelOpened;
        NPC = shipNPCmanager.NPCmanagInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPanelOpened(object source, EventArgs args)
    {
        scrapsPanel.SetActive(false);
        if (currentScraps)
            currentScraps.GetComponent<TooltipHandler>().OnPointerExit(null);
    }

    public void OpenScrapsPanel(Scraps currentScraps)
    {
        AudioManager.audioManager.PlaySoundEffect(SoundEffectsType.ButtonClick);
        PanelManager.panelManager.OnPanelOpened_Caller();
        this.currentScraps = currentScraps;
        scrapsPanel.SetActive(true);
        alreadyBeingCleanedUpText.SetActive(this.currentScraps.beingCleaned);
        cleanUpText.SetActive(!this.currentScraps.beingCleaned);
        SetupPanelButton();        
    }

    private void SetupPanelButton()
    {
        cleanOrCancelButton.onClick.RemoveAllListeners();
        if (!currentScraps.beingCleaned)
        {
            cleanOrCancelButton.onClick.AddListener(() =>
            { 
                if (NPC.IsNPCavailable() == true)
                {
                    Debug.Log("Sending bob");
                    currentScraps.BeginCleanUp();
                    //trouver le bon endroit pour envoyer bob      
                    NPC.NeedAHandOverHere(currentScraps.transform);
                    currentScraps.bob = GameManager.selection;
                    currentScraps.bob.Unavailable();
                }
                else
                {
                    MessagePopup.MessagePopupManager.SetStringAndShowPopup("Select someone to go upgrade");
                    return;
                    //plus tard on pourrait peut-être mettre ici le drop down list avec toutes les persos
                }
            });
            cleanOrCancelButton.GetComponentInChildren<TextMeshProUGUI>().text = "CLEAN";
        }
        else
        {
            cleanOrCancelButton.onClick.AddListener(() => currentScraps.CancelCleanUp());
            cleanOrCancelButton.GetComponentInChildren<TextMeshProUGUI>().text = "CANCEL";
        }
        cleanOrCancelButton.onClick.AddListener(() =>
        {
            scrapsPanel.SetActive(false);
            currentScraps.GetComponent<TooltipHandler>().OnPointerExit(null);
        });
    }
}

