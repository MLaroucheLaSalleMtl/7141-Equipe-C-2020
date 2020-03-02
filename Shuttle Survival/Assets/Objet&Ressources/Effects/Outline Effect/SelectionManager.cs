using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager selectionManager;
    [SerializeField] ISelectable currentObjectSelection;
    [SerializeField] ISelectable currentCharacterSelection;


    private void Awake()
    {
        if(selectionManager == null)
        {
            selectionManager = this;
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

    public void ChangeObjectSelection(ISelectable selection)
    {
        if(currentObjectSelection != null)
            currentObjectSelection.OnDeselection();        
        currentObjectSelection = selection;
        if(currentObjectSelection != null)
            currentObjectSelection.OnSelection();
    }
    public void RemoveObjectSelection()
    {
        if (currentObjectSelection != null)
            currentObjectSelection.OnDeselection();
        currentObjectSelection = null;
    }

    public void ChangeCharacterSelection(ISelectable selection)
    {
        if (currentCharacterSelection != null)
            currentCharacterSelection.OnDeselection();
        currentCharacterSelection = selection;
        if (currentCharacterSelection != null)
            currentCharacterSelection.OnSelection();
    }
    public void RemoveCharacterSelection()
    {
        if (currentCharacterSelection != null)
            currentCharacterSelection.OnDeselection();
        currentCharacterSelection = null;
    }
}
