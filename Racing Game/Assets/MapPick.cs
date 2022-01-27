using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapPick : MonoBehaviour
{
    public TextMeshProUGUI timeOne;
    public float giveme;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        giveme = PlayerPrefs.GetFloat("RaceTime");
        timeOne.text = string.Format("Time 1: {0}", giveme);
    }
}
