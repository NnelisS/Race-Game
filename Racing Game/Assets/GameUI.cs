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

    private BoxCollider boxCol;
    public GameObject finish;
    public GameObject speedoMeter;

    public Animator panel;
    public Animator laps;

    public GameObject finishCam;

    private float timerForScale = 0.5f;
    private bool activateIt = false;

    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();
    }

    public void Update()
    {
        Debug.Log(timerForScale);

        if (activateIt)
        {
            timerForScale -= Time.deltaTime;
            if (timerForScale <= 0)
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = Time.timeScale * 1f;
            }
        }


        if (timerOneActivate)
        {
            lapOne.gameObject.SetActive(true);

            timerLapOne += Time.deltaTime;

            lapOne.text = string.Format("" + TimeString(timerLapOne, lapOne));
        }
        else if (timerTwoActivate)
        {
            lapTwo.gameObject.SetActive(true);

            timerLapTwo += Time.deltaTime;
            lapTwo.text = string.Format("" + TimeString(timerLapTwo, lapTwo));
        }
        else if (timerThreeActivate)
        {
            lapThree.gameObject.SetActive(true);

            timerLapThree += Time.deltaTime;
            lapThree.text = string.Format("" + TimeString(timerLapThree, lapThree));
        }
        else if (calculateTotaleTime)
        {
            totalTime.gameObject.SetActive(true);

            totalTimer = timerLapOne + timerLapTwo + timerLapThree;
            totalTime.text = string.Format("" + TimeString(totalTimer, totalTime));

            PlayerPrefs.SetFloat("RaceTime", totalTimer);
        }
    }

    public string TimeString(float lapTime, TextMeshProUGUI laptimer)
    {
        float mSec = (int)((lapTime - (int)lapTime) * 100);
        float sec = (int)(lapTime % 60);
        float min = (int)(lapTime / 60 % 60);
        return laptimer.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, mSec);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FirstLap"))
        {
            if (timerOneActivate == false && timerTwoActivate == false && timerThreeActivate == false)
            {
                Debug.Log("on");
                timerOneActivate = true;
                other.gameObject.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("NewLap"))
        {
            if (timerOneActivate)
            {
                StartCoroutine(ColliderOn(other.gameObject));
                timerOneActivate = false;
                timerTwoActivate = true;
            }
            else if (timerTwoActivate)
            {
                timerTwoActivate = false;
                timerThreeActivate = true;
                StartCoroutine(ColliderOff(other.gameObject));
            }
            else if (timerThreeActivate)
            {
                totalTime.enabled = true;
                timerThreeActivate = false;
                calculateTotaleTime = true;
            }
        }
        if (other.gameObject.CompareTag("finish"))
        {
            calculateTotaleTime = true;
            timerThreeActivate = false;
            speedoMeter.SetActive(false);
            panel.Play("Panel");
            laps.Play("Laps");
            activateIt = true;
            finishCam.gameObject.SetActive(true);
            Time.timeScale = 0.05f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    public IEnumerator ColliderOn(GameObject trigger)
    {
        trigger.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        trigger.gameObject.SetActive(true);
    }
    public IEnumerator ColliderOff(GameObject trigger)
    {
        trigger.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        finish.gameObject.SetActive(true);
    }
}