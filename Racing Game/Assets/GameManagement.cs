using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public Animator menuChange;
    public GameObject buttonCar;
    public GameObject buttonBack;

    public void ToCarSelect()
    {
        menuChange.Play("MenuChange");
        StartCoroutine(ButtonPress(buttonCar, buttonBack));
    }

    public void BackToSelect()
    {
        menuChange.Play("MenuChangeBack");
        StartCoroutine(ButtonPress(buttonBack, buttonCar));
    }

    public IEnumerator ButtonPress(GameObject button, GameObject otherButton)
    {
        button.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        otherButton.SetActive(true);
    }
}
