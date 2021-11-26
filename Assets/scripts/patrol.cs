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

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("move", 1.0f, 1.0f);
        InvokeRepeating("defeatPirate", 1.0f, 1.0f);

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
    }

    private void defeatPirate()
    {
        GameObject[] pirates = GameObject.FindGameObjectsWithTag("pirate");
        GameObject[] cargos = GameObject.FindGameObjectsWithTag("cargo");

        foreach (Transform sensor in interactionPoints)
        {
            if (sensor.tag == "defeatSensor")
            {
                // loop over Pirates
                foreach (GameObject p in pirates)
                {
                    foreach (GameObject c in cargos)
                    {
                        // check if Pirate is at the sensor
                        if (Vector2.Distance(sensor.position, p.transform.position) <= 5)
                        {
                            shipScript.piratesDefeat += 1;
                            //CHECKING IF THE PIRATE HAS A CAPTURE AND SETTING ROTATION BACK
                            if (p.transform.GetComponent<pirate>().hasCapture == true )
                            {
                                
                                    Debug.Log("this rotated the cargos");
                                    c.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                                
                            }
                            Destroy(p);
                        }
                    }
                }
            }
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