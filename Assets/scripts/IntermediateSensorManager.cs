using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateSensorManager : MonoBehaviour {

    [SerializeField]
    SensorOnOff[] sensorArray;
    int ArraySize;
    int SensorsOn;

    public bool on;

    bool paused;
    void Start()
    {
        ArraySize = sensorArray.Length;
        paused = false;
    }
    void Update()
    {
        SensorsOn = 0;
        on = false;
        for (int i = 0; i < ArraySize; i++)
        {
            if (sensorArray[i].on)
            {
                SensorsOn += 1;
            }
            sensorArray[i].Reset();
        }
        // detect if really on
        if (2 * SensorsOn >= ArraySize)
        {
            on = true;
        }
    }

    public void OnPauseGame()
    {
        paused = true;

    }
    public void OnResumeGame()
    {
        paused = false;

    }

}
