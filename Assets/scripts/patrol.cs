using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class patrol : MonoBehaviour
{

    public Transform movePoint;
    public List<Transform> interactionPoints;

    private Vector3 origPos;
    private float speed = 10000000;
    public cargo rescue;
    private bool waitMove = false;
    private bool waitDefeat = false;

    void Update()
    {
        if (!waitMove && !InvokeUtil.isPaused) {
            waitMove = true;
            InvokeUtil.Invoke(this, () => move(), mainButtonControl.currentSpeed);
        }
        if (!waitDefeat && !InvokeUtil.isPaused) {
            waitDefeat = true;
            InvokeUtil.Invoke(this, () => defeatPirate(), mainButtonControl.currentSpeed + 0.01f);
        }
    }

    private void move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, step);

        // if we moved passed map boundary, remove ship from simulation
        if (transform.position.x <= -45)
        {
            Destroy(this.gameObject);
            shipScript.patrolsExit++;
        }
        waitMove = false;
    }

    private void defeatPirate()
    {
        GameObject[] pirates = GameObject.FindGameObjectsWithTag("pirate");

        foreach (Transform sensor in interactionPoints)
        {
            if (sensor.tag == "defeatSensor")
            {
                // loop over Pirates
                foreach (GameObject p in pirates)
                {
                    // check if Pirate is at the sensor
                    if (Vector2.Distance(sensor.position, p.transform.position) <= 5.5f) {
                        //CHECKING IF THE PIRATE HAS A CAPTURE AND SETTING MOVE POINT & ROTATION BACK
                        if (p.transform.GetComponent<pirate>().hasCapture == true) {
                            p.transform.GetComponent<pirate>().captureInstance.setCargo();

                            // increment rescue counter
                            shipScript.cargosRescue += 1;

                            // if the cargo had previously evaded, increment/decrement counters
                            if(p.transform.GetComponent<pirate>().captureInstance.hasEvaded()) {
                                shipScript.evadesCaptured -= 1;
                                shipScript.evadesNotCaptured += 1;
                            }
                        }
                        // remove pirate from simulation and increment pirate defeat counter
                        Destroy(p);
                        shipScript.piratesDefeat += 1;
                    }
                }
            }
        }
        waitDefeat = false;
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