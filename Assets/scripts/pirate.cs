using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pirate : MonoBehaviour {

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("move", 1.0f, 1.0f);
    }

    private void move() {
        // move 1 grid
        transform.position = new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z);

        // if we moved passed map boundary, remove ship from simulation
        if (transform.position.y >= 301.0f) {
            Destroy(this.gameObject);
            shipScript.piratesExit++;
        }
    }
}
