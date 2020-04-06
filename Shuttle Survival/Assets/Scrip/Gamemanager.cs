
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
    //
    public static GameObject actions;
    public static CharacterSystem selection;
    public static GameManager GM;
    public static bool surSelectable;
    private void Awake()
    {
        if(GM == null)
        {
            GM = this;
        }
        else { Destroy(this); }
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
           if (!surSelectable)
               Invoke("Deselect", 0.1f);
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
        panelInfoPersos.GetComponent<InfoPerso>().InfoUpdate();
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
}
