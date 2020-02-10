using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Module : MonoBehaviour
{
    public Sprite sprite;
    public ModuleType moduleType;
    public int turnsToBuild = 1;
    public bool salvageable = false;
    public bool useable = false;
    public bool unique = false;
    public bool upgradable = false;
    public string moduleName;
    public ResourcesCost ressourcesCost;
    [TextArea(2, 5)]
    public string moduleDescription;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        ModuleManager.moduleManager.hoveredModule = this;
        GetComponent<SpriteRenderer>().color = ModuleManager.moduleManager.hoverColor;
    }

    private void OnMouseExit()
    {
        ModuleManager.moduleManager.hoveredModule = null;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public virtual void UseModule()
    {

    }

    public virtual void OnCreation()
    {

    }

}
