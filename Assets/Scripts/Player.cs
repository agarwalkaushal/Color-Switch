using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float jumpForce = 7.5f;
    public string currentColor;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public GameObject smallCircle;
    public GameObject colorChanger;

    private Transform colorChangerTranform;

    public Color colorCyan;
    public Color colorYellow;
    public Color colorMagenta;
    public Color colorPink;

    private void Start()
    {
        setRandomColor();
        rb.velocity = Vector2.up * jumpForce;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SmallCircle")
            return;

        if(collision.tag == "ColorChanger")
        {
            setRandomColor(); //TODO: Change to already existed color
            colorChangerTranform = collision.gameObject.GetComponent<Transform>();
            Instantiate(colorChanger, new Vector3(0, colorChangerTranform.position.y + 8f, 0), Quaternion.identity);
            Instantiate(smallCircle, new Vector3(0, colorChangerTranform.position.y + 12f, 0), Quaternion.identity); //TODO: Random object
            Destroy(collision.gameObject);
            return;
        }

        if(collision.tag != currentColor)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            //instantiate prefab
        }
       
    }

    void setRandomColor()
    {
        int index = Random.Range(0, 4);

        switch(index)
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
}
