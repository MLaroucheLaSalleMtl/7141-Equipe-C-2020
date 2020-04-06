using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransitionManager : MonoBehaviour
{
    public static ScreenTransitionManager screenTransitionManager;

    [SerializeField] GameObject transitionPanel;
    [SerializeField] float delayBetweenFadeOutAndFadeIn = 2f;
    Action onFadeOutEndAction;
    Action onFadeInEndAction;

    private void Awake()
    {
        if(screenTransitionManager == null)
        {
            screenTransitionManager = this;
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

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ScreenTransition(Action onFadeOutEndAction, Action onFadeInEndAction)
    {
        this.onFadeOutEndAction = onFadeOutEndAction;
        this.onFadeInEndAction = onFadeInEndAction;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        transitionPanel.SetActive(true);
        Image transitionPanelImage = transitionPanel.GetComponent<Image>();
        while(transitionPanelImage.color.a < 1f)
        {
            transitionPanelImage.color = new Color(0, 0, 0, transitionPanelImage.color.a + 0.04f);
            yield return new WaitForSecondsRealtime(0.06f);
        }
        onFadeOutEndAction?.Invoke();
        yield return new WaitForSecondsRealtime(delayBetweenFadeOutAndFadeIn);
        StartCoroutine(FadeIn());

    }

    private IEnumerator FadeIn()
    {
        Image transitionPanelImage = transitionPanel.GetComponent<Image>();
        while (transitionPanelImage.color.a > 0f)
        {
            transitionPanelImage.color = new Color(0, 0, 0, transitionPanelImage.color.a - 0.04f);
            yield return new WaitForSecondsRealtime(0.06f);
        }
        transitionPanel.SetActive(false);
        onFadeInEndAction?.Invoke();
    }
}
