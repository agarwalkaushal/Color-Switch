using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject score;
    public GameObject dashCanvas;
    public GameObject plusOneUI;
    public GameObject plusTwoUI;

    public AudioSource buttonClick;

    public void gameOver()
    {
        Time.timeScale = 0;
        dashCanvas.SetActive(true);
        score.SetActive(false);
        plusOneUI.SetActive(false);
        plusTwoUI.SetActive(false);

    }

    public void Replay()
    {
        buttonClick.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        buttonClick.Play();
        SceneManager.LoadScene(0);
    }
}
