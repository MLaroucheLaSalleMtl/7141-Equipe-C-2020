using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.surSelectable = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.surSelectable = false;
    }
}
