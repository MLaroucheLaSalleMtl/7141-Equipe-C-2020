using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager timeManager;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI dayText;
    public int currentDays = 1;
    public int currentMins;
    [SerializeField] int turnInMinutes = 60;
    [SerializeField] GameObject timeManagerUIHolder;
    [SerializeField] Button skipTimeButton;
    int elapsedTimeInDungeon = 0;
   // public delegate void TimeChangedEventHandler(int turnsAmount);
    //public event TimeChangedEventHandler TimeChanged;

    public event EventHandler OnTimeChanged;

    public void OnTimeChanged_Caller()
    {
        if(OnTimeChanged != null)
        {
            OnTimeChanged(this, EventArgs.Empty);
        }
    }

    private void Awake()
    {
        if (TimeManager.timeManager == null)
        {
            TimeManager.timeManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RefreshClock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshClock()
    {
        if(currentMins >= 1440)
        {
            currentMins -= 1440;
            currentDays++;
        }
        if(currentMins % 60 == 0)
        {
            timeText.text = currentMins / 60 + ":00";

        }
        else
        {
            timeText.text = currentMins / 60 + ":30";
        }
        dayText.text = currentDays.ToString();
    }

    public void AddTurns(int turnsAmount)
    {
        currentMins += turnsAmount * turnInMinutes;
        RefreshClock();
        PanelManager.panelManager.OnPanelOpened_Caller();
        for (int i = 0; i < turnsAmount; i++)
        {
            OnTimeChanged_Caller();
        }
        ShipEventsManager.shipEventsManager.FreeShipEventsQueue();
    }

    public void ToggleUI()
    {
        timeManagerUIHolder.SetActive(!timeManagerUIHolder.activeSelf);
    }

    public void DisableSkipTimeButton()
    {
        skipTimeButton.interactable = false;
    }

    public void EnableSkipTimeButton()
    {
        skipTimeButton.interactable = true;
    }

    public void ReceiveElapsedTimeInDungeon(int elapsedTimeInDungeon)
    {
        this.elapsedTimeInDungeon = elapsedTimeInDungeon;
    }

    public void AddTimeAfterDungeon()
    {
        AddTurns(elapsedTimeInDungeon);
    }
}
