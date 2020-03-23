using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonOptionsHandler : MonoBehaviour
{
    public static DungeonOptionsHandler dungeonOptionsHandler;
    [SerializeField] Transform dungeonEventOptionsGrid;
    [SerializeField] GameObject dungeonEventOptionPrefab;

    private void Awake()
    {
        if(dungeonOptionsHandler == null)
        {
            dungeonOptionsHandler = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetupDungeonOptions(List<DungeonOption> dungeonOptions)
    {
        DestroyPreviousDungeonOptions();

        if(dungeonOptions.Count <= 0) //Set default close option
        {
            GameObject newOption = Instantiate(dungeonEventOptionPrefab, dungeonEventOptionsGrid);
            newOption.GetComponentInChildren<TextMeshProUGUI>().text = "Close.";
            newOption.GetComponent<Button>().onClick.AddListener(() =>
            {
                DungeonEventPanelHandler.dungeonEventPanelHandler.EndDungeonEvent();
            });
        }
        foreach (DungeonOption dungeonOption in dungeonOptions) //see if we respect requirement later 
        {           
            GameObject newOption = Instantiate(dungeonEventOptionPrefab, dungeonEventOptionsGrid);
            newOption.GetComponentInChildren<TextMeshProUGUI>().text = dungeonOption.optionText;
            if (dungeonOption.chooseCharacterForOption)
            {
                newOption.GetComponent<Button>().onClick.AddListener(() =>
                {
                    DungeonCharacterManager.dungeonCharacterManager.SetupChooseCharacterForOption(dungeonOption);
                    foreach (Transform transform in dungeonEventOptionsGrid)
                    {
                        if(transform.gameObject != newOption)
                        {
                            transform.GetComponent<Button>().interactable = false;
                        }
                        else
                        {
                            transform.GetComponent<Button>().onClick.RemoveAllListeners();
                            transform.GetComponent<Button>().onClick.AddListener(() => 
                            {
                                SetupDungeonOptions(dungeonOptions);
                                DungeonCharacterManager.dungeonCharacterManager.DisableChooseCharacterButtons();
                            });
                        }
                    }
                });
            }
            else
            {
                newOption.GetComponent<Button>().onClick.AddListener(() =>
                {
                    dungeonOption.relatedDungeonRoll.Roll();
                });
            }            
        }
    }

    private void DestroyPreviousDungeonOptions()
    {
        foreach (Transform transform in dungeonEventOptionsGrid)
        {
            Destroy(transform.gameObject);
        }
    }
}
