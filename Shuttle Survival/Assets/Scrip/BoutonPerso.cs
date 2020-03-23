using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoutonPerso : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{

    private Button bouton;
    private GameManager gameM;

  
    // Start is called before the first frame update
    void Start()
    {
        gameM = GameManager.GM;            
    }

    public void MisAJour(CharacterSystem perso)
    {
        GameObject temp;
        bouton = this.GetComponent<Button>();
        temp = this.transform.GetChild(0).gameObject;
        temp.GetComponent<Image>().sprite = perso.CharacterSprite;
        //this.gameObject.GetComponentInChildren<Image>().sprite = perso.CharacterSprite;//pourquoi ça change le sprite du bouton pis pas de l'enfant? 
        bouton.onClick.AddListener(() => {       gameM.Select(perso);    });//cette ligne OK
    }
    // Update is called once per frame
    private void OnMouseEnter()
    {       
        Debug.Log("bouton");
        CharacterSystem.surPerso = true;
        Debug.Log(CharacterSystem.surPerso);
    }

    private void OnMouseExit()
    {
        CharacterSystem.surPerso = false;        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CharacterSystem.surPerso = true;
        Debug.Log(CharacterSystem.surPerso);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CharacterSystem.surPerso = false;
        Debug.Log(CharacterSystem.surPerso);
    }
}
