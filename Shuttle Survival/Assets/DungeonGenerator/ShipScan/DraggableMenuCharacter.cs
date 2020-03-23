using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableMenuCharacter : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler
{
    CharacterSystem linkedCharacter;
    Vector3 savedPos;
    bool stayAtHome = true;

    public void OnBeginDrag(PointerEventData eventData)
    {
        savedPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        BoardingCrewSelectionManager.boardingCrewSelectionManager.CheckIfHoveringFrame(stayAtHome);
    }

    public void OnDrop(PointerEventData eventData)
    {
        //if stay at home, watch if in boardcrew zone, if yes, remove it from stayathome list 
        //and put it in boardcrew list, move its object under its transform grid. if anywhere else, return to current list.
        if (BoardingCrewSelectionManager.boardingCrewSelectionManager.IsItDroppedInAnotherTeam(stayAtHome))
        {
            BoardingCrewSelectionManager.boardingCrewSelectionManager.SwitchCharacterList(this, stayAtHome);
            stayAtHome = !stayAtHome;
        }
        else
        {
            transform.position = savedPos;
        }
        BoardingCrewSelectionManager.boardingCrewSelectionManager.ResetFrameColor();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLinkedCharacter(CharacterSystem characterToLink)
    {
        linkedCharacter = characterToLink;
        GetComponent<Image>().sprite = linkedCharacter.CharacterSprite;
    }

    public CharacterSystem GetLinkedCharacter()
    {
        return linkedCharacter;
    }
}
