using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance = null;
    private int persoQte = 0;
    [SerializeField] private int o2Capacity = 10;
    [SerializeField] private int powerCap = 10;
    private int o2Qte;
    private int powerQte;

    //balance
    [SerializeField] private int o2Usage = 1;

    private void Start()
    {
        o2Qte = o2Capacity;
        powerQte = powerCap;
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
    }

    private void OnTimeChanged(object sender, EventArgs e)
    {
        o2Qte -= o2Usage * persoQte;
        
    }

    private void Awake()
    {
        if(ShipManager.instance = null)
        {
            ShipManager.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //pour ajouter de la cap d'o2 quand on debloque une salle
    public void AddOxygenCapacite(int capacite)
    {
        o2Capacity += capacite;
    }


}
