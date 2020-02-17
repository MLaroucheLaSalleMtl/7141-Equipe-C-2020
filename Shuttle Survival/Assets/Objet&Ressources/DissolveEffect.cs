using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField] Material material;

    float dissolveAmount;
    bool isDissolving;
    Action onDoneDissolving;

    private void Awake()
    {
        GetComponent<MeshRenderer>().material = new Material(material);
        material = GetComponent<MeshRenderer>().material;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDissolving)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount += Time.deltaTime / 3);
            material.SetFloat("_DissolveAmount", dissolveAmount);
            if(dissolveAmount > 0.98f)
            {
                onDoneDissolving?.Invoke();
            } 
        }
    }

    public void StartDissolve(Action onDoneDissolving)
    {
        this.onDoneDissolving = onDoneDissolving;
        isDissolving = true;
    }
}
