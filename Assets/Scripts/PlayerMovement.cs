using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public KeyCode Right;
    public KeyCode Left;
    public KeyCode Jump;
    public KeyCode Sprint;
    public float JumpForce;
    public float MovementSpeed;

    private Animator Animator;
    private Rigidbody2D rb;
    private bool onGround;
    private const float MAX_SPEED = 10f;

    private void Start()
    {
        //Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        onGround = true;
    }

    private void Update()
    {
        float multiplier = MovementSpeed;
        if (Input.GetKey(Sprint))
        {
            multiplier *= 1.5f;
        }


        if (Input.GetKey(Right))
        {
            //Animator.SetBool("IsWalking", true);
            rb.AddForce(Vector2.right * multiplier);
        }
        else if (Input.GetKey(Left))
        {
            //Animator.SetBool("IsWalking", true);
            rb.AddForce(Vector2.left * multiplier);
        }
        else
        {
            //Animator.SetBool("IsWalking", false);
        }

        if ((rb.velocity.magnitude > MAX_SPEED))
        {
            rb.velocity = new Vector2(MAX_SPEED, rb.velocity.y);
        }

        if (Input.GetKeyDown(Jump) && onGround)
        {
            rb.AddForce(new Vector3(0, 1.5f, 0) * JumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            onGround = false;
        }
    }
}
