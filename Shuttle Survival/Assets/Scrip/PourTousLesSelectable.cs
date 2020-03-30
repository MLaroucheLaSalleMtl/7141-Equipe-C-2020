using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourTousLesSelectable : MonoBehaviour
{
    private void OnMouseEnter()
    {
        GameManager.surSelectable = true;
    }

    private void OnMouseExit()
    {
        GameManager.surSelectable = false;
    }

}
