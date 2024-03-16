using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TimeManager : MonoBehaviour
{
    [Header("Color Settings")]
    public Light2D GlobalLight;
    public Gradient gradientColor;

    [Header("Clock Settings")]
    public float inGameDay = 60f;
    public float days;
    private float timeProgress;
    // Update is called once per frame
    void Update()
    {
        timeProgress += Time.deltaTime / inGameDay;
        if(timeProgress >= 1f)
        {
            days++;
            timeProgress = 0f;
        }
        float dayNormalized = timeProgress % 1f;
        GlobalLight.color = gradientColor.Evaluate(dayNormalized);
    }
}
