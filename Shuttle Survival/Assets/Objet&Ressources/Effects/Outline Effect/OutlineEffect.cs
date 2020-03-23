using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineEffect : MonoBehaviour, ISelectable
{
    [SerializeField] Material material;
    [SerializeField] Color outlineColor = new Color(72, 255, 0, 255);
    [Range(0, 10f)]
    [SerializeField] float outlineThickness = 3f;
    SpriteRenderer spriteRenderer;
    bool isCharacter = false;

    public void OnDeselection()
    {
        if(spriteRenderer)
            spriteRenderer.material.SetFloat("_OutlineThickness", 0f);
    }

    public void OnSelection()
    {
        if (spriteRenderer)
            spriteRenderer.material.SetFloat("_OutlineThickness", outlineThickness);
    }

    private void OnMouseDown()
    {
        if (isCharacter)
            SelectionManager.selectionManager.ChangeCharacterSelection(this);
        else
            SelectionManager.selectionManager.ChangeObjectSelection(this);
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.material = new Material(material);
        material = spriteRenderer.material;
        if (GetComponent<CharacterSystem>())
        {
            isCharacter = true;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        material.SetFloat("_OutlineThickness", 0f);
        material.SetColor("_OutlineColor", outlineColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
