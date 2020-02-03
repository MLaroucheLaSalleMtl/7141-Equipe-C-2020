using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Module : MonoBehaviour
{
    public Sprite sprite;
    public ModuleType moduleType;
    public bool salvageable = false;
    public bool useable = false;
    public bool unique = false;
    public bool upgradable = false;
    public string moduleName;
    [TextArea(2, 5)]
    public string moduleDescription;
    bool interactable = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(interactable && Input.GetMouseButtonDown(0))
        {
            ModuleManager.moduleManager.GenerateModulePanel(this);
        }
    }

    private void OnMouseEnter()
    {
        interactable = true;
        GetComponent<SpriteRenderer>().color = ModuleManager.moduleManager.hoverColor;
    }

    private void OnMouseExit()
    {
        interactable = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public virtual void UseModule()
    {

    }

    public virtual void OnCreation()
    {

    }

}
