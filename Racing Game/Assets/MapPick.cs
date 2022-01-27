using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapPick : MonoBehaviour
{
    public TextMeshProUGUI timeOne;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeOne.text = string.Format("Time 1: {0}", PlayerPrefs.GetFloat("RaceTime"));
    }
}
