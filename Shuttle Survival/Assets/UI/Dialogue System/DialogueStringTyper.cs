using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueStringTyper : MonoBehaviour
{
    public static DialogueStringTyper dialogueStringTyper;
    [SerializeField] float typingSpeed = 3f;
    char[] messageChars;
    TextMeshProUGUI fieldToTypeIn;

    private void Awake()
    {
        if(dialogueStringTyper == null)
        {
            dialogueStringTyper = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void ReceiveStringToType(string messageToType, TextMeshProUGUI fieldToTypeIn)
    {
        StopAllCoroutines();
        this.fieldToTypeIn = fieldToTypeIn;
        fieldToTypeIn.text = "";       
        messageChars = messageToType.ToCharArray();
        StartCoroutine(TypingCoroutine());
        
    }
    
    IEnumerator TypingCoroutine()
    {
        for (int i = 0; i < messageChars.Length; i++)
        {
            fieldToTypeIn.text += messageChars[i];
            yield return new WaitForSecondsRealtime(0.1f /typingSpeed);
        }       
    }
}
