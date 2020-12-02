using UnityEngine;

public class GoalBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag + " tag hit goal");
        if (collision.gameObject.CompareTag("Player"))
        {
            LevelTestManager.EndTest();
        }
    }
}
