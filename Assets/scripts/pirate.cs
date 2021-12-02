using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pirate : MonoBehaviour
{

    public Transform movePoint;
    public bool hasCapture = false;
    public cargo captureInstance;
    private Vector3 origPos;
    private float speed = 10000000;
    private bool wait = false;

    // Update is called once per frame
    void Update()
    {
        if(!wait && !InvokeUtil.isPaused) {
            wait = true;
            InvokeUtil.Invoke(this, () => move(), mainButtonControl.currentSpeed);
        }
    }

    private void move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, step);

        //boundaries need to be edited
        // if we moved passed map boundary, remove ship from simulation
        if (transform.position.y >= 585.0f || transform.position.y <= -25.0f)
        {
            Destroy(this.gameObject);
            shipScript.piratesExit++;
        }

        wait = false;
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(movePoint.position, .2f);

    }
}
