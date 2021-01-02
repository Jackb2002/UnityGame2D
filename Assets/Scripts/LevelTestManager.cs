using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTestManager : MonoBehaviour
{
    public static GameObject LevelBuilder;

    private void Awake()
    {
        if (LevelBuilder == null)
        {
            Debug.LogError("No level builder to return to");
        }
        GameObject Player = Instantiate(Resources.Load<GameObject>(@"Player\TestPlayer"), null);
        Player.transform.parent = GameObject.Find("LEVEL").transform;
        SpawnPlayer(Player);
    }

    internal static void SpawnPlayer(GameObject Player)
    {
        var s = GameObject.Find("Spawn");
        s.GetComponent<SpawnBlock>().SpawnPlayer(Player);
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