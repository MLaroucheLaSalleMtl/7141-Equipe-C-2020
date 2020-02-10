using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagePopup : MonoBehaviour
{
    public static MessagePopup MessagePopupManager;
    [SerializeField] TextMeshProUGUI messagePopupText;
    [SerializeField] float animationDelay = 2f;
    Animator animator;

    void Awake()
    {
        if(MessagePopup.MessagePopupManager == null)
        {
            MessagePopup.MessagePopupManager = this;
        }
        else
        {
            Destroy(this);
        }        
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Methode qui sert a afficher un message popup a l'ecran
    /// </summary>
    /// <param name="messageString">le string que l'on veut afficher a l'ecran</param>
    public void SetStringAndShowPopup(string messageString)
    {
        gameObject.SetActive(true);
        messagePopupText.text = messageString;
        StartCoroutine(AnimationDelay());

    }

    IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(animationDelay);
        animator.SetTrigger("MessagePopup"); //gameobject is set to inactive in animation event.               
    }

    public void DesactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}
