using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private AsyncOperation async;
    //[SerializeField] private Animation anim;
    // Start is called before the first frame update
    void Start()
    {
       
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(sceneName);
            //async.allowSceneActivation = true;
        }
    }

    
}
