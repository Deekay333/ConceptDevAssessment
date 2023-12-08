using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CamMov : MonoBehaviour
{
    Vector3 playerPos, oldPlayerPos, cameraPos, direction, directionOld;
    Vector2 cameraDistance, cameraVelocity;
    GameObject player;
    public bool directionChange;
    Rigidbody2D rb;
    public float velo;
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPos = new Vector3(player.transform.position.x, player.transform.localPosition.y + 1, -10);
        cameraPos = transform.position;
        cameraDistance = playerPos - cameraPos;
        float step = velo * Time.fixedDeltaTime * cameraDistance.magnitude;
        transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
    }
    private void Update()
    {

    }
}
