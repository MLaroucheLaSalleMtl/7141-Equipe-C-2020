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
            newOption.GetComponent<Button>().onClick.AddListener(() =>
            {
                dungeonOption.relatedDungeonRoll.Roll();
            });
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
