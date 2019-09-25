using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float speed;

    private void Start()
    {
        int r1 = Random.Range(-14, -9);
        int r2 = Random.Range(10, 15);
        int r3 = Random.Range(0, 2);
        if (r3 == 0)
            speed = 10 * r1;
        else
            speed = 10 * r2;
       
    }

    void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
        
    }
}
