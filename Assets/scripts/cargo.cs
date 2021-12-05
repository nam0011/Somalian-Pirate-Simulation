using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cargo : MonoBehaviour {

    public Transform movePoint;
    public Transform captureMovePoint;
    public Transform evadeTarget;
    public List<Transform> interactionPoints;
    private List<int> piratesEvaded = new List<int>(); // a Cargo may only evade each Pirate once
    public bool isCaptured = false;
    public GameObject capPirate;
    private bool waitMove = false;
    private bool waitEvade = false;
    private bool waitCapture = false;

    private Vector3 origPos;
    private float speed = 10000000;
    private Color captureColor = new Color(0.8f, 0.8f, 0.0f, 1.0f);

    void Update() {

        if (!waitMove && !InvokeUtil.isPaused) {
            waitMove = true;
            InvokeUtil.Invoke(this, () => move(), mainButtonControl.currentSpeed);
        }
        if (!waitEvade && !InvokeUtil.isPaused) {
            waitEvade = true;
            InvokeUtil.Invoke(this, () => evadeAction(), mainButtonControl.currentSpeed + 0.01f);
        }
        if (!waitCapture && !InvokeUtil.isPaused) {
            waitCapture = true;
            InvokeUtil.Invoke(this, () => captureAction(), mainButtonControl.currentSpeed + 0.01f);
        }
    }

    private void move() {
        // move 1 grid
        float step = speed * Time.deltaTime;
        if (isCaptured) {
            transform.position = Vector3.MoveTowards(transform.position, captureMovePoint.position, step);
        } else {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, step);
        }

        // if we moved passed map boundary, remove ship from simulation
        if (transform.position.x >= 2630.0f || transform.position.y <= -25.0f) {
            Destroy(this.gameObject);
            shipScript.cargosExit++;
        }
        waitMove = false;
    }

    /* this method should be called each timestep. It shall check if any
    * Pirate ships are overlapping with the evade sensors and move the
    * Cargo 1 grid northeast if so.
    */
    private void evadeAction() {
        if (!isCaptured) {
            // get all Pirates
            GameObject[] pirates = GameObject.FindGameObjectsWithTag("pirate");
            // looping thru evade sensors
            foreach (Transform sensor in interactionPoints) {
                if (sensor.tag == "evadeSensor") {
                    // loop over Pirates
                    foreach (GameObject p in pirates) {
                        // check if Pirate is at the sensor
                        if (Vector2.Distance(sensor.position, p.transform.position) <= 5.5f
                            && !piratesEvaded.Contains(p.GetInstanceID())
                            && !p.transform.GetComponent<pirate>().hasCapture) {
                            // then Cargo should evade
                            Debug.Log("Before: " + transform.position.y.ToString());
                            transform.position = Vector3.MoveTowards(transform.position, evadeTarget.position, speed * Time.deltaTime);
                            Debug.Log("After: " + transform.position.y.ToString());
                            shipScript.evadesNotCaptured += 1; // needs to be decremented if captured
                            piratesEvaded.Add(p.GetInstanceID());
                        }
                    }
                }
            }
        }
        waitEvade = false;
    }

    /* this method should be called each timestep. It shall check if any
    * Pirate ships are overlapping with the capture sensors and capture
    * the Cargo if so.
    */
    private void captureAction() {
        if(!isCaptured) {
            // get all Pirates
            GameObject[] pirates = GameObject.FindGameObjectsWithTag("pirate");
            // looping thru evade sensors
            foreach (Transform sensor in interactionPoints) {
                if (sensor.tag == "captureSensor") {
                    // loop over Pirates
                    foreach (GameObject p in pirates) {
                        // check if Pirate is at the sensor
                        if (Vector2.Distance(sensor.position, p.transform.position) <= 5.5f && !p.transform.GetComponent<pirate>().hasCapture) {
                            // then Cargo becomes a Capture; change color & direction of Cargo,
                            // move Pirate to Cargo's grid, and change direction of Pirate

                            // set Pirate move point and move to Capture's grid
                            Vector3 pirateMovePointPos = p.transform.GetComponent<pirate>().movePoint.localPosition;
                            p.transform.GetComponent<pirate>().movePoint.localPosition = new Vector3(pirateMovePointPos.x, pirateMovePointPos.y - 2.436f, pirateMovePointPos.z);
                            p.transform.position = transform.position;
                            Debug.Log("Capture Cargo: " + transform.position);
                            Debug.Log("Capture Pirate: " + transform.position);

                            // set direction of movement
                            setCapture();

                            // increment capture counter for cargo and evade if necessary
                            shipScript.cargosCapture += 1; // needs to be decremented if rescued
                            if(hasEvaded()) {
                                shipScript.evadesNotCaptured -= 1;
                                shipScript.evadesCaptured += 1;
                            }
                            capPirate = p;
                            p.transform.GetComponent<pirate>().hasCapture = true;
                            p.transform.GetComponent<pirate>().captureInstance = this;
                        }
                    }
                }
            }
        }
        waitCapture = false;
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

    public void setCapture() {
        isCaptured = true;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = captureColor;
    }

    public void setCargo() {
        isCaptured = false;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
    }

    public bool hasEvaded() {
        if (piratesEvaded.Count > 0) {
            return true;
        } else {
            return false;
        }
    }
}
