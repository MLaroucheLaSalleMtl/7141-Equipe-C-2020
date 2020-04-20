using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPerso : MonoBehaviour
{
    [SerializeField] private Text hpTxt, foodTxt, SleepTxt, nameTxt, lvlTxt;
    [SerializeField] private Image hpBar,sleepBar, characterImage, mealsBar;
    private CharacterSystem work;
    private void Start()
    {
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
    }

    private void OnTimeChanged(object sender, EventArgs e)
    {
        InfoUpdate();
    }

    private void MealsAJour()
    {
        float cap, current;
        cap = work.FoodCap;
        current = work.CurrFood;
        float x = current / cap;
        mealsBar.fillAmount = x;
        foodTxt.text = current.ToString("0") + "/" + cap.ToString("0");
        
    }
    private void HpAJour()
    {
        float cap, current;
        cap = work.HPMax;
        current = work.CurrHp;
        float x = current / cap;

        hpBar.fillAmount = x;
        hpTxt.text = current.ToString("0") + "/" + cap.ToString("0");
    }

    private void SleepUpdate()
    {
        float cap, current;
        cap = work.SleepCap;
        current = work.CurrSleep;
        float x = current / cap;
        sleepBar.fillAmount = x;
        SleepTxt.text = current.ToString("0") + "/" +cap.ToString("0") ;
    }

    public void InfoUpdate()
    {   
        nameTxt.text = work.CharacterName;
        lvlTxt.text = "lvl." + work.GetComponent<CharacterSkillsManager>().level;
        characterImage.sprite = work.CharacterSprite;
        MealsAJour();
        HpAJour();
        SleepUpdate();
        
    }

    public void FullInfoUpdate()
    {
        if (GameManager.selection)
        {
            work = GameManager.selection;
            InfoUpdate();
        }
    }
}
