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
    [SerializeField] bool limitedChoice = false;
    public List<GameObject> limitedModulePrefabsChoices;
    public ResourcesPack ressourcesCost;
    [TextArea(2, 5)]
    public string onCreationPopupString;
    [TextArea(2, 5)]
    public string createMenuModuleDescriptionTooltip;
    [TextArea(2, 5)]
    public string longModuleDescriptionForPanels;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        OnCreation();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            ModuleManager.moduleManager.hoveredModule = this;
            GetComponent<SpriteRenderer>().color = ModuleManager.moduleManager.hoverColor;
            MouseCursorManager.mouseCursorManager.SetCursor(MouseCursor.hoverCursor);
        }
    }

    private void OnMouseExit()
    {
        ModuleManager.moduleManager.hoveredModule = null;
        GetComponent<SpriteRenderer>().color = Color.white;
        MouseCursorManager.mouseCursorManager.SetCursor(MouseCursor.defaultCursor);
    }

    public virtual void UseModule()
    {

    }

    public virtual void OnCreation()
    {

    }

    public bool IsModuleLimited()
    {
        return limitedChoice;
    }
}
