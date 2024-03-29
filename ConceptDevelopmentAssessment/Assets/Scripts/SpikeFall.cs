using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class SpikeFall : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    public float distance;
    bool isFalling = false;
    PlayerScript playerScript;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if(isFalling == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance);

            UnityEngine.Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);

            if(hit.transform != null){
                if(hit.transform.tag == "Player")
                {
                    rb.gravityScale = 5;
                    isFalling = true;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerScript.hit = true;
            Destroy(gameObject);
        }
        else
        {
            rb.gravityScale = 0;
            boxCollider2D.enabled = false;

        }
    }
}
