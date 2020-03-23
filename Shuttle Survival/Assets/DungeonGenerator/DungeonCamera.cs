using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{
    [SerializeField] GameObject dungeonCamera;
    public static DungeonCamera dungeonCameraHolder;
    Action atPositionAction;
    Vector3 pos;
    bool lerping;
    [SerializeField] float lerpingSpeed;

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

    private void Update()
    {
        if (lerping)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(pos.x, pos.y, -10), lerpingSpeed * Time.deltaTime);

            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(pos.x, pos.y)) < 0.015f)
            {
                lerping = false;
                transform.position = pos;
                atPositionAction?.Invoke();
            }
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

    public void GetToThisPosition(Vector3 pos, Action atPositionAction = null)
    {
        this.atPositionAction = atPositionAction;
        this.pos = new Vector3(pos.x, pos.y, -5);
        lerping = true;
    }
}
