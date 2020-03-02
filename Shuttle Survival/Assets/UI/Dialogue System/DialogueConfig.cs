using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueConfig : MonoBehaviour
{
    public DialogueEntry[] dialogueEntries;
    public UnityEvent onDialogueEndEvent = null;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UnityEvent DialogueAction(int dialogueIndex)
    {
        return dialogueEntries[dialogueIndex].dialogueEvent;
    }
}
