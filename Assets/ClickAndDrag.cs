
using UnityEngine;

public class ClickAndDrag : MonoBehaviour
{
    Vector2 mouseClickPos;
    Vector2 mouseCurrentPos;
    bool panning = false;
    public float LeftXMax;
    public float RightXMax;
    public float BottomYMax;
    public float TopYMax;

    private void Update()
    {
        // When LMB clicked get mouse click position and set panning to true
        if (Input.GetKeyDown(KeyCode.Mouse0) && !panning)
        {
            mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            panning = true;
        }
        // If LMB is already clicked, move the camera following the mouse position update
        if (panning)
        {
            //functions to allow for camera panning with mouse dragging
            mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var distance = mouseCurrentPos - mouseClickPos;
            transform.position += new Vector3(-distance.x, -distance.y, 0);

            //function calls to keep camera in bounds
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(transform.position.x, LeftXMax, RightXMax);
            pos.y = Mathf.Clamp(transform.position.y, BottomYMax, TopYMax);
            transform.position = pos;
        }

        // If LMB is released, stop moving the camera
        if (Input.GetKeyUp(KeyCode.Mouse0))
            panning = false;
    }
}