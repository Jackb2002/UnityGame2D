using UnityEngine;

public class TimerBlock : MonoBehaviour
{
    public float TickTime { get; internal set; }

    private bool solid;
    private BoxCollider2D col;

    private void Awake()
    {
        InvokeRepeating("TickBlock", TickTime, TickTime);
        solid = true;
        col = GetComponent<BoxCollider2D>();
    }

    private void TickBlock()
    {
        col.isTrigger = !solid;
    }
}
