using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public float timeStepSpeed = 0;

    public void setSpeed(float speed)
    {
        timeStepSpeed = speed;
    }

    public void restartScene()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(currentSceneName);
        Time.timeScale = 0;
    }
}
