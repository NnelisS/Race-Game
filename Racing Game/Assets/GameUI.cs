using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI lapOne;
    public TextMeshProUGUI lapTwo;
    public TextMeshProUGUI lapThree;
    public TextMeshProUGUI totalTime;

    public float timerLapOne = 0;
    public float timerLapTwo = 0;
    public float timerLapThree = 0;
    public float totalTimer = 0;

    public bool timerOneActivate = false;
    public bool timerTwoActivate = false;
    public bool timerThreeActivate = false;
    public bool calculateTotaleTime = false;

    private float time;
    private float mSec;
    private float sec;
    private float min;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            if (timerOneActivate == false && timerTwoActivate == false && timerThreeActivate == false)
            {
                timerOneActivate = true;
            }
            else if (timerOneActivate)
            {
                timerOneActivate = false;
                timerTwoActivate = true;
            }
            else if (timerTwoActivate)
            {
                timerTwoActivate = false;
                timerThreeActivate = true;
            }
            else if (timerThreeActivate)
            {
                timerThreeActivate = false;
                calculateTotaleTime = true;
            }
        }

        if (timerOneActivate)
        {
            timerLapOne += Time.deltaTime;

            lapOne.text = string.Format("Lap 1: " + TimeString(timerLapOne, lapOne));
        }
        else if (timerTwoActivate)
        {
            timerLapTwo += Time.deltaTime;
            lapTwo.text = string.Format("Lap 2: " + TimeString(timerLapTwo, lapTwo));
        }
        else if (timerThreeActivate)
        {
            timerLapThree += Time.deltaTime;
            lapThree.text = string.Format("Lap 3: " + TimeString(timerLapThree, lapThree));
        }
        else if (calculateTotaleTime)
        {
            totalTimer = timerLapOne + timerLapTwo + timerLapThree;
            totalTime.text = string.Format("Total Time: " + TimeString(totalTimer, totalTime));
        }
    }

    public string TimeString(float lapTime, TextMeshProUGUI laptimer)
    {
        float mSec = (int)((lapTime - (int)lapTime) * 100);
        float sec = (int)(lapTime % 60);
        float min = (int)(lapTime / 60 % 60);
        return laptimer.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, mSec);
    }
}