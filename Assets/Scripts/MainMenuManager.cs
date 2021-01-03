using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Multiplayer()
    {

    }

    public void LevelDesigner()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
