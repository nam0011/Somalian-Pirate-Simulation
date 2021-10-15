using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSpawnGizmoPaint : MonoBehaviour
{
    public Transform gizmo;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(gizmo.position, .2f);
    }
}
