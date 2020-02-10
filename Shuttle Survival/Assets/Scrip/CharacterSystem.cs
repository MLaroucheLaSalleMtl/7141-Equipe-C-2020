using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem : MonoBehaviour
{
    [SerializeField] public Inventaire shipInventory;
    [SerializeField] private int HPMax = 0;
    private int SleepCap = 16;
    [SerializeField] private int FoodCap;

    private int SatieteMax = 4;
    int satiete = 4;
    int hunger = 8; 
    // hunger * temps q'un tour represente = nombre d'heure avant de decrementer la satiete
    int incrementHunger = 0;
    ShipManager ship;

    private int currHp;
    private int currSleep;
    private int currFood;

    
    // Start is called before the first frame update
    void Start()
    {
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
        currFood = FoodCap;
        currSleep = SleepCap;
        currHp = HPMax;
        ship = ShipManager.instance;
        
    }

    private void OnTimeChanged(object sender, EventArgs e)
    {
        currSleep --;
        incrementHunger++;
        if(incrementHunger%hunger == 0)
        {
            incrementHunger = 0;

        }
    }

 
}
