using UnityEngine;

public class DamageBlock : MonoBehaviour
{
    public float DPS { get; internal set; }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Damage(DPS/Time.deltaTime); // damage the calculated damage per frame
        }
    }
}
