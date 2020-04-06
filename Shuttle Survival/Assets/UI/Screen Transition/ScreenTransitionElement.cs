using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransitionElement : MonoBehaviour
{
    TextMeshProUGUI tmpro;

    // Start is called before the first frame update
    void Start()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tmpro.color = new Color(tmpro.color.r, tmpro.color.b, tmpro.color.g, Mathf.Clamp01(GetComponentInParent<Image>().color.a - 0.2f));
    }
}
