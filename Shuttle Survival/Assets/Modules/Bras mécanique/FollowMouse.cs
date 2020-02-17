using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.IK;

public class FollowMouse : MonoBehaviour
{
    private Vector3 posSouris;
    [SerializeField] private Transform targetIK;
    [SerializeField] private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        posSouris = Input.mousePosition;       
        Vector3 vect = camera.ScreenToWorldPoint(new Vector3(posSouris.x, posSouris.y, 0 ));
        targetIK.transform.position = vect;
    }

}
