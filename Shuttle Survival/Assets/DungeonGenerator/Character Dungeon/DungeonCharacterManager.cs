using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonCharacterManager : MonoBehaviour
{
    public static DungeonCharacterManager dungeonCharacterManager;
    [SerializeField] DungeonCharacterUI[] charactersUI;

    private void Awake()
    {
        if(dungeonCharacterManager == null)
        {
            dungeonCharacterManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupDungeonCharactersUI(CharacterSystem[] charactersInDungeons)
    {
        for (int i = 0; i < charactersUI.Length; i++)
        {
            charactersUI[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < charactersInDungeons.Length; i++)
        {
            Debug.Log(i);
            charactersUI[i].SetupUI(charactersInDungeons[i]);
            charactersUI[i].gameObject.SetActive(true);
        }
    }

    public void SetupChooseCharacterForOption(DungeonOption dungeonOption, List<DungeonCharacterUI> passingCharacters)
    {
        foreach (DungeonCharacterUI characterUI in passingCharacters)
        {
            characterUI.GetChooseCharacterButton().SetActive(true);
            characterUI.GetChooseCharacterButton().GetComponent<Button>().onClick.AddListener(() =>
            {               
                DungeonEventPanelHandler.dungeonEventPanelHandler.SetCharacterForOption(characterUI.GetLinkedCharacter());
                dungeonOption.relatedDungeonRoll.Roll();
                DisableChooseCharacterButtons();
            });
        }
    }

    public void DisableChooseCharacterButtons()
    {
        foreach (DungeonCharacterUI characterUI in charactersUI)
        {
            characterUI.GetChooseCharacterButton().SetActive(false);
        }
    }

    public void RefreshEveryCharacterUI()
    {
        foreach (DungeonCharacterUI characterUI in charactersUI)
        {
            characterUI.RefreshUI();
        }
    }

    public bool CheckIfThereIsPlaceInCharacterBackpack(int characterIndex)
    {
        return charactersUI[characterIndex].CheckIfThereIsPlaceInBackpack();
    }

    public void AddItemToBackpack(int characterIndex)
    {
        charactersUI[characterIndex].AddItemToBackpack();
    }

    public void RemoveItemFromBackpack(int characterIndex)
    {
        charactersUI[characterIndex].RemoveItemFromBackpack();
    }

    public List<DungeonCharacterUI> GetActivePartyCharacters()
    {
        List<DungeonCharacterUI> partyCharacters = new List<DungeonCharacterUI>();
        foreach (DungeonCharacterUI character in charactersUI)
        {
            if (character.isActiveAndEnabled)
            {   
                partyCharacters.Add(character);
            }
        }
        return partyCharacters;
    }
}
