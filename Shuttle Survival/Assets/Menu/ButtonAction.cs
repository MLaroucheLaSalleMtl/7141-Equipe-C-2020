using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Selectable[] selectionDefault;
    [SerializeField] private GameObject credits;
    private AsyncOperation async;
    public void LoadScene(int sceneID)
    {
        if(async == null)
        {
            async = SceneManager.LoadSceneAsync(sceneID);
            async.allowSceneActivation = true;
        }
    }
    public void LoadScene(string sceneName)
    {
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(sceneName);
            async.allowSceneActivation = true;
        }
    }

    public void ChangeMenu(int panelNumber)
    {
        Input.ResetInputAxes();
        for(int i = 0; i< panels.Length; i++)
        {
            panels[i].SetActive(panelNumber == i);
            if (panelNumber == i) { 
                selectionDefault[i].Select(); 
            }
        }
    }

    private void Start()
    {
        ChangeMenu(0);
        credits.SetActive(false);
    }

    public void Quitter()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Credits()
    {
        credits.SetActive(true);
    }

    public void CloseAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

}
