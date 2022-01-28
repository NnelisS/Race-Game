using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public Animator menuChange;
    public GameObject buttonTrackSelect;
    public GameObject buttonToCar;
    public GameObject buttonShowRoom;
    public GameObject buttonCredits;
    public GameObject buttonPlayGame;
    public GameObject buttonBack;

    public bool mainMenu = true;
    public bool trackSelect = false;
    public bool carSelect = false;
    public bool credits = false;
    public bool showRoom = false;
    public bool showCar = false;

    private void Start()
    {
        buttonPlayGame.SetActive(false);
        buttonToCar.SetActive(false);
        buttonBack.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainMenu == false)
            {
                if (trackSelect)
                {
                    menuChange.Play("MenuChangeBack");
                    StartCoroutine(ButtonPress(buttonBack, buttonBack));
                    StartCoroutine(ButtonPress(buttonBack, buttonTrackSelect));
                    StartCoroutine(ButtonPress(buttonBack, buttonShowRoom));
                    StartCoroutine(ButtonPress(buttonBack, buttonCredits));
                    trackSelect = false;
                    mainMenu = true;
                }
                else if (carSelect)
                {
                    menuChange.Play("TrackToCarBack");
                    StartCoroutine(ButtonPress(buttonBack, buttonBack));
                    StartCoroutine(ButtonPress(buttonPlayGame, buttonTrackSelect));
                    carSelect = false;
                    trackSelect = true;
                }
                else if (credits)
                {
                    menuChange.Play("MenuToCreditsBack");
                    StartCoroutine(ButtonPress(buttonBack, buttonBack));
                    StartCoroutine(ButtonPress(buttonBack, buttonTrackSelect));
                    StartCoroutine(ButtonPress(buttonBack, buttonShowRoom));
                    StartCoroutine(ButtonPress(buttonBack, buttonCredits));
                    credits = false;
                    mainMenu = true;
                }
                else if (showRoom)
                {
                    menuChange.Play("MenuToShowBack");
                    StartCoroutine(ButtonPress(buttonBack, buttonBack));
                    StartCoroutine(ButtonPress(buttonBack, buttonTrackSelect));
                    StartCoroutine(ButtonPress(buttonBack, buttonShowRoom));
                    StartCoroutine(ButtonPress(buttonBack, buttonCredits));
                    showRoom = false;
                    mainMenu = true;
                }
            }
        }
    }

    public void ToCarSelect()
    {
        menuChange.Play("TrackToCar");
        StartCoroutine(ButtonPress(buttonToCar, buttonPlayGame));
        carSelect = true;
        trackSelect = false;
        showRoom = false;
        credits = false;
        mainMenu = false;
    }

    public void ToTimeTrial()
    {
        menuChange.Play("MenuChange");
        StartCoroutine(ButtonPress(buttonTrackSelect, buttonToCar));
        StartCoroutine(ButtonPress(buttonShowRoom, buttonBack));
        StartCoroutine(ButtonPress(buttonCredits, buttonBack));
        trackSelect = true;
        carSelect = false;
        showRoom = false;
        credits = false;
        mainMenu = false;
    }

    public void ToShowRoom()
    {
        menuChange.Play("MenuToShow");
        StartCoroutine(ButtonPress(buttonShowRoom, buttonBack));
        StartCoroutine(ButtonPress(buttonTrackSelect, buttonBack));
        StartCoroutine(ButtonPress(buttonCredits, buttonBack));
        showRoom = true;
        trackSelect = false;
        carSelect = false;
        credits = false;
        mainMenu = false;
    }

    public void ToCredits()
    {
        menuChange.Play("MenuToCredits");
        StartCoroutine(ButtonPress(buttonShowRoom, buttonBack));
        StartCoroutine(ButtonPress(buttonTrackSelect, buttonBack));
        StartCoroutine(ButtonPress(buttonCredits, buttonBack));
        credits = true;
        trackSelect = false;
        showRoom = false;
        carSelect = false;
        mainMenu = false;
    }

    public void ToPlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void BackButton()
    {
        if (mainMenu == false)
        {
            if (trackSelect)
            {
                menuChange.Play("MenuChangeBack");
                StartCoroutine(ButtonPress(buttonBack, buttonBack));
                StartCoroutine(ButtonPress(buttonBack, buttonTrackSelect));
                StartCoroutine(ButtonPress(buttonBack, buttonShowRoom));
                StartCoroutine(ButtonPress(buttonBack, buttonCredits));
                trackSelect = false;
                mainMenu = true;
            }
            else if (carSelect)
            {
                menuChange.Play("TrackToCarBack");
                StartCoroutine(ButtonPress(buttonBack, buttonBack));
                StartCoroutine(ButtonPress(buttonPlayGame, buttonTrackSelect));
                carSelect = false;
                trackSelect = true;
            }
            else if (credits)
            {
                menuChange.Play("MenuToCreditsBack");
                StartCoroutine(ButtonPress(buttonBack, buttonBack));
                StartCoroutine(ButtonPress(buttonBack, buttonTrackSelect));
                StartCoroutine(ButtonPress(buttonBack, buttonShowRoom));
                StartCoroutine(ButtonPress(buttonBack, buttonCredits));
                credits = false;
                mainMenu = true;

            }
            else if (showRoom)
            {
                menuChange.Play("MenuToShowBack");
                StartCoroutine(ButtonPress(buttonBack, buttonBack));
                StartCoroutine(ButtonPress(buttonBack, buttonTrackSelect));
                StartCoroutine(ButtonPress(buttonBack, buttonShowRoom));
                StartCoroutine(ButtonPress(buttonBack, buttonCredits));
                showRoom = false;
                mainMenu = true;
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public IEnumerator ButtonPress(GameObject button, GameObject otherButton)
    {
        button.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        otherButton.SetActive(true);
    }
}
