using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleManager : MonoBehaviour
{
    public static ModuleManager moduleManager;
    public Color32 hoverColor = Color.black;
    public Module currentModule;
    public GameObject modulePanel;
    public GameObject moduleCreationPanel;
    UIModule currentUImodule;
    [SerializeField] Image moduleCreationPrefab;
    [SerializeField] Transform moduleCreationGrid;
    [SerializeField] Text modulePanelTitle;
    [SerializeField] Transform moduleOptionsGrid;
    [SerializeField] GameObject createModuleButton;
    [SerializeField] GameObject salvageModuleButton;
    [SerializeField] GameObject useModuleButton;
    [SerializeField] GameObject upgradeModuleButton;

    [Header("All modules")]
    public List<GameObject> modulePrefabs = new List<GameObject>();
    public GameObject emptyModule;
    public GameObject healthModule;
    private void Awake()
    {
        if(ModuleManager.moduleManager == null)
        {
            ModuleManager.moduleManager = this;
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
    
    public void GenerateModuleCreationPanel()
    {
        modulePanel.SetActive(false);
        moduleCreationPanel.SetActive(true);
        foreach (Transform transform in moduleCreationGrid)
        {
            Destroy(transform.gameObject);
        }
        for (int i = 0; i < modulePrefabs.Count; i++)
        {
            Image uiModule = Instantiate(moduleCreationPrefab, moduleCreationGrid);
            currentUImodule = uiModule.GetComponent<UIModule>();
            uiModule.sprite = modulePrefabs[i].GetComponent<Module>().sprite;
            uiModule.GetComponent<UIModule>().moduleIndex = i;
            uiModule.GetComponent<UIModule>().moduleDescription = modulePrefabs[i].GetComponent<Module>().moduleDescription;
        }
    }

    public void CreateModule(int moduleIndex)
    {
        Instantiate(modulePrefabs[moduleIndex], currentModule.transform.position, Quaternion.identity);
        Destroy(currentModule.gameObject);
        if (currentModule.unique)
        {
            modulePrefabs.RemoveAt(moduleIndex);
        }
        currentUImodule.GetComponent<TooltipHandler>().OnPointerExit(null);
        moduleCreationPanel.SetActive(false);
    }

    public void GenerateModulePanel(Module currentModule)
    {
        foreach (Transform transform in moduleOptionsGrid)
        {
            Destroy(transform.gameObject);
        }
        this.currentModule = currentModule;
        modulePanel.SetActive(true);
        modulePanelTitle.text = currentModule.moduleName;
        if(currentModule.moduleType == ModuleType.Empty)
        {
            GameObject newGO = Instantiate(createModuleButton, moduleOptionsGrid);
            newGO.GetComponent<Button>().onClick.AddListener(() => GenerateModuleCreationPanel());
        }
        if (currentModule.useable)
        {
            GameObject newGO = Instantiate(useModuleButton, moduleOptionsGrid);
            newGO.GetComponent<Button>().onClick.AddListener(() => UseModule());
        }
        if (currentModule.salvageable)
        {
            GameObject newGO = Instantiate(salvageModuleButton, moduleOptionsGrid);
            newGO.GetComponent<Button>().onClick.AddListener(() => SalvageModule());
        }
        if (currentModule.upgradable)
        {
            GameObject newGO = Instantiate(upgradeModuleButton, moduleOptionsGrid);
            newGO.GetComponent<Button>().onClick.AddListener(() => UpgradeModuleButton());
        }
    }

    public void SalvageModule()
    {
        currentModule.GetComponent<TooltipHandler>().OnPointerExit(null);
        Destroy(currentModule.gameObject);
        modulePanel.SetActive(false);
    }

    public void UseModule()
    {
        currentModule.UseModule();
    }

    public void UpgradeModuleButton()
    {
        Debug.Log("Upgrade pas implementer...");
    }
}
