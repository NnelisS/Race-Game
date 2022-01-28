using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private float timer = 58;

    public GameObject video;

    public GameObject logo;
    public GameObject pressEnter;

    public bool canGoToMenu = true;
    public bool goToMenu = false;

    private void Start()
    {
        logo.SetActive(false);
        pressEnter.SetActive(false);
    }

    void Update()
    {
        if (canGoToMenu)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                video.SetActive(false);
                logo.SetActive(true);
                pressEnter.SetActive(true);
            }
        }

        if (canGoToMenu)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                canGoToMenu = false;
                video.SetActive(false);
                logo.SetActive(true);
                pressEnter.SetActive(true);
                goToMenu = true;
            }
        }

        if (goToMenu)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Menu 2");
            }
        }
    }
}
