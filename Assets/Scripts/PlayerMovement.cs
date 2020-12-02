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
    private int mapContactPoints = 0;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        onGround = true;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        float multiplier = MovementSpeed;
        if (Input.GetKey(Sprint))
        {
            multiplier *= 1.5f;
        }
        if (Input.GetKey(Right))
        {
            transform.Translate(Vector2.right * multiplier * Time.fixedDeltaTime);
        }
        else if (Input.GetKey(Left))
        {
            transform.Translate(Vector2.left * multiplier * Time.fixedDeltaTime);
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
            mapContactPoints++;
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            mapContactPoints--;
            if (mapContactPoints == 0)
            {
                onGround = false;
            }
        }
    }
}
