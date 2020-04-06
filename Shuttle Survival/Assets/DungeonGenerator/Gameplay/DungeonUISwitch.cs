using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonUISwitch : MonoBehaviour
{
    public static DungeonUISwitch dungeonUISwitch;
    [SerializeField] GameObject dungeonUIHolder;


    private void Awake()
    {
        if(dungeonUISwitch == null)
        {
            dungeonUISwitch = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CloseDungeonUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseDungeonUI()
    {
        dungeonUIHolder.SetActive(false);
    }

    public void OpenDungeonUI()
    {
        dungeonUIHolder.SetActive(true);
    }
}
