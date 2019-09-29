using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject smallCircle1;
    public GameObject smallCircle2;
    public GameObject smallWheel1;
    public GameObject smallWheel2;
    public GameObject highScore;

    public Text highScoreText;

    int r1, r2;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
        r1 = Random.Range(-17, -12);
        r2 = Random.Range(13, 18);

        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScore.SetActive(true);
            highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
    }

    public void Update()
    {
        smallCircle1.transform.Rotate(0f, 0f, 10 * r1 * Time.deltaTime);
        smallCircle2.transform.Rotate(0f, 0f, 10 * r2 * Time.deltaTime);
        smallWheel1.transform.Rotate(0f, 0f, 10 * r1 * Time.deltaTime);
        smallWheel2.transform.Rotate(0f, 0f, 10 * r2 * Time.deltaTime);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=7338777534892840680");
    }
}
