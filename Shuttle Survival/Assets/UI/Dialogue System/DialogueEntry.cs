using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueEntry
{
    [SerializeField] public DialogueSpeaker dialogueSpeaker;
    [TextArea(2, 4)]
    [SerializeField] public string dialogueMessage;
    [SerializeField] public UnityEvent dialogueEvent;
}
