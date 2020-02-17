using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject fog;
    [SerializeField] private GameObject[] modules;
    [SerializeField] private List<GameObject> personnages;
    

    private static GameManager GM;

    private GameManager(){}

    public static GameManager InstanceGM()
    {
        if (GM == null)
        {
            GM = new GameManager();
        }
        return GM;
    }

    public List<GameObject> Personnages { get => personnages;}

    public void AddPerso(GameObject perso)
    {
        personnages.Add(perso);
    }

    void Start()
    {
        fog.SetActive(true);
        for(int i = 0; i< modules.Length; i++) { modules[i].SetActive(false); }
    }

    
}
