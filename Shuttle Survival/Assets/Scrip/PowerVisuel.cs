using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerVisuel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] powerLeft;//0-full,2-empty
    ShipManager ship;

    //onturnchange check power, update the visuel
    private void Start()
    {
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
        ship = ShipManager.shipM;
        ChangeBar(0);
    }

    private void OnTimeChanged(object sender, EventArgs e)
    {
        ChangeBar(ReturnBarPos());
    }

    private void ChangeBar(int pos)
    {
        for (int i = 0; i < powerLeft.Length; i++)
        {
            if (i != pos)
                powerLeft[i].SetActive(false);
            else
                powerLeft[i].SetActive(true);
        }
    }

    private int ReturnBarPos()
    {
        float x, y;
        x = ship.PowerQte;
        y = ship.PowerCap;
        float pourcent = x/y;
        if (pourcent > 0.66f)
        {
            return 0;
        }else if(pourcent < 0.66f && pourcent > 0.33f)
        {
            return 1;
        }
        else
        {
            return 2;
        }
        
    }
}
