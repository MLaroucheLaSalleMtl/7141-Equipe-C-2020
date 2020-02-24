using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueEvents : MonoBehaviour
{
    [SerializeField] Transform[] cameraPositions;

    public void GetCameraToPosition(int cameraPositionIndex)
    {
        CameraController.cameraController.GetToThisPosition(cameraPositions[cameraPositionIndex].position);
    }

}
