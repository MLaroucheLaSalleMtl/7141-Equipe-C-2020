using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public  class DungeonTimeCounter : MonoBehaviour
{
    public static DungeonTimeCounter dungeonTimeCounter;
    int elapsedTimeInDungeon = 0;
    [SerializeField] TextMeshProUGUI dungeonTimeCounterText;
    [SerializeField] TextMeshProUGUI leaveDungeonPanelElapsedTimeText;

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
        dungeonTimeCounterText.text = "Time spent: " + elapsedTimeInDungeon + " <sprite=0>";
        leaveDungeonPanelElapsedTimeText.text = "Elapsed time : " + elapsedTimeInDungeon+"   <sprite=0>";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseTurnsElapsed(int numberOfTurnsElapsed)
    {
        elapsedTimeInDungeon += numberOfTurnsElapsed;
        RefreshTimeCountersText();

    }

    private void RefreshTimeCountersText()
    {
        dungeonTimeCounterText.text = "Time spent: " + elapsedTimeInDungeon + " <sprite=0>";
        leaveDungeonPanelElapsedTimeText.text = "Elapsed time : " + elapsedTimeInDungeon + "   <sprite=0>";
    }

    public void SendElapsedTurnsToTimeManager()
    {
        TimeManager.timeManager.ReceiveElapsedTimeInDungeon(elapsedTimeInDungeon);
    }
}
