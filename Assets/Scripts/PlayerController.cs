using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode Right;
    public KeyCode Left;
    public KeyCode Jump;
    public KeyCode Sprint;
    public float JumpForce;
    public float MovementSpeed;
    public float SpawnHealth;

    private Animator Animator;
    private Rigidbody2D rb;
    private bool onGround;
    private const float MAX_SPEED = 10f;
    private int mapContactPoints = 0;
    private float healthPoints;


    private void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        onGround = true;
        healthPoints = SpawnHealth;
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

    public void Damage(float amount)
    {
        if (healthPoints - amount <= 0)
        {
            LevelTestManager.SpawnPlayer(gameObject);
            healthPoints = SpawnHealth;
        }
        else
        {
            healthPoints -= amount;
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
