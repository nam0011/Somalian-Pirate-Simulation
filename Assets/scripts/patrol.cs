using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour {

    public Transform movePoint;
    public List<Transform> interactionPoints;

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("move", 1.0f, 1.0f);
    }

    private void move() {
        // move 1 grid
        transform.position = new Vector3(transform.position.x -10.0f, transform.position.y, transform.position.z);

        // if we moved passed map boundary, remove ship from simulation
        if (transform.position.x <= -301.0f) {
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