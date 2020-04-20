using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController cameraController;

    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 cameraMinLimits;
    public Vector2 cameraMaxLimits;
    Camera myCamera;
    public float scrollSpeed = 2f;
    public float minZoom = 3f;
    public float maxZoom = 12f;
    public float goalZoomForEvents = 6.5f;
    public float zoomSpeedForEvents = 3f;
    
    public bool playerControlEnabled = true;
    public bool lerping = false;
    public bool changingZoom = false;
    public float lerpingSpeed = 0.4f;
    Vector3 pos;
    Vector3 savedPosition;
    Action atPositionAction;
    private void Awake()
    {
        if(CameraController.cameraController == null)
        {
            CameraController.cameraController = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        myCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControlEnabled)
        {
            Vector3 newPos = transform.position;

            float h = InputHandler.inputHandler.h;
            float v = InputHandler.inputHandler.v;
            if (v > 0 || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                newPos.y += panSpeed * Time.deltaTime;
            }
            if (v < 0 || Input.mousePosition.y <= panBorderThickness)
            {
                newPos.y -= panSpeed * Time.deltaTime;
            }
            if (h > 0 || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                newPos.x += panSpeed * Time.deltaTime;
            }
            if (h < 0 || Input.mousePosition.x <= panBorderThickness)
            {
                newPos.x -= panSpeed * Time.deltaTime;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize - (scroll * scrollSpeed * 50f * Time.deltaTime), minZoom, maxZoom);
            newPos.x = Mathf.Clamp(newPos.x, cameraMinLimits.x, cameraMaxLimits.x);
            newPos.y = Mathf.Clamp(newPos.y, cameraMinLimits.y, cameraMaxLimits.y);
            transform.position = newPos;

        }
        if (lerping)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(pos.x, pos.y, -5), lerpingSpeed * Time.deltaTime);
            if (changingZoom)
            {
                myCamera.orthographicSize = Mathf.Lerp(myCamera.orthographicSize, goalZoomForEvents, zoomSpeedForEvents * Time.deltaTime);
                if(Mathf.Abs(myCamera.orthographicSize - goalZoomForEvents) < 0.1f)
                {
                    changingZoom = false;
                }
            }
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(pos.x, pos.y)) < 0.15f)
            {
                lerping = false;
                atPositionAction?.Invoke();
            }
        }

    }

    public void GetToThisPosition(Vector3 pos, Action atPositionAction = null)
    {
        playerControlEnabled = false;
        this.atPositionAction = atPositionAction;
        this.pos = new Vector3(pos.x, pos.y, -5);
        this.savedPosition = transform.position;
        lerping = true;
        changingZoom = true;
    }
}
