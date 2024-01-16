using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool Grounded;
    public GameObject Player;
    Vector2 velocity;
    public float direction;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        velocity = Player.GetComponent<Rigidbody2D>().velocity;
        Grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        direction = Player.GetComponent<PlayerScript>().direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Grounded = true;
            Player.GetComponent<PlayerScript>().GroundCheck = Grounded;
        }
    }
    private void OnTriggerExit2D()
    {
        Grounded = false;
        Player.GetComponent<PlayerScript>().GroundCheck = Grounded;
        if (direction != 0)
        {
            if (direction < 0)
            {
                Player.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.99f, Player.GetComponent<Rigidbody2D>().velocity.y);
            }
            else if (direction > 0)
            {
                Player.GetComponent<Rigidbody2D>().velocity = new Vector2(5.99f, Player.GetComponent<Rigidbody2D>().velocity.y);
            }
        }

    }
}
