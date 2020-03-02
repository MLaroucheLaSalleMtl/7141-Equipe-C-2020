using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggers : MonoBehaviour
{
    public static DialogueTriggers dialogueTriggers;
    [SerializeField] DialogueConfig[] dialoguesConfigs;
    bool entryDialogueDone = false;
    private void Awake()
    {
        if(dialogueTriggers == null)
        {
            dialogueTriggers = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!entryDialogueDone)
        {
            entryDialogueDone = true;
            TriggerDialogue(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerDialogue(int dialogueIndex)
    {
        DialogueSystem.dialogueSystem.SetDialogueConfig(dialoguesConfigs[dialogueIndex]);
    }
}
