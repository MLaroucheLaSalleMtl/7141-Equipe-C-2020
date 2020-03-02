using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonHoverColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color hoverColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = hoverColor;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.white;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
