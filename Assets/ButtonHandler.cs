using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonHandler : MonoBehaviour
{
    float lastSpeed = 1.0f;
    public void setSpeed(float speed)
    {
        if (Time.timeScale != 0.0)
        {
            lastSpeed = Time.timeScale;
        }
        Console.WriteLine("valueString");
        Time.timeScale = speed;

    }

    public void start()
    {
        Time.timeScale = lastSpeed;
    }
}
