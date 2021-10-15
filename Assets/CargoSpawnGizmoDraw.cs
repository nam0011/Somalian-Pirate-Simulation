using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoSpawnGizmoDraw : MonoBehaviour
{
    public Transform gizmo;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gizmo.position, .2f);
    }
}
