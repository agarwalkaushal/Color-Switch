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
    public GameObject mute;
    public GameObject low;
    public GameObject high;

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

        if (PlayerPrefs.HasKey("Audio"))
        {
            if (PlayerPrefs.GetFloat("Audio") == 0)
                highAudio();
            else if (PlayerPrefs.GetFloat("Audio") == 1)
                lowAudio();
            else
                muteAudio();
        }
        else
        {
            PlayerPrefs.SetInt("Audio", 1);
        }

        //
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

    public void muteAudio()
    {
        high.SetActive(false);
        mute.SetActive(false);
        low.SetActive(true);
        AudioListener.volume = 0.5f;
        PlayerPrefs.SetFloat("Audio", .5f);
    }

    public void lowAudio()
    {
        high.SetActive(true);
        mute.SetActive(false);
        low.SetActive(false);
        AudioListener.volume = 1f;
        PlayerPrefs.SetFloat("Audio", 1);
    }

    public void highAudio()
    {
        //Sprite to mute
        high.SetActive(false);
        mute.SetActive(true);
        low.SetActive(false);
        AudioListener.volume = 0;
        PlayerPrefs.SetFloat("Audio", 0);
    }
}
