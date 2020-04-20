using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollingEnd : MonoBehaviour
{
    [SerializeField] private GameObject panelCredit;
    private AsyncOperation async;

    public void End(string message)
    {
        if (message.Equals("EndCreditCompleted"))
        {
            panelCredit.SetActive(false);
        }

        if (message.Equals("LoadMenu"))
        {
            if (async == null)
            {
                async = SceneManager.LoadSceneAsync("MenuPrincipal");
                async.allowSceneActivation = true;
            }
        }
    }
}
