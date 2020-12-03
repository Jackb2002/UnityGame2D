using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTestManager : MonoBehaviour
{
    private SpawnBlock Spawn;
    public static GameObject LevelBuilder;

    private void Awake()
    {
        if (LevelBuilder == null)
        {
            Debug.LogError("No level builder to return to");
        }
        GameObject Player = Instantiate(Resources.Load<GameObject>(@"Player\TestPlayer"), null);
        Player.transform.parent = GameObject.Find("LEVEL").transform;
        var s = GameObject.Find("Spawn");
        Spawn = s.GetComponent<SpawnBlock>();
        Spawn.SpawnPlayer(Player);
    }

    public static void EndTest()
    {
        if (LevelBuilder == null)
        {
            return;
        }
        LevelBuilder.SetActive(true);
        LevelBuilder = null;
        Destroy(GameObject.Find("LEVEL"));
        SceneManager.UnloadSceneAsync(1, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
    }
}