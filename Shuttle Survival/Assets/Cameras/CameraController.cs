﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 cameraMinLimits;
    public Vector2 cameraMaxLimits;
    Camera myCamera;
    public float scrollSpeed = 2f;
    public float minZoom = 3f;
    public float maxZoom = 12f;

    private void Start()
    {
        myCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize - (scroll * scrollSpeed * 50f * Time.deltaTime), minZoom, maxZoom);
        pos.x = Mathf.Clamp(pos.x, cameraMinLimits.x, cameraMaxLimits.x);
        pos.y = Mathf.Clamp(pos.y, cameraMinLimits.y, cameraMaxLimits.y);

        transform.position = pos;
    }
}
