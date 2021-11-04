using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour {

    public Transform movePoint;
    public List<Transform> interactionPoints;

    private Vector3 origPos;
    private float speed = 10000000;

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("move", 1.0f, 1.0f);
    }

    private void move() {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, step);

        // if we moved passed map boundary, remove ship from simulation
        if (transform.position.x <= -45) {
            Destroy(this.gameObject);
            shipScript.patrolsExit++;
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(movePoint.position, .2f);

        Gizmos.color = Color.green;
        for (int i = 0; i < interactionPoints.Count; i++)
        {
            Gizmos.DrawSphere(interactionPoints[i].position, .2f);
        }
    }
}