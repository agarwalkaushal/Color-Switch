using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float jumpForce = 8.5f;
    private string currentColor = "null";
    private int Score = 0;
    private int highScore = 0;
    private bool boolDeath = false;

    private enum Colors
    {
        Cyan = 0,
        Yellow = 1,
        Magenta = 2,
        Pink = 3
    }

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Text scoreText;

    public Animator plusOneAnim;
    public Animator plusTwoAnim;

    public GameOver gameOver;

    public GameObject smallCircle;
    public GameObject square;
    public GameObject plus;
    public GameObject triangle;
    public GameObject colorChanger;
    public GameObject hand;
    public GameObject point;
    public GameObject score;
    public GameObject dashCanvas;
    public GameObject dashboard;
    public GameObject close;

    private Transform colorChangerTranform;

    public Color colorCyan;
    public Color colorYellow;
    public Color colorMagenta;
    public Color colorPink;

    private void Start()
    {
        Application.targetFrameRate = 60;
        setRandomColor();
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        Time.timeScale = 0;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if(Score>highScore)
        {
            highScore = Score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Point")
        {
            Destroy(collision.gameObject);
            Score += 1;
            scoreText.text = Score.ToString();
            plusOneAnim.Play("Plus One");
            return;
        }

        if (collision.tag == "Golden")
        {
            Destroy(collision.gameObject);
            Score += 2;
            scoreText.text = Score.ToString();
            plusTwoAnim.Play("Plus One");
            return;
        }

        if(collision.tag == "ColorChanger")
        {
            setRandomColor();
            colorChangerTranform = collision.gameObject.GetComponent<Transform>();
            Instantiate(colorChanger, new Vector3(0, colorChangerTranform.position.y + 7f, 0), Quaternion.identity);
            instantiateRandomObject();
            Destroy(collision.gameObject);
            return;
        }

        if(collision.tag != currentColor)
        {
            //Play death animation
            gameOver.gameOver();
        }
       
    }

    void instantiateRandomObject()
    {
        int r = Random.Range(0, 4);

        if(r != 1)
        {
            Instantiate(point, new Vector3(0, colorChangerTranform.position.y + 10.5f, 0), Quaternion.identity);
        }

        if (r == 0)
            Instantiate(smallCircle, new Vector3(0, colorChangerTranform.position.y + 10.5f, 0), Quaternion.identity);
        else if (r == 1)
        {
            int r1 = Random.Range(0, 2);
            if(r1 == 0)
                Instantiate(plus, new Vector3(-0.75f, colorChangerTranform.position.y + 10.5f, 0), Quaternion.identity);
            else
                Instantiate(plus, new Vector3(0.75f, colorChangerTranform.position.y + 10.5f, 0), Quaternion.identity);
        }
        else if(r == 2)
            Instantiate(square, new Vector3(0, colorChangerTranform.position.y + 10.5f, 0), Quaternion.identity);
        else
            Instantiate(triangle, new Vector3(0, colorChangerTranform.position.y + 10.5f, 0), Quaternion.identity);
    }

    void setRandomColor()
    {
        
        int index = Random.Range(0, 4);
        if(System.Enum.GetName(typeof(Colors), index) == currentColor && currentColor!="null")
        {
            if (index == 3)
                index = 0;
            else
                index += 1;
        }
        switch (index)
        {
            case 0:
                currentColor = "Cyan";
                sr.color = colorCyan;
                break;
            case 1:
                currentColor = "Yellow";
                sr.color = colorYellow;
                break;
            case 2:
                currentColor = "Magenta";
                sr.color = colorMagenta;
                break;
            case 3:
                currentColor = "Pink";
                sr.color = colorPink;
                break;

        }
    }

    public void Play()
    {
        hand.SetActive(false);
        Time.timeScale = 1;
        score.SetActive(true);
        dashboard.SetActive(true);
        rb.velocity = Vector2.up * jumpForce;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        dashCanvas.SetActive(true);
        score.SetActive(false);
        dashboard.SetActive(false);
        close.SetActive(true);      
    }

    public void Continue()
    {
        Time.timeScale = 1;
        dashCanvas.SetActive(false);
        score.SetActive(true);
        dashboard.SetActive(true);
        close.SetActive(false);
        rb.velocity = Vector2.up * jumpForce;
    }
}
