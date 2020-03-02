using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleOptions : MonoBehaviour
{
    ShipManager ship;

    GameManager gameM;
    
    public enum TypeOfInteractible { Power, Air }
    public TypeOfInteractible type;

    private void Start()
    {
        ship = ShipManager.shipM;
        gameM = GameManager.GM;
    }


    public void UseBatterie()
    {      
        ship.ShipInv().PayFromID(2, 1);            
        ship.AddPower();        
    }

    public void UseBonbonne()
    {
        ship.ShipInv().PayFromID(5,1);
        ship.AddO2();
    }

    private void OnMouseOver()
    {                           
        if (Input.GetButtonDown("Fire2"))           
        {                
            gameM.NewActionPanel();
            GenerateQuickMenu();
        }
        //tooltip?
    }

    private void GenerateQuickMenu()
    {
        switch (type)
        {
            case TypeOfInteractible.Air:
                {
                    if (ship.ShipInv().GetAmount(5) > 0) 
                    { 
                        gameM.NewActionPanel();
                    gameM.AddBouton("Ajouter une bombonne", UseBonbonne);
                    }
                }
                break;

            case TypeOfInteractible.Power:
                if (ship.ShipInv().GetAmount(2) > 0) { 
                    gameM.NewActionPanel();
                    gameM.AddBouton("Ajouter une batterie(+" + ship.BatteriePwr + ")", UseBatterie);
                }
                break;

        }
    }
}
