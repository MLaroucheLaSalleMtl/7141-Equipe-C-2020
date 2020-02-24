using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiblePanelActions : MonoBehaviour
{
    ShipManager ship;
    GameManager game;
    PanelManager panelM;
    GameManager gameM;
    [TextArea(1,3)]
    [SerializeField] private string text;
    private void Start()
    {
        ship = ShipManager.shipM;
        game = GameManager.GM;
        panelM = PanelManager.panelManager;
        gameM = GameManager.GM;
    }

    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire2"))
        {

        }
    }
    public void UseBatterie()
    {
        if (ship.ShipInv().GetAmount(2) > 0)
        {
            ship.ShipInv().PayFromID(2, 1);
            ship.AddPower();
        }
    }

}
