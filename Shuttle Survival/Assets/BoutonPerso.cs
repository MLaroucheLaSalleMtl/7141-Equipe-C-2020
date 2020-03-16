using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutonPerso : MonoBehaviour
{
    private Sprite img;
    private Button bouton;
    private CharacterSystem perso;

    public CharacterSystem Perso { get => perso; set => perso = value; }

    // Start is called before the first frame update
    void Start()
    {
        bouton = this.GetComponent<Button>();
    }

    // Update is called once per frame
    
}
