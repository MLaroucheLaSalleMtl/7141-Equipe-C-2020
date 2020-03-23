using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCompScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject panelInfo;
    [SerializeField] Sprite bootedCompSprite;
    Sprite computerOffSprite;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        computerOffSprite = spriteRenderer.sprite;
    }

    private void OnMouseDown()
    {        
        panelInfo.GetComponent<InfoPanelScript>().PanelUpdate();
    }

    private void OnMouseEnter()
    {
           GetComponent<SpriteRenderer>().color = ModuleManager.moduleManager.hoverColor;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void FlickerComputerAndBoot()
    {
        StartCoroutine(FlickerComputerLights());
            
    }

    private IEnumerator FlickerComputerLights()
    {
        spriteRenderer.sprite = bootedCompSprite;
        for (int i = 0; i < 5; i++)
        {           
            yield return new WaitForSecondsRealtime(0.3f);
            spriteRenderer.color = ModuleManager.moduleManager.hoverColor;
            spriteRenderer.sprite = computerOffSprite;
            yield return new WaitForSecondsRealtime(0.3f);
            spriteRenderer.sprite = bootedCompSprite;
            spriteRenderer.color = Color.white;
        }
    }
}
