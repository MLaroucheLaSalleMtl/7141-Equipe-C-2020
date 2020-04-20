
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region UI
    [SerializeField] private List<CharacterSystem> personnages;
    [SerializeField] private GameObject panelInfoPersos;//paneau d'info du perso selectionner
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button boutonBlueprint;//boutons des menus dynamiques
    [SerializeField] private GameObject menu;//paneau des menus dynamiques
    [SerializeField] private GameObject playerButtonBP;//bouton du top
    [SerializeField] private GameObject panelTop;//panel pour les boutons perso
    //public GameObject persoInfoPanel;
    #endregion UI
    [SerializeField] private GameObject fog;
    [SerializeField] private GameObject[] modules;
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelSkills;
    [SerializeField] private GameObject panelVictory;
    //
    public static GameObject actions;
    public static CharacterSystem selection;
    public static GameManager GM;
    public static bool surSelectable;
    private bool panelCheck;
    private int motorCount;
    private void Awake()
    {
        if(GM == null)
        {
            GM = this;
        }
        else { Destroy(this); }
    }

    public event EventHandler OnSkillOpen;
    public void OnSkillOpen_Caller()
    {
        if (OnSkillOpen != null)
        {
            OnSkillOpen(this, EventArgs.Empty);
        }
    }

    #region accessors
    public List<CharacterSystem> Personnages { get => personnages;}
    public GameObject PlayerButtonBP { get => playerButtonBP;}
    public GameObject PanelTop { get => panelTop;}
    #endregion

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Invoke("CloseActionPanel", 0.1f);
           
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)){
            
                panelPause.SetActive(!panelPause.activeSelf);           
             
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Deselect();
        }
        if (Input.GetKeyDown(KeyCode.C) && selection)
        {                      
            panelSkills.SetActive(!panelSkills.activeSelf);
            Invoke("OnSkillOpen_Caller",0.1f);
        }
        
    }
    
    

    public void Select(CharacterSystem select)
    {
        Deselect();
        selection = select;
        selection.gameObject.GetComponent<OutlineEffect>().OnSelection();//temporaire en attendant qu'on se parle du code
        GetInfo();
    }

    public void GetInfo()
    {
        panelInfoPersos.SetActive(true);
        panelInfoPersos.GetComponent<InfoPerso>().FullInfoUpdate();
    }

    public void Deselect()
    {
        if(selection)
            selection.gameObject.GetComponent<OutlineEffect>().OnDeselection();//temporaire en attendant qu'on se parle du code

        selection = null;
        panelInfoPersos.SetActive(false);
    }

    public void NewActionPanel()
    {
        if (actions) 
            Destroy(actions);

        actions = Instantiate(menu);
        actions.transform.SetParent(canvas.transform, true);
        actions.transform.position = Input.mousePosition; 
    }
    
    public void CloseActionPanel()
    {
        if (actions)
            Destroy(actions);
    }

    public void AddPerso(CharacterSystem perso)
    {
        personnages.Add(perso);
        perso.CreateTop();
    }

    public void RemovePerso(CharacterSystem that)
    {
        personnages.Remove(that);
    }

    void Start()
    {
        fog.SetActive(true);
        foreach (GameObject mod in modules) { mod.SetActive(false); }
        //CharacterSystem[] temp = GameObject.FindObjectsOfType<CharacterSystem>();
        //foreach (CharacterSystem perso in temp) { personnages.Add(perso); }//(int i = 0; i<temp.Length; i++) { personnages.Add(temp[i]); }
        Invoke("ClosePauseMenu", 0.01f);
        panelVictory.SetActive(false);
               
    }
 
    void ClosePauseMenu()
    {
        panelPause.SetActive(false);
    }

    public void AddBouton(string txt, System.Action action)
    {
        Button boutonTravail;
        boutonTravail = Instantiate(boutonBlueprint, this.gameObject.transform);
        boutonTravail.GetComponentInChildren<Text>().text = txt;//change le text du bouton
        boutonTravail.transform.SetParent(actions.transform, true);//met le bouton enfant du paneau action
        boutonTravail.onClick.AddListener(() =>action());
        boutonTravail.onClick.AddListener(CloseActionPanel);
    }

    public void motorCheck()
    {
        motorCount++;

        panelVictory.SetActive(true);
        
    }
}
