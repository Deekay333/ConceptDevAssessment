using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool GroundCheck;
    private float movX;
    private float movY;
    public float groundedSpeed = 1f;
    private Vector2 movementVector;
    private Vector2 mov;
    public float speed;
    public float jumpSpeed;
    public float jumpHeight;
    private float i;
    private float j = 1;
    private float timer;
    private BoxCollider2D Collider;
    public float gravLow;
    public float gravHigh;
    private bool jump = false;
    public float direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = movementVector.x;
        i += Time.fixedDeltaTime;
        j += Time.fixedDeltaTime;
        if (GroundCheck == false)
        {
            if ((movementVector.x >= 0 && rb.velocity.x >= -6) || (movementVector.x <= 0 && rb.velocity.x < 6))
            {
                rb.AddForce(new Vector2(movementVector.x * speed, 0));
            }
            if (rb.velocity.x < -6)
            {
                rb.velocity = new Vector2(-5.99f, rb.velocity.y);
            }
            if (rb.velocity.x > 6)
            {
                rb.velocity = new Vector2(5.99f, rb.velocity.y);
            }
        }

        else if (GroundCheck == true && i > 0.1 && j > 0.1)
        {
            if (movementVector.x < 1 && movementVector.x > -1)
            {
                if (movementVector.x > 0 || mov.x > 0)
                {
                    if (movementVector == new Vector2(1, 0) || movementVector == new Vector2(0.8f, 0))
                    {
                        movementVector = new Vector2(1, 0);
                    }

                }
                else if (movementVector.x < 0 || mov.x < 0)
                {
                    if (movementVector == new Vector2(-1, 0) || movementVector == new Vector2(-0.8f, 0))
                    {
                        movementVector = new Vector2(-1, 0);
                    }
                }
            }

            rb.MovePosition(rb.position + new Vector2(movementVector.x, 0) * groundedSpeed * Time.fixedDeltaTime);
            rb.velocity = new Vector2(0, 0);
        }

    }
    private void OnMove(InputValue movement)
    {
        if (movement.Get<Vector2>().x == movementVector.x)
        {
            movementVector.x = movement.Get<Vector2>().x;
            mov.x = movement.Get<Vector2>().x;
            Debug.Log("CCCCCCCCC");
        }
        else
        {
            movementVector.x = movement.Get<Vector2>().x;
            mov.x = movement.Get<Vector2>().x;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck == false)
        {
            j = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck == true || jump == true)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(movementVector.x * jumpSpeed, jumpHeight));
            i = 0;
            jump = false;
        }
        if (GroundCheck == true && j < 0.1f)
        {
            Debug.Log("Coyote");
            j = 0.2f;
            StartCoroutine(coyoteTime());
        }
        if (Input.GetKey(KeyCode.Space) == true && GroundCheck == false)
        {
            rb.gravityScale = gravLow;
        }
        else if (Input.GetKey(KeyCode.Space) == false && GroundCheck == false)
        {
            rb.gravityScale = gravHigh;
        }
        if (rb.velocity.y < -12)
        {
            rb.velocity = new Vector2(rb.velocity.x, -12);
        }
    }
    IEnumerator coyoteTime()
    {
        yield return new WaitForSeconds(0.02f);
        rb.velocity = (new Vector2(0, 0));
        jump = true;
    }
}
