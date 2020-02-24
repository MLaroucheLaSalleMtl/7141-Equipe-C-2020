using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class CharacterSystem : MonoBehaviour
{
    //connecté à ShipManager
    ShipManager ship;
    
    GameManager gameM;

    //doit etre sur tous les personnages
    [SerializeField] private int HPMax = 6;   
    [SerializeField] private int FoodCap = 12;//12 = 2 jours
    [SerializeField] private int foodValue = 4;
    [SerializeField]private int SleepCap = 36;//18 heures ->36
    public List<CharacterAlertType> characterAlertTypes = new List<CharacterAlertType>();
   
    
    readonly int hunger = 8; //4heure, 6 fois par jour
    // hunger * temps q'un tour represente = nombre d'heure avant de decrementer la satiete
    int incrementHunger = 0;
    

    private int currHp;
    private int currSleep;
    private int currFood;

    private bool dispo = true;
    //UI stuff
    [SerializeField] private GameObject ActionDropDown;
    [SerializeField] private GameObject pannelQuestion;
    
    // Start is called before the first frame update
    void Start()
    {
        ship = ShipManager.shipM;
        gameM = GameManager.GM;
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
        currFood = FoodCap;
        currSleep = SleepCap;
        currHp = HPMax;
        currFood = FoodCap;
        Hurt(1);// test
        gameM.AddPerso(this);

    }


    private void OnTimeChanged(object sender, EventArgs e)
    {
        
        Sleeping();//si le perso dort
        
        FinishEat();//si le perso mange, il va completer l'action

        FinishHealing();//si le perso se soigne

        #region sleep
        if (!sleeping) 
        { 
            currSleep--; 
        }
        if (currSleep == 0) { 
            Debug.Log("Message de confirmation : le perso est fatigué");
            //GoToBed();
        }
        #endregion

        #region eating
        if (!eating) { 
            incrementHunger++;
            if(incrementHunger%hunger == 0)
            {
                incrementHunger = 0;
                if (currFood >= 1) { 
                    currFood --;
                }
                else
                {
                    currHp--;
                    //message dans le ui
                }

                if (currFood==0) {
                    //message "perso x a besoin de manger" + bouton pour manger
                    Debug.Log("Message de confirmation : le perso a faim");
                }
            }
        }
        #endregion
        Debug.Log("food " +currFood +"|" + "sleep" + currSleep);
    }

    #region TurnActions
    #region eating
    private bool eating=false;
    public void GoEat()
    {
        CancelAction(); 
        if (ship.ShipInv().GetAmount(1)>0) {
            //va mangez
            Dispo = false;               
            eating = true;
        }
        else
        {
            Debug.Log("pas de bouffe");    
            //message dans le ui    
        }
        

    }

    private void FinishEat()
    {
        if (eating) { 
            eating = false;
            Dispo = true;
            currFood += foodValue;
            Debug.Log("Done eat");
        }
    }

#endregion

    #region sleeping
    private bool sleeping = false;
    private void GoToBed()
    {
        CancelAction();       
        sleeping = true;
        Dispo = false;
        Debug.Log("slep");
    }

    private void Sleeping() {
        if (sleeping)
        {
            Debug.Log("is comfy");
            currSleep+= 3;
            
            if (currSleep >= SleepCap)
            {
                currSleep = SleepCap;
                sleeping = false;
                Dispo = true;
            }
        }      
    }
    #endregion

    #region healing
    private bool healing=false;
    private void StartHealing()
    {
        
        CancelAction();//ligne temporaire
        Debug.Log("Start Healing");
        //if (yes)
        //{
            if (ship.ShipInv().GetAmount(4) >= 1)
            {

                healing = true;
                Dispo = false;
                Debug.Log("healing");
            }
            else
            {
                Debug.Log("pas de medkits");
                //message dans le hud
            }
        //}
    }

    public void FinishHealing()
    {
        if (healing)
        {
            healing = false;
            ship.ShipInv().PayFromID(4, 1);
            Dispo = true;
            this.currHp = this.HPMax;
        }
    }
    #endregion
#endregion
    public void Hurt(int dmg)
    {
        currHp -= dmg;
        if (currHp <= 0)
        {
            Debug.Log("big oof");
            Destroy(this);
        }
    }

    public void RestoreSleep()
    {
        CancelAction();
        this.currSleep = SleepCap;

    }
    public void CancelAction()
    {
        Dispo = true;
        eating = false;
        sleeping = false;
        healing = false;
    }

    #region UI
    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire2"))//click de droit, avec le input system)
        {
            Debug.Log("hey");
            CreateDynamicOptions();
        }
    }

    [SerializeField] private Button boutonBlueprint;

    public bool Dispo { get => dispo; set => dispo = value; }

    public void CreateDynamicOptions()
    {
        gameM.NewActionPanel(ActionDropDown);      
        
        if (currFood < FoodCap && !eating)
        {           
            AddBouton("Manger", 1);
            Debug.Log("food");                                  
        }
        if (currHp < HPMax && !healing)
        {
            Debug.Log("hel");
            AddBouton("soigner", 2);
        }
        if (currSleep < SleepCap && !sleeping)
        {
            Debug.Log("Need Sleep");
            AddBouton("Dormir", 3);

            if (ship.ShipInv().GetAmount(3)>0)
            {
                AddBouton("Prendre une Pillule", 4);

            }   
        }
    }

    private void AddBouton(string txt, int cas)
    {
        Button boutonTravail;
        boutonTravail = Instantiate(boutonBlueprint, this.gameObject.transform);
        boutonTravail.GetComponentInChildren<Text>().text = txt;
        boutonTravail.transform.SetParent(gameM.actions.transform, true);
        switch (cas)
        {
            case 1: boutonTravail.onClick.AddListener(GoEat);                  
                break;
                
            case 2: boutonTravail.onClick.AddListener(StartHealing);
                break;

            case 3: boutonTravail.onClick.AddListener(GoToBed);                
                break;

            case 4: boutonTravail.onClick.AddListener(RestoreSleep);              
                break;

                
        }
        boutonTravail.onClick.AddListener(gameM.CloseActionPanel);
    }

    public void Confirmation()
    {
        CancelAction();
        pannelQuestion.SetActive(false);
    }

    public void Negation()
    {
        pannelQuestion.SetActive(false);
    }

    public void Question(string txt)
    {
        if (!Dispo)
        {
            pannelQuestion.SetActive(true);
            pannelQuestion.GetComponent<Text>().text = txt;
        }
    }
    #endregion UI
}
