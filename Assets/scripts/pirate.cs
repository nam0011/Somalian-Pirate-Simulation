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

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("move", 1.0f, 1.0f);
    }

    private void move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, step);

        //boundaries need to be edited
        // if we moved passed map boundary, remove ship from simulation
        if (transform.position.y >= 585.0f)
        {
            Destroy(this.gameObject);
            shipScript.piratesExit++;
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(movePoint.position, .2f);

    }
}
