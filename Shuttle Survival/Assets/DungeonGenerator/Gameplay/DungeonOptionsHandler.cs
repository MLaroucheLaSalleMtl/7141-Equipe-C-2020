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
    List<DungeonCharacterUI> currentListOfPassingCharacters = new List<DungeonCharacterUI>();

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
        foreach (DungeonOption dungeonOption in dungeonOptions) 
        {
            ResetListOfPassingCharacters();
            if (DungeonOptionRequirementsHandler.IsRequirementMet(dungeonOption.requirementsToBeActive))
            {
                GameObject newOption = Instantiate(dungeonEventOptionPrefab, dungeonEventOptionsGrid);
                newOption.GetComponentInChildren<TextMeshProUGUI>().text = "";
                if (dungeonOption.requirementsToBeActive.Count > 0)
                {
                    newOption.GetComponentInChildren<TextMeshProUGUI>().text +=
                        DungeonOptionsTextSpriteAdder.AddTextAndSpritesAccordingToOptionsRequirements(dungeonOption.requirementsToBeActive);
                }
                newOption.GetComponentInChildren<TextMeshProUGUI>().text += dungeonOption.optionText;
                if (dungeonOption.chooseCharacterForOption)
                {
                    List<DungeonCharacterUI> tempList = new List<DungeonCharacterUI>(currentListOfPassingCharacters);
                    newOption.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        DungeonCharacterManager.dungeonCharacterManager.SetupChooseCharacterForOption(dungeonOption, new List<DungeonCharacterUI>(tempList));
                        foreach (Transform transform in dungeonEventOptionsGrid)
                        {
                            if (transform.gameObject != newOption)
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
                else if (dungeonOption.relatedDungeonRoll.successEvent == null && dungeonOption.relatedDungeonRoll.failureEvent == null)
                {
                    newOption.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        DungeonEventPanelHandler.dungeonEventPanelHandler.EndDungeonEvent();
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
    }

    private void DestroyPreviousDungeonOptions()
    {
        foreach (Transform transform in dungeonEventOptionsGrid)
        {
            Destroy(transform.gameObject);
        }
    }

    public void SetCurrentListOfPassingCharacters(List<DungeonCharacterUI> listOfPassingCharacters)
    {
        currentListOfPassingCharacters = listOfPassingCharacters;
    }

    public void ResetListOfPassingCharacters()
    {
        currentListOfPassingCharacters = new List<DungeonCharacterUI>();
    }
}
