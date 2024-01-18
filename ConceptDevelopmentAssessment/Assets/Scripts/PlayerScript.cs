using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class PlayerScript : MonoBehaviour
{
    [Header("Player")]
    private Rigidbody2D rb;

    [Header("Movement")]
    public bool GroundCheck;
    private float movX;
    private float movY;
    public float groundedSpeed = 1f;
    private Vector2 movementVector;
    private Vector2 mov;
    public float speed;

    [Header("Jumping")]
    //public float jumpSpeed;
    //public float jumpHeight;

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
    private Animator anim;
    public float time;
    private bool swung;
    private GameObject pickaxe;
    private CapsuleCollider2D pickaxeCollider;
    public float coal;
    public float iron;

    [Header("Crouching")]
    private Transform bodyTransform;
    private BoxCollider2D bodyCollider;
    private Vector2 originalColliderSize;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        pickaxe = GameObject.Find("Pickaxe");
        pickaxeCollider = pickaxe.GetComponent<CapsuleCollider2D>();

        // Assuming the "Body" object is a direct child of the player object
        bodyTransform = transform.Find("Body");
        bodyCollider = bodyTransform.GetComponent<BoxCollider2D>();

        originalColliderSize = bodyCollider.size;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        coal = coal * 100;
        iron = iron * 100;
        coal = Mathf.Round(coal);
        iron = Mathf.Round(iron);
        coal = coal / 100;
        iron = iron / 100;
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
        if (pickaxeCollider.enabled == true)
        {
            pickaxeCollider.enabled = false;
        }
        if (Input.GetKey(KeyCode.Mouse0) && time <= 0)
        {
            if (time > -0.5f)
            {
                pickaxeCollider.enabled = true;
                time = 0.4f;
            }
            else
            {
                time = 0.15f;
            }
        }
    }
    /*private void OnMove(InputValue movement)
    {
        Debug.Log("AAAAAAAAAAAAAAA");
        Debug.Log(movement.Get<Vector2>().x);
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
    }*/
    private void Update()
    {
        time -= Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            movementVector.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movementVector.x = 1;
        }
        else
        {
            movementVector.x = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) >= 0.01f)
        {
            j = 0;
        }
        if ((Input.GetKeyDown(KeyCode.Space) && (Mathf.Abs(rb.velocity.y) < 0.01f) || jump == true) && anim.GetBool("isSwinging") == false)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(movementVector.x * jumpSpeed, jumpHeight));
            i = 0;
            jump = false;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            anim.SetBool("isSwinging", true);
            movementVector.x = 0;
        }
        else if(!Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetBool("isSwinging", false);
        }
        if (GroundCheck == true && j < 0.1f)
        {
            Debug.Log("Coyote");
            j = 0.2f;
            StartCoroutine(coyoteTime());
        }
        if (Input.GetKey(KeyCode.Space) == true && (Mathf.Abs(rb.velocity.y) > 0.01f))
        {
            rb.gravityScale = gravLow;
        }
        else if (Input.GetKey(KeyCode.Space) == false && (Mathf.Abs(rb.velocity.y) > 0.01f))
        {
            rb.gravityScale = gravHigh;
        }
        if (rb.velocity.y < -12)
        {
            rb.velocity = new Vector2(rb.velocity.x, -12);
        }
        if(rb.velocity.x > 0 || movementVector.x > 0 || rb.velocity.x < 0 || movementVector.x < 0)
        {
            anim.SetBool("isRunning", true);
        }
        if(movementVector.x < 0)
        {
            transform.localScale = new Vector3(-1.06f, 1.06f, 1.06f);
        }
        else if(movementVector.x > 0)
        {
            transform.localScale = new Vector3(1.06f, 1.06f, 1.06f);
        }
        else if(rb.velocity.x == 0 && movementVector.x == 0)
        {
            anim.SetBool("isRunning", false);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            bodyCollider.size = new Vector2(originalColliderSize.x, 0.41f * originalColliderSize.y);
            anim.SetBool("isCrawling", true);
        }
        else
        {
            bodyCollider.size = originalColliderSize;
            anim.SetBool("isCrawling", false);
        }
        anim.SetFloat("verticalVelo", rb.velocity.y);
    }
    IEnumerator coyoteTime()
    {
        yield return new WaitForSeconds(0.02f);
        rb.velocity = (new Vector2(0, 0));
        jump = true;
    }
}
