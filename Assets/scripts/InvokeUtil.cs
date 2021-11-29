using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Static class for scripts 
 */
public static class InvokeUtil {
    public static bool isPaused = true;
    public static void Invoke(MonoBehaviour mb, Action f, float delay) {
        /*if(!isPaused) {
            mb.StartCoroutine(InvokeRoutine(f, delay));
        }*/
        mb.StartCoroutine(InvokeRoutine(f, delay));
    }

    private static IEnumerator InvokeRoutine(Action f, float delay) {
        yield return new WaitForSeconds(delay);
        f();
    }
}
