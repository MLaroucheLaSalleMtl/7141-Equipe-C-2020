using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public enum TooltipType { UImodule, SimpleText };

public class TooltipHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    GameObject tooltip;
    [TextArea(2,4)]
    [SerializeField] string simpleTextString;
    [SerializeField] TooltipType tooltipsType;
    TextMeshProUGUI tooltipText;

    // Start is called before the first frame update
    void Start()
    {
        tooltip = FindObjectOfType<Tooltip>().gameObject;
        tooltipText = tooltip.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.transform.position = transform.position;
        switch (tooltipsType)
        {
            case TooltipType.UImodule:
                tooltipText.text = GetComponent<UIModule>().moduleDescription;
                break;
            case TooltipType.SimpleText:
                tooltipText.text = simpleTextString;
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.transform.localPosition = new Vector3(1100, 0, 0);
    }

    private void OnMouseEnter()
    {
        tooltip.transform.position = Camera.main.WorldToScreenPoint(transform.GetComponentInChildren<TooltipSpawn>().transform.position);
        switch (tooltipsType)
        {
            case TooltipType.UImodule:
                tooltipText.text = GetComponent<UIModule>().moduleDescription;
                break;
            case TooltipType.SimpleText:
                tooltipText.text = simpleTextString;
                break;
        }
    }

    private void OnMouseExit()
    {
        tooltip.transform.localPosition = new Vector3(1100, 0, 0);
    }
}
