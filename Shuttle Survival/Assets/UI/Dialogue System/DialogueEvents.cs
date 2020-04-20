using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueEvents : MonoBehaviour
{
    [SerializeField] Transform[] cameraPositions;

    [Header("Buttons Activation Dialogue Events")]
    [SerializeField] int amountOfButtonFlickers = 3;
    [SerializeField] float timeBetweenFlickers = 0.25f;

    public void GetCameraToPosition(int cameraPositionIndex)
    {
        CameraController.cameraController.GetToThisPosition(cameraPositions[cameraPositionIndex].position);
    }

    public void ActivateCollider(GameObject gameObjectWithColliderToActivate)
    {
        gameObjectWithColliderToActivate.GetComponent<Collider2D>().enabled = true;
    }

    public void ScanPathfindGraphs()
    {       
        AstarPath.active.Scan();
    }

    public void ActivateAndFlickerButtons(GameObject button)
    {
        StartCoroutine(FlickerButton(button));
    }

    private IEnumerator FlickerButton(GameObject button)
    {
        button.SetActive(true);
        for (int i = 0; i < amountOfButtonFlickers; i++)
        {
            yield return new WaitForSecondsRealtime(timeBetweenFlickers);
            button.SetActive(false);
            yield return new WaitForSecondsRealtime(timeBetweenFlickers);
            button.SetActive(true);
        }
    }
}
