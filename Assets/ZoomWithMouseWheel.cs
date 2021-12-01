using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomWithMouseWheel : MonoBehaviour
{
    [SerializeField]
    private float ScrollSpeed = 10;
    private Camera ZoomCamera;

    public float maxZoom = 1;
    public float minZoom = 35;
    public float sensitivity = 1;
    public float speed = 10;
    public float targetZoom = 35;
    private float newSize;
    // Start is called before the first frame update
    void Start()
    {
        ZoomCamera = Camera.main;
        ZoomCamera.orthographicSize = 50;

        InvokeRepeating("UpdateCameraLocation", 0.0f, 0.0001f);
    }

    void UpdateCameraLocation()
    {
        targetZoom -= Input.mouseScrollDelta.y * sensitivity;
        if (targetZoom > 275.0f) {
            targetZoom = 275.0f;
        }
        newSize = Mathf.MoveTowards(ZoomCamera.orthographicSize, targetZoom, speed * Time.deltaTime);
        ZoomCamera.orthographicSize = newSize;
    }

}
