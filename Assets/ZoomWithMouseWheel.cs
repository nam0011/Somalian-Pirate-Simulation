using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomWithMouseWheel : MonoBehaviour
{
    [SerializeField]
    private float ScrollSpeed = 10;
    private Camera ZoomCamera;

    // Start is called before the first frame update
    void Start()
    {
        ZoomCamera = Camera.main;
        InvokeRepeating("CheckCamera", 0.0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (ZoomCamera.orthographic)
                ZoomCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;        
    }

    public void CheckCamera()
    {
        if(ZoomCamera.orthographicSize <= 0)
        {
            ZoomCamera.orthographicSize = 0;
        }
        else if(ZoomCamera.orthographicSize >= 35)
        {
            ZoomCamera.orthographicSize = 35;
        }
    }
}
