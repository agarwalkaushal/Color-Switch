using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float jumpForce = 8f;
    private string currentColor = "null";
    private int Score = 0;
    private int highScore = 0;
    private bool boolDeath = false;
    private bool gameStart = false;

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
    public Text finalScoreText;
    public Text highScoreText;

    public Animator plusOneAnim;
    public Animator plusTwoAnim;

    public GameOver gameOver;

    public GameObject particleSystem;

    public AudioSource playerJump;
    public AudioSource plusOnePoint;
    public AudioSource plusTwoPoint;
    public AudioSource hitShape;
    public AudioSource background;

    public GameObject smallCircle;
    public GameObject square;
    public GameObject plus;
    //public GameObject doubleCircle;
    public GameObject colorChanger;
    public GameObject hand;
    public GameObject point;
    public GameObject score;
    public GameObject dashCanvas;
    public GameObject dashPanel;
    public GameObject destroyer;
    public GameObject fallDeath;

    private Transform colorChangerTranform;
    private Transform playerTransform;
    private SpriteRenderer playerSprite;

    public Color colorCyan;
    public Color colorYellow;
    public Color colorMagenta;
    public Color colorPink;

    private void Start()
    {
        Application.targetFrameRate = 60;
        playerTransform = gameObject.GetComponent<Transform>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        setRandomColor();
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (boolDeath)
        {
            background.Stop();
            finalScoreText.text = "Score: " + Score.ToString();
            if (PlayerPrefs.HasKey("HighScore"))
            {
                highScoreText.text = "Best: " + PlayerPrefs.GetInt("HighScore").ToString();
            }
            else
            {
                highScoreText.text = "Best: " + Score.ToString();
            }
        }
        if(gameStart && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !boolDeath)
        {
            playerJump.Stop();
            playerJump.Play();
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

        if (boolDeath)
            return;

        if (collision.tag == "Destroyer")
        {
            boolDeath = true;
            Handheld.Vibrate();
            StartCoroutine(FadeTo());
            StartCoroutine(playFallAnim());
            return;
        }

        if (collision.tag == "Point")
        {
            plusOnePoint.Play();
            Destroy(collision.gameObject);
            Score += 1;
            scoreText.text = Score.ToString();
            plusOneAnim.Play("Plus One");
            return;
        }

        if (collision.tag == "Golden")
        {
            plusTwoPoint.Play();
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

        if (collision.tag != currentColor)
        {
            boolDeath = true;
            Handheld.Vibrate();
            hitShape.Play();
            StartCoroutine(FadeTo());
            StartCoroutine(playDeathAnim());
        }
       
    }

    IEnumerator FadeTo()
    {
        Image image = dashPanel.GetComponent<Image>();
        float alpha = image.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1)
        {
            Color newColor = new Color(0, 0, 0  , Mathf.Lerp(alpha, 0.85f, t));
            image.color = newColor;
            yield return null;
        }
    }

    private IEnumerator playFallAnim()
    {
        playerSprite.color = Color.clear;
        yield return new WaitForSeconds(.50f);
        gameOver.gameOver();
    }

    private IEnumerator playDeathAnim()
    {
        Transform particleSystemTransform = particleSystem.GetComponent<Transform>();
        particleSystemTransform.position = playerTransform.position;
        playerSprite.color = Color.clear;
        particleSystem.SetActive(true);
        yield return new WaitForSeconds(1f);
        gameOver.gameOver();
    }


    void instantiateRandomObject()
    {
        int r = Random.Range(0, 3);

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
        else
            Instantiate(square, new Vector3(0, colorChangerTranform.position.y + 10.5f, 0), Quaternion.identity);

        //Instantiate(doubleCircle, new Vector3(0, colorChangerTranform.position.y + 10.5f, 0), Quaternion.identity);
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
        gameStart = true;
        background.Play();
        hand.SetActive(false);
        Time.timeScale = 1;
        score.SetActive(true);
        rb.velocity = Vector2.up * jumpForce;
    }

}
