﻿using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    public void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if(player.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }
    }
}
