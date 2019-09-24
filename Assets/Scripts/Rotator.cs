using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float speed = 100f;

    private void Start()
    {
        //TODO: Make speed Random
    }

    void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
        
    }
}
