﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    //connecté à l'inventaire du vaisseau et le GameManager
    [SerializeField] private Inventaire shipInv;
    private GameManager gameM;
    public static ShipManager shipM;
    private int persoQte;
    [SerializeField] private int o2Capacity = 96;
    [SerializeField] private int powerCap = 20;
    [SerializeField] private int batteriePwr = 10;
    [SerializeField] private int bonbonneQte = 32;

    private int o2Qte;
    private int powerQte;

    //balance
    [SerializeField] private int o2Usage = 1;

    #region accesseurs
    public int O2Qte { get => o2Qte; }
    public int O2Capacity { get => o2Capacity; }
    public int BatteriePwr { get => batteriePwr; }
    public int PowerQte { get => powerQte; }
    public int PowerCap { get => powerCap; }
    #endregion

    //public Inventaire ShipInv { get => shipInv;}

    public Inventaire ShipInv()
    {
        return shipInv;

    }

    private void Start()
    {
        gameM = GameManager.GM;
        persoQte = gameM.Personnages.Count;
        o2Qte = O2Capacity;
        powerQte = powerCap;
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
    }

    private void OnTimeChanged(object sender, EventArgs e)
    {
        o2Qte -= o2Usage * persoQte;
        //le changement de power va se faire dans les scriptes des modules
    }

    private void Awake()
    {
        if (ShipManager.shipM is null)
        {
            ShipManager.shipM = this;
        }
        else { Destroy(this); }

    }

    //pour ajouter de la cap d'o2 quand on debloque une salle
    public void AddOxygenCapacite(int capacite)
    {
        o2Capacity += capacite;
    }

    public void AddPower()
    {
        powerQte += BatteriePwr;
        powerQte = Mathf.Clamp(powerQte, 0, powerCap);
    }

    public void AddO2()
    {
        o2Qte += bonbonneQte;
        o2Qte = Mathf.Clamp(o2Qte, 0, o2Capacity);
    }

    public void AddAir(int qte) 
    {
        o2Qte += qte;
        o2Qte = Mathf.Clamp(o2Qte, 0, o2Capacity);
    }


}
