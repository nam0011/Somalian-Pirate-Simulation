using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cargo : MonoBehaviour {

    public Transform movePoint;
    public Transform evadeTarget;
    public List<Transform> interactionPoints;
    private List<int> piratesEvaded = new List<int>(); // a Cargo may only evade each Pirate once

    private Vector3 origPos;
    private float speed = 10000000;
    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("move", 1.0f, 1.0f);
        InvokeRepeating("evadeAction", 1.01f, 1.01f);
    }

    private void move() {
        // move 1 grid
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, step);

        // if we moved passed map boundary, remove ship from simulation
        if (transform.position.x >= 2630.0f) {
            Destroy(this.gameObject);
            shipScript.cargosExit++;
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

    /* this method should be called each timestep. It shall check if any
    * Pirate ships are overlapping with the evade sensors and move the
    * Cargo 1 grid northeast if so.
    */
    private void evadeAction() {
        // get all Pirates
        GameObject[] pirates = GameObject.FindGameObjectsWithTag("pirate");
        // looping thru evade sensors
        foreach (Transform sensor in interactionPoints) {
            if (sensor.tag == "evadeSensor") {
                // loop over Pirates
                foreach (GameObject p in pirates) {
                    // check if Pirate is at the sensor
                    if (Vector2.Distance(sensor.position, p.transform.position) <= 6.8 && !piratesEvaded.Contains(p.GetInstanceID())) {
                        // then Cargo should evade
                        transform.position = Vector3.MoveTowards(transform.position, evadeTarget.position, speed * Time.deltaTime);
                        shipScript.evadesNotCapture += 1; // needs to be decremented if captured
                        piratesEvaded.Add(p.GetInstanceID());
                    }
                }
            }
        }
    }
}
