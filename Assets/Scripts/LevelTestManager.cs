using System.Collections.Generic;
using UnityEngine;

public class LevelTestManager : MonoBehaviour
{
    private GameObject Spawn;
    private readonly List<GameObject> Players = new List<GameObject>();


    private void Awake()
    {
        GameObject Player = Instantiate<GameObject>(Resources.Load<GameObject>(@"Player\TestPlayer"), null);
        Transform transform = Player.transform;
        Spawn = GameObject.Find("Spawn");
        Vector3 spawnPos = Spawn.transform.position;
        transform.position = new Vector3(spawnPos.x, spawnPos.y + 20, spawnPos.z);
        Player.transform.position = transform.position; // move the player to the spawn object   
    }
}