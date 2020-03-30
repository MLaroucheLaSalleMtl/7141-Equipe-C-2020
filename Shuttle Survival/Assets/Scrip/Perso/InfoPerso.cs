using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPerso : MonoBehaviour
{
    [SerializeField] private Text hpTxt, foodTxt, SleepTxt, nameTxt, lvlTxt;
    [SerializeField] private GameObject meals;
    [SerializeField] private Image hpBar,sleepBar;
    
    private void MealsAJour()
    {
        int cap, current;
        cap = GameManager.selection.FoodCap;
        current = GameManager.selection.CurrFood;
        int x = cap - current;
        for(int i = meals.transform.childCount - 1; i >= x; i--) 
        {
            meals.transform.GetChild(i).gameObject.SetActive(true);
        }
        for (int i = 0; i < x; i++)
        {
            meals.transform.GetChild(i).gameObject.SetActive(false);
        }
        foodTxt.text = current.ToString() + "/" + cap.ToString();
    }
    private void HpAJour()
    {
        float cap, current;
        cap = GameManager.selection.HPMax;
        current = GameManager.selection.CurrHp;
        float x = current / cap;
        Debug.Log(x);
        hpBar.fillAmount = x;
        hpTxt.text = current.ToString("0") + "/" + cap.ToString("0");
    }

    private void SleepUpdate()
    {
        int cap, current;
        cap = GameManager.selection.SleepCap;
        current = GameManager.selection.CurrSleep;
        float x = current / cap;
        sleepBar.fillAmount = x;
        SleepTxt.text = current.ToString() + "/" +cap.ToString() ;
    }

    public void InfoUpdate()
    {
        nameTxt.text = GameManager.selection.name;
        lvlTxt.text = "lvl.1" + "";
        MealsAJour();
        HpAJour();
        SleepUpdate();
    }
}
