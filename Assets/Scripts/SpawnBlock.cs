using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    public void SpawnPlayer(GameObject Player)
    {
        Player.transform.position = GetTopOfBlock();
    }

    private Vector3 GetTopOfBlock()
    {
        return new Vector3(
            transform.position.x,
            transform.position.y + GetComponent<BoxCollider2D>().bounds.extents.y * 1.1f,
            transform.position.z);
    }

    private void Start()
    {

    }
}
