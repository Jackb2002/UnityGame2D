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
        if (Input.GetKeyDown(Jump) && onGround)
        {
            Animator.SetTrigger("Jump");
            rb.AddForce(new Vector3(0, 1.5f, 0) * JumpForce, ForceMode2D.Impulse);
        }


        float multiplier = MovementSpeed;
        if (Input.GetKey(Sprint))
        {
            multiplier *= 1.5f;
        }
        if (Input.GetKey(Right))
        {
            Animator.SetBool("IsWalking", true);
            Animator.SetBool("IsRight", true);
            transform.Translate(Vector2.right * multiplier * Time.fixedDeltaTime);
        }
        else if (Input.GetKey(Left))
        {
            Animator.SetBool("IsWalking", true);
            Animator.SetBool("IsRight", false);
            transform.Translate(Vector2.left * multiplier * Time.fixedDeltaTime);
        }
        else
        {
            Animator.SetBool("IsWalking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            Debug.Log($"Map points: {mapContactPoints + 1}");
            if (mapContactPoints == 0)
            {
                Animator.SetTrigger("Land");
            }
            mapContactPoints++;
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            Debug.Log($"Map points: {mapContactPoints - 1}");
            mapContactPoints--;
            if (mapContactPoints == 0)
            {
                onGround = false;
            }
        }
    }
}
