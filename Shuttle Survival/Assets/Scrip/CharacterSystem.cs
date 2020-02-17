using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem : MonoBehaviour
{
    //connecté à ShipManager
    ShipManager ship;

    //doit etre sur tous les personnages
    [SerializeField] private int HPMax = 6;   
    [SerializeField] private int FoodCap = 12;//12 = 2 jours
    [SerializeField] private int foodValue = 4;
    [SerializeField]private int SleepCap = 36;//18 heures ->36
   

    readonly int hunger = 8; //4heure, 6 fois par jour
    // hunger * temps q'un tour represente = nombre d'heure avant de decrementer la satiete
    int incrementHunger = 0;
    

    private int currHp;
    private int currSleep;
    private int currFood;

    private bool dispo = true;

    
    
    // Start is called before the first frame update
    void Start()
    {
        ship = ShipManager.instance;
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
        currFood = FoodCap;
        currSleep = SleepCap;
        currHp = HPMax;
        currFood = FoodCap;
        //Mathf.Clamp(currHp, 0, HPMax);
    }

    private void OnTimeChanged(object sender, EventArgs e)
    {
        
        Sleeping();//si le perso dort
        
        FinishEat();//si le perso mange, il va completer l'action

        #region sleep
        if (!sleeping) 
        { 
            currSleep--; 
        }
        if (currSleep == 0) { GoToBed();}
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
                    //message "perso x a besoin de manger"
                    GoEat();
                }
            }
        }
        #endregion
        Debug.Log("food " +currFood +"|" + "sleep" + currSleep);
    }


    private bool eating=false;
    public void GoEat()
    {
        if (dispo) { 
            if (ship.ShipInv.GetAmount(1)>0) {
                //va mangez et deviens indispo pour 1 tour
                dispo = false;
                eating = true;

            }
            else
            {
                Debug.Log("pas de bouffe");
                //message dans le ui
            }
        }
    }

    private void FinishEat()
    {
        if (eating) { 
            eating = false;
            dispo = true;
            currFood += foodValue;
            Debug.Log("Done eat");
        }
    }

    public void CancelAction()
    {
        dispo = true;
        eating = false;
        sleeping = false;
    }

    private bool sleeping = false;
    private void GoToBed()
    {           
        if (dispo) {             
            //va
            sleeping = true;
            dispo = false;
            Debug.Log("slep");
        }
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
                dispo = true;
            }
        }      
    }

    public void Hurt(int dmg)
    {
        currHp -= dmg;
        if (currHp<=0) {
            Debug.Log("big oof");
            Destroy(this);
        }
    }

    public void StartHealing()
    {

    }
}
