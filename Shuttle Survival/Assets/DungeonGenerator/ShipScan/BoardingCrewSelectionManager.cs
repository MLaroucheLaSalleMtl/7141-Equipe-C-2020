using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardingCrewSelectionManager : MonoBehaviour
{
    public static BoardingCrewSelectionManager boardingCrewSelectionManager;

    List<CharacterSystem> stayAtHomeCrewList = new List<CharacterSystem>();
    List<CharacterSystem> boardingCrewList = new List<CharacterSystem>();

    [SerializeField] RectTransform boardingCrewRectTransform;
    [SerializeField] RectTransform stayAtHomeCrewRectTransform;
    [SerializeField] Image boardingCrewFrameImage;
    [SerializeField] Image stayAtHomeCrewFrameImage;

    [SerializeField] GameObject draggableMenuCharacterPrefab;
    [SerializeField] Color atRestFrameColor;

    private void Awake()
    {
        if(boardingCrewSelectionManager == null)
        {
            boardingCrewSelectionManager = this;
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

    public void ManageBoardingSelection(List<CharacterSystem> listOfShipCharacters)
    {
        CleanUpPastCharatersList();

        stayAtHomeCrewList = listOfShipCharacters;
        foreach (CharacterSystem character in stayAtHomeCrewList)
        {
            GameObject draggableMenuCharacter = Instantiate(draggableMenuCharacterPrefab, stayAtHomeCrewRectTransform);
            draggableMenuCharacter.GetComponent<DraggableMenuCharacter>().SetLinkedCharacter(character);
        }
    }

    private void CleanUpPastCharatersList()
    {
        stayAtHomeCrewList.Clear();
        boardingCrewList.Clear();
        foreach (Transform transform in boardingCrewRectTransform)
        {
            Destroy(transform.gameObject);
        }
        foreach (Transform transform in stayAtHomeCrewRectTransform)
        {
            Destroy(transform.gameObject);
        }
    }

    public void SwitchCharacterList(DraggableMenuCharacter characterToSwitch, bool stayAtHomeCrew)
    {
        if (stayAtHomeCrew)
        {
            characterToSwitch.transform.SetParent(boardingCrewRectTransform);
            boardingCrewList.Add(characterToSwitch.GetLinkedCharacter());
            stayAtHomeCrewList.Remove(characterToSwitch.GetLinkedCharacter());
        }
        else
        {
            characterToSwitch.transform.SetParent(stayAtHomeCrewRectTransform);
            stayAtHomeCrewList.Add(characterToSwitch.GetLinkedCharacter());
            boardingCrewList.Remove(characterToSwitch.GetLinkedCharacter());
        }
    }

    public bool IsItDroppedInAnotherTeam(bool stayAtHomeCrew)
    {
        if(stayAtHomeCrew)
            return RectTransformUtility.RectangleContainsScreenPoint(boardingCrewRectTransform, Input.mousePosition);
        else
            return RectTransformUtility.RectangleContainsScreenPoint(stayAtHomeCrewRectTransform, Input.mousePosition);
    }

    public void CheckIfHoveringFrame(bool stayAtHome)
    {
        if (stayAtHome)
        {
            boardingCrewFrameImage.color = Color.white;           
        }
        else
        {
            stayAtHomeCrewFrameImage.color = Color.white;
        }
    }

   public void ResetFrameColor()
   {
        stayAtHomeCrewFrameImage.color = atRestFrameColor;
        boardingCrewFrameImage.color = atRestFrameColor;
   }

    public CharacterSystem[] GetBoardingCrew()
    {
        return boardingCrewList.ToArray();
    }
}
