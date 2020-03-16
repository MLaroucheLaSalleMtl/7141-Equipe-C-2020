using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{
    [SerializeField] GameObject dungeonCamera;
    public static DungeonCamera dungeonCameraHolder;

    private void Awake()
    {
        if(dungeonCameraHolder == null)
        {
            dungeonCameraHolder = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void ActivateDungeonCamera()
    {
        dungeonCamera.SetActive(true);
    }

    public void DesactivateDungeonCamera()
    {
        dungeonCamera.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
