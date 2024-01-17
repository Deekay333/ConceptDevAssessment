using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool Grounded;
    public GameObject Player;
    Vector2 velocity;
    public float direction;
    private new string tag;
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        tag = collision.gameObject.tag;
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Iron" && collision.gameObject.tag != "Coal")
        {
            Grounded = true;
            Player.GetComponent<PlayerScript>().GroundCheck = Grounded;
        }
    }
    private void OnTriggerExit2D()
    {
        Debug.Log(tag);
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
