using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSpawnColorGizmo : MonoBehaviour
{
    public Transform gizmo;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(gizmo.position, .2f);
    }
}
