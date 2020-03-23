using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public  class DungeonTimeCounter : MonoBehaviour
{
    public static DungeonTimeCounter dungeonTimeCounter;
    int turnsElapsed = 0;
    [SerializeField] TextMeshProUGUI dungeonTimeCounterText;

    private void Awake()
    {
        if(dungeonTimeCounter == null)
        {
            dungeonTimeCounter = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dungeonTimeCounterText.text = "Time spent: " + turnsElapsed + " <sprite=0>";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseTurnsElapsed(int numberOfTurnsElapsed)
    {
        turnsElapsed += numberOfTurnsElapsed;
        dungeonTimeCounterText.text = "Time spent: " + turnsElapsed + " <sprite=0>";
    }
}
