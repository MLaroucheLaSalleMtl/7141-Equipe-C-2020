using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTreePanelManager : MonoBehaviour
{
    [SerializeField] private Image charImage;
    //[SerializeField] private GameObject toolPanel;
    [SerializeField] private Image xpBar;
    [SerializeField] private Text xpText;
    [SerializeField] private Text abilityPoint;
   
    private void Start()
    {
        GameManager.GM.OnSkillOpen += Open;
    }

    private void Open(object sender, EventArgs e)
    {
        Debug.Log("hewwo");
        CharacterSkillsManager temp = GameManager.selection.GetComponent<CharacterSkillsManager>();
        float a = 1.0f * (temp.Experience - temp.XpLast());
        Debug.Log(a + "a");
        charImage.sprite = GameManager.selection.GetComponent<SpriteRenderer>().sprite;
        xpText.text = temp.Experience.ToString() + "/" + temp.ExpToNext.ToString();
        float b = a / temp.XpMarginal();
        Debug.Log(b + " b");
        xpBar.fillAmount = b;
        abilityPoint.text = "Aptitude points : " + temp.AbilityPoints.ToString();
    }

    /*public void ToolSelect()
    {
        toolPanel.SetActive(true);
        //paneau avec les différent tools
    }*/
}
