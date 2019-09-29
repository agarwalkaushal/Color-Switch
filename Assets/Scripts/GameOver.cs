using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject score;
    public GameObject dashCanvas;
    public GameObject dashboard;
    public GameObject close;
    public GameObject plusOneUI;
    public GameObject plusTwoUI;

    public void gameOver()
    {
        Debug.Log("gameOver called");
        Time.timeScale = 0;
        dashCanvas.SetActive(true);
        score.SetActive(false);
        dashboard.SetActive(false);
        close.SetActive(false);
        plusOneUI.SetActive(false);
        plusTwoUI.SetActive(false);

    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }
}
