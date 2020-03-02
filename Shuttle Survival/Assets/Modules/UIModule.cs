using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIModule : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Image myImage;
    public int moduleIndex;
    public string moduleDescription;

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<TooltipHandler>().OnPointerExit(null);
        ModuleManager.moduleManager.ShowDetailedModuleInformations(moduleIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        myImage.color = ModuleManager.moduleManager.hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myImage.color = Color.white;
    }

    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
