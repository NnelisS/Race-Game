using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MapPick : MonoBehaviour
{
    public TextMeshProUGUI timeOne;
    public float giveme;

    public float scoreLength;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        giveme = PlayerPrefs.GetFloat("RaceTime");
        TimeString(giveme, timeOne);
   }
    public string TimeString(float lapTime, TextMeshProUGUI laptimer)
    {
        float mSec = (int)((lapTime - (int)lapTime) * 100);
        float sec = (int)(lapTime % 60);
        float min = (int)(lapTime / 60 % 60);
        return laptimer.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, mSec);
    }

}
