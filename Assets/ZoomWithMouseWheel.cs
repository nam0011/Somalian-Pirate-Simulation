using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomWithMouseWheel : MonoBehaviour
{
    [SerializeField]
    private float ScrollSpeed = 10;
    private Camera ZoomCamera;

    public float maxZoom = 5;
    public float minZoom = 35;
    public float sensitivity = 1;
    public float speed = 10;
    public float targetZoom = 35;

    // Start is called before the first frame update
    void Start()
    {
        ZoomCamera = Camera.main;
        ZoomCamera.orthographicSize = 35;
    }

    // Update is called once per frame
    void Update()
    {
        
         targetZoom -= Input.mouseScrollDelta.y * sensitivity;
         targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
         float newSize = Mathf.MoveTowards(ZoomCamera.orthographicSize, targetZoom, speed * Time.deltaTime);
         ZoomCamera.orthographicSize = newSize;
         

         /*
        if (ZoomCamera.orthographic)
                ZoomCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        */
    }

}
