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
    [SerializeField] private int SleepCap = 36;//18 heures ->36
    public List<CharacterAlertType> characterAlertTypes = new List<CharacterAlertType>();
    [SerializeField] private bool addOnStart = true;


    public static readonly int hunger = 8; //4heure, 6 fois par jour
    // hunger * temps q'un tour represente = nombre d'heure avant de decrementer la satiete
    int incrementHunger = 0;


    private int currHp;
    private int currSleep;
    private int currFood;

    private bool dispo = true;
    public bool Dispo { get => dispo; set => dispo = value; }

    //UI stuff
    [SerializeField] private GameObject ActionDropDown;
    [SerializeField] private GameObject pannelQuestion;

    private void Awake()
    {       

    }
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
        if (addOnStart)
        {
            gameM.AddPerso(this);
            addOnStart = false;
        }

    }

    private void OnMouseEnter()
    {
        GameManager.selection = this;
        Debug.Log(GameManager.selection);
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
        if (currSleep == 0)
        {
            Debug.Log("Message de confirmation : le perso est fatigué");
            //GoToBed();
        }
        #endregion

        #region eating
        if (!eating)
        {
            incrementHunger++;
            if (incrementHunger % hunger == 0)
            {
                incrementHunger = 0;
                if (currFood >= 1)
                {
                    currFood--;
                }
                else
                {
                    currHp--;
                    //message dans le ui
                }

                if (currFood == 0)
                {
                    //message "perso x a besoin de manger" + bouton pour manger
                    Debug.Log("Message de confirmation : le perso a faim");
                }
            }
        }
        #endregion
        Debug.Log("food " + currFood + "|" + "sleep" + currSleep);
    }

    #region TurnActions
    #region eating
    private bool eating = false;
    public void GoEat(bool fromShip)
    {
        if (dispo)
        {
            if (ship.ShipInv().GetAmount(1) > 0)
            {
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
        if (!fromShip)
        {
            CancelAction();
            GoEat(false);
        }

    }

    private void FinishEat()
    {
        if (eating)
        {
            eating = false;
            Dispo = true;
            currFood += foodValue;
            Debug.Log("Done eat");
        }
    }

    #endregion

    #region sleeping
    private bool sleeping = false;

    public void GoToBed(bool overrideAction)
    {
        if (overrideAction)
        {
            CancelAction();
        }
        if (dispo)
        {
            sleeping = true;
            Dispo = false;
            Debug.Log("slep");
        }        
            
        
        
    }

    private void Sleeping()
    {
        if (sleeping)
        {
            Debug.Log("is comfy");
            currSleep += 3;

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
    private bool healing = false;
    public void StartHealing(bool fromShip)
    {

        //CancelAction();//ligne temporaire
        Debug.Log("Start Healing");
        if (dispo)
        {
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
        }
        else if (!fromShip)
        {
            CancelAction();
        }
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
            gameM.RemovePerso(this);
            Destroy(this.gameObject);

        }
    }

    public void RestoreSleep()
    {
        CancelAction();
        this.currSleep = SleepCap;
        ship.ShipInv().PayFromID(3, 1);
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




    public void CreateDynamicOptions()
    {
        gameM.NewActionPanel();

        if (currFood < FoodCap && !eating)
        {
            gameM.AddBouton("Manger", () => GoEat(true));
            Debug.Log("food");
        }
        if (currHp < HPMax && !healing)
        {
            Debug.Log("nned hel");
            gameM.AddBouton("soigner", () => StartHealing(true));
        }
        if (currSleep < SleepCap && !sleeping)
        {
            Debug.Log("Need Sleep");
            gameM.AddBouton("Dormir", () => GoToBed(true));

            if (ship.ShipInv().GetAmount(3) > 0)
            {
                gameM.AddBouton("Prendre une Pillule", RestoreSleep);

            }
        }
    }
    #endregion UI
    
}
