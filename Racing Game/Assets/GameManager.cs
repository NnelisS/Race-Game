using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Gamescene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("finish"))
        {
            SceneManager.LoadScene("YouFinished");
        }
    }
}
