using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModuleManager : MonoBehaviour
{
    public static ModuleManager moduleManager;
    public Color32 hoverColor = Color.black;
    [HideInInspector]
    public Module currentModule;
    [HideInInspector]
    public Module hoveredModule;
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
    [SerializeField] GameObject underConstructionPanel;
    [SerializeField] Image underConstructionProgressBar;
    [SerializeField] TextMeshProUGUI turnsRemainingText;
    [SerializeField] TextMeshProUGUI buildingModuleText;
    List<GameObject> currentLimitedModulesChoices;
    bool buildingFromLimitedChoices = false;
    [Header("All modules")]
    public List<GameObject> standardModulePrefabs = new List<GameObject>();
    public GameObject emptyModule;
    [SerializeField] GameObject underConstructionModulePrefab;
    [SerializeField] Inventaire inventaire;

    
    shipNPCmanager NPC;

    public void OnPanelOpened(object source, EventArgs args)
    {
        modulePanel.SetActive(false);
        moduleCreationPanel.SetActive(false);
        underConstructionPanel.SetActive(false);
        if(currentModule)
            currentModule.GetComponent<TooltipHandler>().OnPointerExit(null);
    }

   
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
        PanelManager.panelManager.OnPanelOpened += ModuleManager.moduleManager.OnPanelOpened;
        NPC = shipNPCmanager.NPCmanagInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if(hoveredModule != null && PanelManager.panelManager.IsInteractablesEnabled() && Input.GetMouseButtonDown(0))
        {
            if (hoveredModule.moduleType != ModuleType.UnderConstruction)
            {
                GenerateModulePanel(hoveredModule);
            }
            else
            {
                GenerateUnderConstructionPanel((UnderConstructionModule)hoveredModule);
            }
        }
        
    }


    public void GenerateModuleCreationPanel()
    {
        buildingFromLimitedChoices = false;
        PanelManager.panelManager.OnPanelOpened_Caller();
        moduleCreationPanel.SetActive(true);
        foreach (Transform transform in moduleCreationGrid)
        {
            Destroy(transform.gameObject);
        }

        if (currentModule.IsModuleLimited())
        {
            buildingFromLimitedChoices = true;
            currentLimitedModulesChoices = currentModule.limitedModulePrefabsChoices;
            for (int i = 0; i < currentLimitedModulesChoices.Count; i++)
            {
                Image uiModule = Instantiate(moduleCreationPrefab, moduleCreationGrid);
                currentUImodule = uiModule.GetComponent<UIModule>();
                uiModule.sprite = currentLimitedModulesChoices[i].GetComponent<Module>().sprite;
                uiModule.GetComponent<UIModule>().moduleIndex = i;
                uiModule.GetComponent<UIModule>().moduleDescription = currentLimitedModulesChoices[i].GetComponent<Module>().moduleDescription;
            }
        }
        else
        {
            for (int i = 0; i < standardModulePrefabs.Count; i++)
            {
                Image uiModule = Instantiate(moduleCreationPrefab, moduleCreationGrid);
                currentUImodule = uiModule.GetComponent<UIModule>();
                uiModule.sprite = standardModulePrefabs[i].GetComponent<Module>().sprite;
                uiModule.GetComponent<UIModule>().moduleIndex = i;
                uiModule.GetComponent<UIModule>().moduleDescription = standardModulePrefabs[i].GetComponent<Module>().moduleDescription;
            }
        }        
    }

    public void CreateModule(int moduleIndex)
    {
        List<GameObject> listOfPossibleModules = buildingFromLimitedChoices ? currentLimitedModulesChoices : standardModulePrefabs;

        if (inventaire.PayRessource(listOfPossibleModules[moduleIndex].GetComponent<Module>().ressourcesCost)) //if it can pay
        {
            //vérifie si un personnage est en mode listen pour aller construire le module
             if (NPC.IsNPCavailable() == true)
             {
                 Debug.Log("Sending bob");
                
                GameObject underConstructionModule = Instantiate(underConstructionModulePrefab, currentModule.transform.position, Quaternion.identity);
                underConstructionModule.GetComponent<UnderConstructionModule>().SetTurnsToBuild(listOfPossibleModules[moduleIndex].GetComponent<Module>().turnsToBuild);
                underConstructionModule.GetComponent<UnderConstructionModule>().moduleToBuild = listOfPossibleModules[moduleIndex];
                Destroy(currentModule.gameObject);
                    if (currentModule.unique && !buildingFromLimitedChoices)
                    {
                        standardModulePrefabs.RemoveAt(moduleIndex);
                    }
                moduleCreationPanel.SetActive(false);

                //trouver le bon endroit pour envoyer bob      
                NPC.NeedAHandOverHere(underConstructionModule.transform);
            }
            else
            {
                MessagePopup.MessagePopupManager.SetStringAndShowPopup("Select someone to go upgrade");
                //plus tard on pourrait peut-être mettre ici le drop down list avec toutes les persos
            }
            
        }
        else
        {
            MessagePopup.MessagePopupManager.SetStringAndShowPopup("Not enough ressources");
        }

        currentUImodule.GetComponent<TooltipHandler>().OnPointerExit(null);
    }

    private void GenerateUnderConstructionPanel(UnderConstructionModule currentModule)
    {  
        PanelManager.panelManager.OnPanelOpened_Caller();
        this.currentModule = currentModule;
        underConstructionPanel.SetActive(true);
        buildingModuleText.text = "Building <b>" + currentModule.moduleToBuild.GetComponent<Module>().moduleName + "</b>";
        underConstructionProgressBar.fillAmount = Mathf.Abs(1 - ((float)currentModule.turnsRemainingToBuild) / currentModule.totalTurnsToBuild);
        turnsRemainingText.text = "<b>" + currentModule.turnsRemainingToBuild + " <sprite=0>remaining";
    }

    public void GenerateModulePanel(Module currentModule)
    {
        PanelManager.panelManager.OnPanelOpened_Caller();
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

    public void SalvageModule(bool underConstruction = false) //and cancel construction
    {
        if (underConstruction)
        {
            inventaire.AddManyResources(((UnderConstructionModule)currentModule).moduleToBuild.GetComponent<Module>().ressourcesCost); //salvage et cancel construction refund 100% pour linstant
            ((UnderConstructionModule)currentModule).CancelCreation();
        }
        else
        {
            inventaire.AddManyResources(currentModule.ressourcesCost); //salvage et cancel construction refund 100% pour linstant
        }
        currentModule.GetComponent<TooltipHandler>().OnPointerExit(null);
        Destroy(currentModule.gameObject);
        Instantiate(emptyModule, currentModule.transform.position, Quaternion.identity);
        modulePanel.SetActive(false);
        underConstructionPanel.SetActive(false);
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
