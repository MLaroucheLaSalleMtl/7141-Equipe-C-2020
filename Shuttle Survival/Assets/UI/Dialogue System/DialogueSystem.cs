using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem dialogueSystem;
    DialogueConfig currentDialogueConfig;
    Queue<DialogueEntry> dialogueEntries = new Queue<DialogueEntry>();
    bool dialogueInitiated = false;
    bool canLoadNextDialogueEntry = false;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueTextField;
    [SerializeField] Image speakerImageBox;
    [SerializeField] TextMeshProUGUI speakerNameTextField;

    private void Awake()
    {
        if(DialogueSystem.dialogueSystem == null)
        {
            DialogueSystem.dialogueSystem = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (canLoadNextDialogueEntry && Input.GetKeyDown(KeyCode.Space))
        {
            DequeueDialogueEntryAndDisplay();
        }
    }

    public void SetDialogueConfig(DialogueConfig dialogueConfig)
    {
        currentDialogueConfig = dialogueConfig;
        foreach (DialogueEntry dialogueEntry in currentDialogueConfig.dialogueEntries)
        {
            dialogueEntries.Enqueue(dialogueEntry);
        }
        InitiateDialogue();
    }

    private void InitiateDialogue()
    {
        CameraController.cameraController.playerControlEnabled = false;
        dialoguePanel.SetActive(true);
        TimeManager.timeManager.DisableSkipTimeButton();
        dialogueInitiated = true;
        if (!DialogueIsOver())
        {
            DequeueDialogueEntryAndDisplay();
        }
    }

    private void DequeueDialogueEntryAndDisplay()
    {
        canLoadNextDialogueEntry = false;
        if (!DialogueIsOver())
        {
            DialogueEntry currentDialogueEntry = dialogueEntries.Dequeue();
            FillDialoguePanel(currentDialogueEntry);
            currentDialogueEntry.dialogueEvent?.Invoke();
            canLoadNextDialogueEntry = true;
        }
        else
        {
            EndDialogue();
        }        
    }

    private void FillDialoguePanel(DialogueEntry currentDialogueEntry)
    {
        speakerNameTextField.text = currentDialogueEntry.dialogueSpeaker.speakerName;
        speakerImageBox.sprite = currentDialogueEntry.dialogueSpeaker.speakerSprite;
        DialogueStringTyper.dialogueStringTyper.ReceiveStringToType(currentDialogueEntry.dialogueMessage, dialogueTextField);
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        currentDialogueConfig.onDialogueEndEvent?.Invoke();
        CameraController.cameraController.playerControlEnabled = true;
        TimeManager.timeManager.EnableSkipTimeButton();
    }

    private bool DialogueIsOver()
    {
        return dialogueEntries.Count <= 0;
    }
}
